﻿using BusinessObjects;
using DataAccess;

namespace Repositories.CarrierRepo
{

    public class CarrierRepository : ICarrierRepository
    {
        // repository pattern
        public void SaveCarrier(Carrier carrier) => CarrierDAO.GetInstance.SaveCarrier(carrier);

        public void DeleteCarrier(Carrier carrier) => CarrierDAO.GetInstance.DeleteCarrier(carrier);

        public Carrier GetCarrierById(int id) => CarrierDAO.GetInstance.GetCarrierById(id);

        public Carrier GetCarrierByLastId() => CarrierDAO.GetInstance.GetCarrierByLastId();

        public List<Carrier> GetCarriers() => CarrierDAO.GetInstance.GetCarriers();

        public void UpdateCarrier(Carrier carrier) => CarrierDAO.GetInstance.UpdateCarrier(carrier);
    }
}
