using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class OrganizerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<EventOrganizer> EventOrganizers { get; set; } = new List<EventOrganizer>();
    }
    public class CreateOrganizerRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Your functional Email address is highly required.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "This field must match with the Password.")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        [Required]
        public IList<int> RoleIds { get; set; } = new List<int>();

    }
    public class UpdateOrganizerRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

    }
}
