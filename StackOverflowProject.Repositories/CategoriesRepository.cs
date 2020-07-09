using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int cid);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryID(int CategoryID);
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        StackOverflowDatabaseDbContext db;

        public CategoriesRepository()
        {
            db = new StackOverflowDatabaseDbContext(); 
        }

        public void InsertCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }

        public void UpdateCategory(Category c)
        {
            Category Ct = db.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();
            if (Ct != null)
            {
                Ct.CategoryName = c.CategoryName;
                db.SaveChanges();
            }            
        }

        public void DeleteCategory(int cid)
        {
            Category Ct = db.Categories.Where(temp => temp.CategoryID == cid).FirstOrDefault();
            if (Ct != null)
            {
                db.Categories.Remove(Ct);
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoryByCategoryID(int CategoryID)
        {
            List<Category> cid = db.Categories.Where(temp => temp.CategoryID == CategoryID).ToList();
            return cid;
        }
    }
}
