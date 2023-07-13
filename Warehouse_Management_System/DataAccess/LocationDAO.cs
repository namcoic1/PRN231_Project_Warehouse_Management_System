using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LocationDAO
    {
        private static LocationDAO _instance = null;
        private static MyDbContext _context = null;
        private LocationDAO()
        {
        }
        public static LocationDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocationDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Location> GetLocations() => _context.Locations.Include(u => u.User).ToList();
        public Location GetLocationById(int id) => _context.Locations.Include(u => u.User).SingleOrDefault(c => c.Id == id);
        public Location GetLocationByLastId() => _context.Locations.Include(u => u.User).OrderBy(c => c.Id).LastOrDefault();

        public void SaveLocation(Location location)
        {
            try
            {
                _context.Locations.Add(location);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateLocation(Location location)
        {
            try
            {
                _context.Entry<Location>(location).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // do not delete location
        public void DeleteLocation(Location location)
        {
            try
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
