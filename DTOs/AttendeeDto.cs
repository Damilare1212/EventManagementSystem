using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class AttendeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public ICollection<AttendeeCategory> AttendeeCategories { get; set; } = new List<AttendeeCategory>();
    }


    public class CreateAttendeeRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Your functional phone number is required.")]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "A functional Email is highly required.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The Confirm Password must match with the input Password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        [Required]
        public string Role { get; set; }
        public IList<int> CategoryIds { get; set; } = new List<int>();
    }


    public class UpdateAttendeeRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
