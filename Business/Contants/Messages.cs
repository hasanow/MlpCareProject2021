using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contants
{
   public static class Messages
    {
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";

        public static string UserAlreadyExists = "Bu Emailde bir kullanıcı mevcuttur";

        public static string RegisterSuccessfull = "Kullanıcı başarıyla kaydedildi";

        public static string AccessTokenCreated = "Giriş tokeni başarıyla oluşturuldu";

        public static string AutorizationDenied = "Yetkisiz İşlem";
    }
}
