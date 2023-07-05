using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RoleDAO
    {
        private static RoleDAO _instance = null;
        private RoleDAO() { }
        public static RoleDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RoleDAO();
            }
            return _instance;
        }

        public List<Role> GetRoles() => MyDB_Context.GetInstance().Roles.ToList();
        public Role GetRoleById(int id) => MyDB_Context.GetInstance().Roles.SingleOrDefault(c => c.RoleID == id);

        public void SaveRole(Role role)
        {
            try
            {
                MyDB_Context.GetInstance().Roles.Add(role);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Role>(role).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
