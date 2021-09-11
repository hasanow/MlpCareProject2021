using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.Security.Encryption
{
    public interface IEncryptionHelper
    {
        string Encrypt(string dataToEncrypt);
        string Decrypt(string dataToDecrypt);
    }
}
