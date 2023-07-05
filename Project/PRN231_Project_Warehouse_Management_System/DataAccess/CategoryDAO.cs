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

        public List<Category> GetCategories() => MyDB_Context.GetInstance().Categories.ToList();
        public Category GetCategoryById(int id) => MyDB_Context.GetInstance().Categories.SingleOrDefault(c => c.CategoryID == id);

        public void SaveCategory(Category category)
        {
            try
            {
                MyDB_Context.GetInstance().Categories.Add(category);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Category>(category).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Categories.Remove(category);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
