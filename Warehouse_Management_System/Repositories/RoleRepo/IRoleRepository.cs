using BusinessObjects;

namespace Repositories.RoleRepo
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role GetRoleById(int id);
        void SaveRole(Role role);
        void UpdateRole(Role role);
        //void DeleteRole(Role role);
    }
}
