using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenrAPI.Models
{
    public class CredentialModel : IdentityUser
    {
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
