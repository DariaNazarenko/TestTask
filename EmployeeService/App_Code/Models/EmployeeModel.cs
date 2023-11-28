using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EmployeeService.App_Code.Models
{
    [DataContract]
    public class EmployeeModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int ManagerId { get; set; }
        [DataMember]
        public bool Enable { get; set; }
        [DataMember]
        public List<EmployeeModel> Subordinates { get; set; }
    }
}