using BusinessObjects;

namespace Repositories.InventoryRepo
{
    public interface IInventoryRepository
    {
        List<Inventory> GetInventories();
        Inventory GetInventoryById(string id);
        void SaveInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void DeleteInventory(Inventory inventory);
    }
}
