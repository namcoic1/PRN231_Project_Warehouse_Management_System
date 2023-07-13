using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class InventoryDAO
    {
        private static InventoryDAO _instance = null;
        private static MyDbContext _context = null;
        private InventoryDAO()
        {
        }
        public static InventoryDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InventoryDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Inventory> GetInventories() => _context.Inventories.Include(l => l.Location).Include(p => p.Product).ToList();
        public Inventory GetInventoryById(string id) => _context.Inventories.Include(l => l.Location).Include(p => p.Product).SingleOrDefault(c => c.Id.Equals(id));
        public Inventory GetInventoryByLastId() => _context.Inventories.Include(l => l.Location).Include(p => p.Product).OrderBy(i => i.Id).LastOrDefault();

        public void SaveInventory(Inventory inventory)
        {
            try
            {
                _context.Inventories.Add(inventory);
                _context.SaveChanges();
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
                _context.Entry<Inventory>(inventory).State = EntityState.Modified;
                _context.SaveChanges();
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
                _context.Inventories.Remove(inventory);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
