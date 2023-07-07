using BusinessObjects;

namespace Repositories.CarrierRepo
{
    public interface ICarrierRepository
    {
        List<Carrier> GetCarriers();
        Carrier GetCarrierById(int id);
        void SaveCarrier(Carrier carrier);
        void UpdateCarrier(Carrier carrier);
        void DeleteCarrier(Carrier carrier);
    }
}
