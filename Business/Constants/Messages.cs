using System;
using System.Collections.Generic;
using System.Linq;
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
        internal static string ProductCountOfCategoryError = "Kategoride olabilecek ürün sayısını aştınız. ";
        internal static string ProductNameExistError = "Bu isimde bir ürün var. ";
        internal static string CategoryAmountNotValidError = "Maxmimum category sayısı geçildi. ";
    }
}
