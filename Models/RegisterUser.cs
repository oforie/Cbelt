using System;
using System.ComponentModel.DataAnnotations;
using beltretake;
using Microsoft.EntityFrameworkCore;

namespace beltretake.Models
{
    public class RegisterUser: BaseEntity
    { 
       
        
        [Required(ErrorMessage="Please enter a valid name. Must be at least 2 characters")]
        [MinLength(2)]
        public string Name {get; set;}

        [Required(ErrorMessage="Please enter a valid alias. Must be at least 2 characters")]
        [MinLength(2)]
        public string Alias {get; set;}

        [Required(ErrorMessage="Please enter a valid Email")]
        [EmailAddress]
        public string Email {get; set;}

       
        [Required(ErrorMessage="Password must be at least 8 characters long")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Password and confirmation must match")]
        public string PasswordConfirmation {get; set;}  
    }
}
