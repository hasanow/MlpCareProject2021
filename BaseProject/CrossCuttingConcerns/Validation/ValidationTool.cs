using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BaseProject.CrossCuttingConcerns.Validation
{
    public  static class ValidationTool
    {
        public static void Validate<T>(IValidator validator,T entity)
        {
            var result = validator.Validate(new ValidationContext<T>(entity));
            if (!result.IsValid)
            {
                throw new ValidationException("Giriş Bilgileri Doğru Formatta Değil",result.Errors,false);
            }
        }
    }
}
