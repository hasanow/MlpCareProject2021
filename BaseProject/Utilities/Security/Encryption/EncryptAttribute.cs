using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.Security.Encryption
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true,Inherited =false)]
    public class EncryptAttribute:Attribute
    {
    }
}
