using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.Security.Encryption
{
    public static class ModelBuilderExtension
    {
        public static void UseEncryption(this ModelBuilder modelBuilder,IEncryptionHelper encryptionHelper)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder), "There is not ModelBuilder object");
            if (encryptionHelper is null)
                throw new ArgumentNullException(nameof(encryptionHelper), "You should create encryption provider");

            var encryptionConverter = new EncryptionConverter(encryptionHelper);
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if(property.ClrType==typeof(string) && !IsDiscriminator(property))
                    {
                        object[] attributes = property.PropertyInfo.GetCustomAttributes(typeof(EncryptAttribute), false);
                        if (attributes.Any())
                            property.SetValueConverter(encryptionConverter);
                    }
                }
            }
        }

        private static bool IsDiscriminator(IMutableProperty property)
        {
            return property.Name == "Discriminator" || property.PropertyInfo == null;
        }
    }
}
