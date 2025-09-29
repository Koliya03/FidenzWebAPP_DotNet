using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FidenzApp.Application.DTO
{
    public class updateCustomerDto
    {
        public updateCustomerDto(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Name can contain letters and spaces only.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^\+1 \(\d{3}\) \d{3}-\d{4}$",
         ErrorMessage = "Phone must be in the format +1 (###) ###-####")]
        public string? Phone { get; set; }
    }
}
