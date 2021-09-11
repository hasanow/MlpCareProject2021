using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserListModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserListModel(int userId,string firstName,string lastName)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
