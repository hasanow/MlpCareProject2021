using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Entities.Concrete
{
    public class OperationClaim:IEntity
    {
        [Key]
        public int ClaimId { get; set; }
        public string Name { get; set; }

        public IList<UserOperationClaims> UserOperationClaims { get; set; }
    }
}
