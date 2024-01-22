using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using LMSEntity;
using System.Runtime.Serialization;

namespace LMS.BLL
{
    public class EmployeePhoto : EntityBase
    {
        [DataMember, DataColumn(true)]
        public int Id { get; set; }

        [DataMember, DataColumn(true)]
        public int EmployeeId { get; set; }

        [DataMember, DataColumn(true)]
        public byte[] PhotoSignature { get; set; }

        [DataMember, DataColumn(true)]
        public bool IsPhoto { get; set; }

        //Extend
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public string SelectedClass { get; set; }
        public string ActionType { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
