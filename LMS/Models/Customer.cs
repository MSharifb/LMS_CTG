using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMS.Web.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Required.")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Required.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Orders Placed")]
        public int OrdersPlaced { get; set; }
        [DisplayName("Date of Last Order")]
        public DateTime? DateOfLastOrder { get; set; }
        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
