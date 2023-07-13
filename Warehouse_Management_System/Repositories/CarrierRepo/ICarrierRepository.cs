using BusinessObjects;

namespace Repositories.CarrierRepo
{
    public interface ICarrierRepository
    {
        List<Carrier> GetCarriers();
        Carrier GetCarrierById(int id);
        Carrier GetCarrierByLastId();
        void SaveCarrier(Carrier carrier);
        void UpdateCarrier(Carrier carrier);
        void DeleteCarrier(Carrier carrier);
    }
}
