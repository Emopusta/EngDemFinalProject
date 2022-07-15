using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz. ";
        public static string MaintenanceTime = "Bakım yapılıyor. ";
        public static string ProductsListed = "Ürünler listelendi. ";
        public static string ProductCountOfCategoryError = "Kategoride olabilecek ürün sayısını aştınız. ";
        public static string ProductNameExistError = "Bu isimde bir ürün var. ";
        public static string CategoryAmountNotValidError = "Maxmimum category sayısı geçildi. ";
        public static string AuthorizationDenied = "Yetkiniz yok. ";
        public static string UserRegistered = "Kullanıcı kayıt oldu. ";
        public static string UserNotFound = "Kullanıcı bulunamadı. ";
        public static string PasswordError = "Şifre hatalı. ";
        public static string SuccessfulLogin = "Giriş başarılı. ";
        public static string UserAlreadyExists = "Böyle bir kullanıcı mevcut. ";
        public static string AccessTokenCreated = "Access Token Oluşturuldu.";
    }
}
