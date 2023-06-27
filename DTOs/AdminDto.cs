using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AdminPhoto { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
         public bool IsDeleted { get; set; }


    }
    public class CreateAdminRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AdminPhoto { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "A fuctional Email is required.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The Confirm Password must match with the registered password." )]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "You are expected to choose a role.")]
        public IList<int> RoleIds { get; set; } = new List<int>();

    }
    public class UpdateAdminRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AdminPhoto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
