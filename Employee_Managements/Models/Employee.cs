using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Employee_Managements.Models
{
    public class Employee
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "Designation Should not be null.")]
        public string DesignationName { get; set; }
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Experiece should not be null")]
        public int Experience { get; set; }
        public SelectList DesignationList { get; set; }
    }
}
