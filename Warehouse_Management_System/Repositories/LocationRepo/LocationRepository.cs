﻿using BusinessObjects;
using DataAccess;

namespace Repositories.LocationRepo
{
    public class LocationRepository : ILocationRepository
    {
        public void SaveLocation(Location location) => LocationDAO.GetInstance().SaveLocation(location);

        //public void DeleteLocation(Location location) => LocationDAO.GetInstance().DeleteLocation(location);

        public Location GetLocationById(int id) => LocationDAO.GetInstance().GetLocationById(id);

        public List<Location> GetLocations() => LocationDAO.GetInstance().GetLocations();

        public void UpdateLocation(Location location) => LocationDAO.GetInstance().UpdateLocation(location);
    }
}