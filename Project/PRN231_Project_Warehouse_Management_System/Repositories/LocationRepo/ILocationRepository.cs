using BusinessObjects;

namespace Repositories.LocationRepo
{
    public interface ILocationRepository
    {
        List<Location> GetLocations();
        Location GetLocationById(int id);
        void SaveLocation(Location location);
        void UpdateLocation(Location location);
        //void DeleteLocation(Location location);
    }
}
