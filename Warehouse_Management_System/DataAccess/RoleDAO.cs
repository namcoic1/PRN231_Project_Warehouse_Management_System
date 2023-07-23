using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RoleDAO
    {
        private static RoleDAO _instance = null;
        private static MyDbContext _context = null;
        private RoleDAO()
        {
        }
        public static RoleDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoleDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Role> GetRoles() => _context.Roles.ToList();
        public Role GetRoleById(int id) => _context.Roles.SingleOrDefault(c => c.Id == id);
        public Role GetRoleByLastId() => _context.Roles.OrderBy(c => c.Id).LastOrDefault();

        // do not to cud role
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
                //_context = new MyDbContext();
                _context.Entry<Role>(role).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteRole(Role role)
        {
            try
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
