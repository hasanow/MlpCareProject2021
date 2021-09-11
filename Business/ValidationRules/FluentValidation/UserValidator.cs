using BaseProject.Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User_T>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.TcKimlikNo)
                .Length(11).WithMessage("TC Kimlik No 11 hane olmalıdır")
                .Must(SadeceRakamOlmali).WithMessage("TC Kimlik Numarası sadece rakam içermelidir.");
            RuleFor(u => u.FirstName).NotEmpty().NotNull().WithMessage("Ad alanı boş geçilemez");
            RuleFor(u => u.LastName).NotEmpty().NotNull().WithMessage("Soyad alanı boş geçilemez");
        }
        private bool SadeceRakamOlmali(string TCKimlikNo)
        {
            foreach (var r in TCKimlikNo)
            {
                if (!int.TryParse(r.ToString(), out _))
                    return false;
            }
            return true;
        }
    }
    public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.TcKimlikNo)
                .Length(11).WithMessage("TC Kimlik No 11 hane olmalıdır");
            RuleFor(u=>u.TcKimlikNo).Must(SadeceRakamOlmali).WithMessage("TC Kimlik Numarası sadece rakam içermelidir.");
            RuleFor(u => u.FirstName).NotEmpty().NotNull().WithMessage("Ad alanı boş geçilemez");
            RuleFor(u => u.LastName).NotEmpty().NotNull().WithMessage("Soyad alanı boş geçilemez");
            When(u => u.UserId == 0, () => RuleFor(u => u.Password).NotEmpty().NotNull().WithMessage("Yeni kullanıcılar için password alanı doldurulmalıdır"));
            
        }
        private bool SadeceRakamOlmali(string TCKimlikNo)
        {
            foreach (var r in TCKimlikNo)
            {
                if (!int.TryParse(r.ToString(), out _))
                    return false;
            }
            return true;
        }
    }
}
