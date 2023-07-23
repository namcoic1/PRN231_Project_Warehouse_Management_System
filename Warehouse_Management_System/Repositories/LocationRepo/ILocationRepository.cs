using BusinessObjects;

namespace Repositories.LocationRepo
{
    public interface ILocationRepository
    {
        List<Location> GetLocations();
        List<Location> GetLocationByUserId(int? id);
        Location GetLocationById(int id);
        Location GetLocationByLastId();
        void SaveLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(Location location);
    }
}
