using Employee.BusinessLogic.IBusinessLogic;
using Employee.Model.Models;
using Employee.Model.RequestModels;
using Employee.WebApi.Extension;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Net;

namespace Employee.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Properties-
        private readonly IEmployeeBusinessLogic _employeeLogic;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeBusinessLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }
        #endregion

        #region Public Methods
        [HttpPost]
        [Produces("application/json")]
        public async Task<ResponseModel> GetAllEmployees(EmployeeListRequest model)
        {
            return await _employeeLogic.GetAllEmployees(model.Condition, model.SortColumn, model.SortDirection, model.StartIndex, model.PageSize);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ResponseModel> GetEmployees(EmployeeSearchModel model)
        {
            return await _employeeLogic.GetEmployees(model.From,model.To,model.Name,model.DesignationId);
        }

        [HttpPost("AddEmployee")]
        public async Task<ResponseModel> AddEmployee(EmployeeRequestModel employee)
        {
            ResponseModel result = new();
            TryValidateModel(employee);
            if (!ModelState.IsValid)
            {
                result.Message = ModelState.GetModelStateErrors();
                result.HttpStatus = HttpStatusCode.BadRequest;
                return result;
            }

            return await _employeeLogic.AddEmployee(employee);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ResponseModel> UpdateEmployee(EmployeeRequestModel employee)
        {
            ResponseModel result = new();
            TryValidateModel(employee);
            if (!ModelState.IsValid)
            {
                result.Message = ModelState.GetModelStateErrors();
                result.HttpStatus = HttpStatusCode.BadRequest;
                return result;
            }

            return await _employeeLogic.UpdateEmployee(employee);
        }

        [HttpDelete("DeleteEmployeeById")]
        public async Task<ResponseModel> DeleteEmployeeById(int employeeId)
        {
            return await _employeeLogic.DeleteEmployeeById(employeeId);
        }

        [HttpGet("GetEmployeeById")]
        //[Produces("application/json")]
        public async Task<ResponseModel> GetEmployeeById(int employeeId)
        {
            return await _employeeLogic.GetEmployeeById(employeeId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ResponseModel> GetDepartment()
        {
            return await _employeeLogic.GetDepartment();
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ResponseModel> GetDesignation()
        {
            return await _employeeLogic.GetDesignation();
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ResponseModel> GetKnowledge()
        {
            return await _employeeLogic.GetKnowledge();
        }
        #endregion
    }
}
