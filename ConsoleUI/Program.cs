using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DTO == Data Transformation Object
            //IoC
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            

      
            //CategoryTest();
            //ProductTest(productManager);
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest(ProductManager productManager)
        {
            var result = productManager.GetProductDetails();
            
            if (result.Success)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " / " + product.CategoryName);
                }
                
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
        
    }
   
    
}
