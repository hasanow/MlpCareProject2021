using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Entities.Concrete
{
    public class UserOperationClaims:IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User_T User { get; set; }
        public int OperationClaimId { get; set; }
        public OperationClaim Claim { get; set; }
    }
}
