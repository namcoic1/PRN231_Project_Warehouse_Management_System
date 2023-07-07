using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class InventoryDAO
    {
        private static InventoryDAO _instance = null;
        private InventoryDAO() { }
        public static InventoryDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new InventoryDAO();
            }
            return _instance;
        }

        public List<Inventory> GetInventories() => MyDbContext.GetInstance().Inventories.ToList();
        public Inventory GetInventoryById(string id) => MyDbContext.GetInstance().Inventories.SingleOrDefault(c => c.ID == id);

        public void SaveInventory(Inventory inventory)
        {
            try
            {
                MyDbContext.GetInstance().Inventories.Add(inventory);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateInventory(Inventory inventory)
        {
            try
            {
                MyDbContext.GetInstance().Entry<Inventory>(inventory).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteInventory(Inventory inventory)
        {
            try
            {
                MyDbContext.GetInstance().Inventories.Remove(inventory);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
