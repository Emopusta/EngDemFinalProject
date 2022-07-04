using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DTO == Data Transformation Object
            //IoC
            ProductManager productManager = new ProductManager(new EfProductDal());




            //CategoryTest();
            ProductTest(productManager);
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest(ProductManager productManager)
        {
            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine(product.ProductName + " / " + product.CategoryName);
            }
            Console.WriteLine("--------------------------------------------");
            foreach (var product in productManager.GetAllByCategoryId(8))
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}
