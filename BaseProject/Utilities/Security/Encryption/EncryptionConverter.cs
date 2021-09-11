using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.Security.Encryption
{
    internal sealed class EncryptionConverter:ValueConverter<string,string>
    {
        public EncryptionConverter(IEncryptionHelper encryptionHelper, ConverterMappingHints mappingHints = null) : base(x => encryptionHelper.Encrypt(x), x => encryptionHelper.Decrypt(x))
        {

        }
    }
}
