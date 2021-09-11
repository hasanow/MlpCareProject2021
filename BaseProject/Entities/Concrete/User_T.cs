using BaseProject.Utilities.Security.Encryption;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Entities.Concrete
{
    public class User_T:IEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Encrypt]
        public string TcKimlikNo { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }

        public IList<UserOperationClaims> UserOperationClaims { get; set; }
    }
}
