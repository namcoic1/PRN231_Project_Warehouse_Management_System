using BusinessObjects;
using DataAccess;

namespace Repositories.InventoryRepo
{
    public class InventoryRepository : IInventoryRepository
    {
        public void SaveInventory(Inventory inventory) => InventoryDAO.GetInstance().SaveInventory(inventory);

        public void DeleteInventory(Inventory inventory) => InventoryDAO.GetInstance().DeleteInventory(inventory);

        public Inventory GetInventoryById(string id) => InventoryDAO.GetInstance().GetInventoryById(id);

        public List<Inventory> GetInventories() => InventoryDAO.GetInstance().GetInventories();

        public void UpdateInventory(Inventory inventory) => InventoryDAO.GetInstance().UpdateInventory(inventory);
    }
}
