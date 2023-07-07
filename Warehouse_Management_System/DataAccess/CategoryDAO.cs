using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO _instance = null;
        private CategoryDAO() { }
        public static CategoryDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CategoryDAO();
            }
            return _instance;
        }

        public List<Category> GetCategories() => MyDbContext.GetInstance().Categories.ToList();
        public Category GetCategoryById(int id) => MyDbContext.GetInstance().Categories.SingleOrDefault(c => c.ID == id);

        public void SaveCategory(Category category)
        {
            try
            {
                MyDbContext.GetInstance().Categories.Add(category);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCategory(Category category)
        {
            try
            {
                MyDbContext.GetInstance().Entry<Category>(category).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCategory(Category category)
        {
            try
            {
                MyDbContext.GetInstance().Categories.Remove(category);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
