using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Model.RequestModels
{
    public class EmployeeSearchModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string Name { get; set; }

        public int? DesignationId { get; set; }
    }
}
