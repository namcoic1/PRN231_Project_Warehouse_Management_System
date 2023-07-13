using BusinessObjects;
using DataAccess;

namespace Repositories.RoleRepo
{
    public class RoleRepository : IRoleRepository
    {
        public void SaveRole(Role role) => RoleDAO.GetInstance.SaveRole(role);

        public void DeleteRole(Role role) => RoleDAO.GetInstance.DeleteRole(role);

        public Role GetRoleById(int id) => RoleDAO.GetInstance.GetRoleById(id);

        public Role GetRoleByLastId() => RoleDAO.GetInstance.GetRoleByLastId();

        public List<Role> GetRoles() => RoleDAO.GetInstance.GetRoles();

        public void UpdateRole(Role role) => RoleDAO.GetInstance.UpdateRole(role);
    }
}
