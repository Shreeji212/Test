using ConsumeWebAPI.Models;
using Employee.Model.Models;
using Employee.Model.RequestModels;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsumeWebAPI.Controllers
{
    public class EmployeeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7016/api");
        private readonly HttpClient _client;
        public EmployeeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortField, string currentSortField, string currentSortOrder, string currentFilter, int? pageNo, DateTime? From, DateTime? To, string name, int? DesignationId)
        {
            int pageSize = 5;
            List<EmployeeViewModel> employees = [];

            EmployeeSearchModel _param = new()
            {
                Name = string.IsNullOrEmpty(name) ? "" : name,
                To = !To.HasValue ? null : To,
                From = !From.HasValue ? null : From,
                DesignationId = DesignationId.HasValue ? null : DesignationId,
            };

            var json = JsonConvert.SerializeObject(_param);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_client.BaseAddress + "/Employee/GetEmployees", content);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ResponseModel>(data);
                var employeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(result.Data.ToString());
                employees = employeeList.Select(x => new EmployeeViewModel()
                {
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CreatedDate = x.CreatedDate,
                    DepartmentName = x.DepartmentName,
                    DesignationName = x.DesignationName,
                    IsDeleted = x.IsDeleted,
                    JoiningDate = x.JoiningDate,
                    KnowledgeName = x.KnowledgeName,
                    ReportingPerson = x.ReportingPerson,
                    Salary = x.Salary,
                    UpdatedDate = x.UpdatedDate
                }).ToList();
            }
            ViewData["CurrentSort"] = sortField;
            employees = this.SortEmployeeData(employees, sortField, currentSortField, currentSortOrder);
            return View(PagingList<EmployeeViewModel>.CreateAsync(employees.AsQueryable<EmployeeViewModel>(), pageNo ?? 1, pageSize));

        }
        private List<EmployeeViewModel> SortEmployeeData(List<EmployeeViewModel> employees, string sortField, string currentSortField, string currentSortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "FirstName";
                ViewBag.SortOrder = "Asc";
            }
            else
            {
                if (currentSortField == sortField)
                {
                    ViewBag.SortOrder = currentSortOrder == "Asc" ? "Desc" : "Asc";
                }
                else
                {
                    ViewBag.SortOrder = "Asc";
                }
                ViewBag.SortField = sortField;
            }

            var propertyInfo = typeof(EmployeeViewModel).GetProperty(ViewBag.SortField);
            if (ViewBag.SortOrder == "Asc")
            {
                employees = employees.OrderBy(s => propertyInfo.GetValue(s, null)).ToList();
            }
            else
            {
                employees = employees.OrderByDescending(s => propertyInfo.GetValue(s, null)).ToList();
            }
            //int pageSize = 10;
            //int pageNumber = (pageNo ?? 1);
            //return employees.ToPagedList(pageNumber, pageSize).ToList();
            return employees;

        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadDepartment();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Employee/AddEmployee", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["successMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [NonAction]
        private void LoadDepartment()
        {
            List<DepartmentModel> department = new List<DepartmentModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employee/GetDepartment").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ResponseModel>(data);
                var departmentList = JsonConvert.DeserializeObject<List<DepartmentModel>>(result.Data.ToString());
                ViewBag.Department = new SelectList(departmentList.ToList(), "DepartmentId", "DepartmentName");
            }
        }
    }
}
