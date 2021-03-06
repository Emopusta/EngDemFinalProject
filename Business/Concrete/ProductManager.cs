using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //[LogAspect] --> AOP
        //[Validate]
        //[RemoveCache]
        //[Transaction]
        //[Performance] .......
        //AOP   

        [SecuredOperation("admin,editor")] //Claim
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product); attribute olarak yazıldı

            //business codes 
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),
                CheckIsCategoryAmountValid(15)
                );

            if (result != null)
            {
                return result;
            }
            
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }

        [CacheAspect] // key, value pair
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 10)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id),Messages.ProductsListed);
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var result = _productDal.Get(p => p.ProductId == product.ProductId);
            if (result != null)
            {
                _productDal.Update(product);
                return new SuccessResult();
            }
            return new ErrorResult();
        }







        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result < 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameExistError);
            }
            return new SuccessResult();
        }
        /*
        // başka servis çağırılarak daha doğru çözülür çünkü her manager da kendi işi yapılır burda kategori işi yapılıyor.
        private IResult CheckIsCategoryAmountValid(int categoryMaxAmount)
        {
            var result = (ICategoryDal)Activator.CreateInstance(typeof(EfCategoryDal));
            var categoryList = result.GetAll();
            if (categoryList.Count >= categoryMaxAmount)
            {
                return new ErrorResult(Messages.CategoryAmountNotValidError);
            }
            return new SuccessResult();
        }
        */
        private IResult CheckIsCategoryAmountValid(int categoryMaxAmount)
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count >= categoryMaxAmount)
            {
                return new ErrorResult(Messages.CategoryAmountNotValidError);
            }
            return new SuccessResult();
        }

        }
}
