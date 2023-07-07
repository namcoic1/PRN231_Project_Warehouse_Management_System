using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LocationDAO
    {
        private static LocationDAO _instance = null;
        private LocationDAO() { }
        public static LocationDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LocationDAO();
            }
            return _instance;
        }

        public List<Location> GetLocations() => MyDbContext.GetInstance().Locations.ToList();
        public Location GetLocationById(int id) => MyDbContext.GetInstance().Locations.SingleOrDefault(c => c.ID == id);

        public void SaveLocation(Location location)
        {
            try
            {
                MyDbContext.GetInstance().Locations.Add(location);
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Entry<Location>(location).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // do not delete location
        //public void DeleteLocation(Location location)
        //{
        //    try
        //    {
        //        MyDB_Context.GetInstance().Locations.Remove(location);
        //        MyDB_Context.GetInstance().SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
