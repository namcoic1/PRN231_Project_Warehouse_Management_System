using BusinessObjects;
using DataAccess;

namespace Repositories.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
        public void SaveCategory(Category category) => CategoryDAO.GetInstance().SaveCategory(category);

        public void DeleteCategory(Category category) => CategoryDAO.GetInstance().DeleteCategory(category);

        public Category GetCategoryById(int id) => CategoryDAO.GetInstance().GetCategoryById(id);

        public List<Category> GetCategories() => CategoryDAO.GetInstance().GetCategories();

        public void UpdateCategory(Category category) => CategoryDAO.GetInstance().UpdateCategory(category);
    }
}
