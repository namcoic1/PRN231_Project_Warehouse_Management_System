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

        public List<Location> GetLocations() => MyDB_Context.GetInstance().Locations.ToList();
        public Location GetLocationById(int id) => MyDB_Context.GetInstance().Locations.SingleOrDefault(c => c.LocationID == id);

        public void SaveLocation(Location location)
        {
            try
            {
                MyDB_Context.GetInstance().Locations.Add(location);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Location>(location).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
