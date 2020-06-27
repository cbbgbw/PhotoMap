using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMap.Backend.Models.Users
{
    public class UserRegisterModel
    {
        public Guid UserROWGUID = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedAt => DateTime.Now;

        
    }
}
