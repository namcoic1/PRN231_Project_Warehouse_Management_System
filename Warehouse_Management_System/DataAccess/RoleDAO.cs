using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RoleDAO
    {
        private static RoleDAO _instance = null;
        private MyDbContext _context;
        private RoleDAO()
        {
            //_context = MyDbContext.GetInstance();
            _context = new MyDbContext();
        }
        public static RoleDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RoleDAO();
            }
            return _instance;
        }

        public List<Role> GetRoles() => _context.Roles.ToList();
        public Role GetRoleById(int id) => _context.Roles.SingleOrDefault(c => c.ID == id);

        public void SaveRole(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateRole(Role role)
        {
            try
            {
                _context.Entry<Role>(role).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // do not to delete role
        //public void DeleteRole(Role role)
        //{
        //    try
        //    {
        //        MyDB_Context.GetInstance().Roles.Remove(role);
        //        MyDB_Context.GetInstance().SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
