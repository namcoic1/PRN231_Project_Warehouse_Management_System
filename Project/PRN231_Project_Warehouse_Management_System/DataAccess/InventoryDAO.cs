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

        public List<Inventory> GetInventories() => MyDB_Context.GetInstance().Inventories.ToList();
        public Inventory GetInventoryById(string id) => MyDB_Context.GetInstance().Inventories.SingleOrDefault(c => c.InventoryID == id);

        public void SaveInventory(Inventory inventory)
        {
            try
            {
                MyDB_Context.GetInstance().Inventories.Add(inventory);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Inventory>(inventory).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Inventories.Remove(inventory);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
