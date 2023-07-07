using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CarrierDAO
    {
        // static object of carrierdao
        private static CarrierDAO _instance = null;
        private CarrierDAO() { }
        // singleton pattern
        public static CarrierDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CarrierDAO();
            }
            return _instance;
        }

        public List<Carrier> GetCarriers() => MyDbContext.GetInstance().Carriers.ToList();
        public Carrier GetCarrierById(int id) => MyDbContext.GetInstance().Carriers.SingleOrDefault(c => c.ID == id);

        public void SaveCarrier(Carrier carrier)
        {
            try
            {
                MyDbContext.GetInstance().Carriers.Add(carrier);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCarrier(Carrier carrier)
        {
            try
            {
                MyDbContext.GetInstance().Entry<Carrier>(carrier).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCarrier(Carrier carrier)
        {
            try
            {
                MyDbContext.GetInstance().Carriers.Remove(carrier);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
