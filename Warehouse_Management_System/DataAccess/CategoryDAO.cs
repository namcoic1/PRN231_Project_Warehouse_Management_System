using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO _instance = null;
        private static MyDbContext _context = null;
        private CategoryDAO()
        {
        }
        public static CategoryDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CategoryDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Category> GetCategories() => _context.Categories.ToList();
        public Category GetCategoryById(int id) => _context.Categories.SingleOrDefault(c => c.Id == id);
        public Category GetCategoryByLastId() => _context.Categories.OrderBy(c => c.Id).LastOrDefault();

        public void SaveCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
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
                _context.Entry<Category>(category).State = EntityState.Modified;
                _context.SaveChanges();
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
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
