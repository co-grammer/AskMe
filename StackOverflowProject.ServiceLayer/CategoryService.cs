using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int cid);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByID(int CategoryID);
    }

    public class CategoryService : ICategoriesService
    {
        ICategoriesRepository cr;

        public CategoryService()
        {
            cr = new CategoriesRepository();
        }

        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.InsertCategory(c);
        }

        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.UpdateCategory(c);
        }

        public void DeleteCategory(int cid)
        {
            cr.DeleteCategory(cid);
        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> c = cr.GetCategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(c);
            return cvm;
        }

        public CategoryViewModel GetCategoryByID(int CategoryID)
        {
            Category c = cr.GetCategoryByCategoryID(CategoryID).FirstOrDefault();
            CategoryViewModel cvm = null;
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category, CategoryViewModel>(c);                
            }
            return cvm;
        }
    }
}
