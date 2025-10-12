using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices;

public class ProductService : BaseService, IProductService
{
    public ProductService(ApplicationDbContext dbContext, IMapper mapper, ILogger<ProductService> logger)
        : base(dbContext, mapper, logger) { }

    public ProductVm AddOrUpdateProduct(AddOrUpdateProductVm vm)
    {
        try
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));

            var entity = Mapper.Map<Product>(vm);

            if (vm.Id.HasValue && vm.Id.Value != 0)
                DbContext.Products.Update(entity);
            else
                DbContext.Products.Add(entity);

            DbContext.SaveChanges();
            return Mapper.Map<ProductVm>(entity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public ProductVm? GetProduct(Expression<Func<Product, bool>> filter)
    {
        try
        {
            if (filter is null) throw new ArgumentNullException(nameof(filter));
            var entity = DbContext.Products.FirstOrDefault(filter);
            return entity is null ? null : Mapper.Map<ProductVm>(entity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<ProductVm> GetProducts(Expression<Func<Product, bool>>? filter = null)
    {
        try
        {
            var q = DbContext.Products.AsQueryable();
            if (filter != null) q = q.Where(filter);
            return Mapper.Map<IEnumerable<ProductVm>>(q.ToList());
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
