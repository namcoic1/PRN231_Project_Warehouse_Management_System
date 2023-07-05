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

        public List<Carrier> GetCarriers() => MyDB_Context.GetInstance().Carriers.ToList();
        public Carrier GetCarrierById(int id) => MyDB_Context.GetInstance().Carriers.SingleOrDefault(c => c.CarrierID == id);

        public void SaveCarrier(Carrier carrier)
        {
            try
            {
                MyDB_Context.GetInstance().Carriers.Add(carrier);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Carrier>(carrier).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Carriers.Remove(carrier);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
