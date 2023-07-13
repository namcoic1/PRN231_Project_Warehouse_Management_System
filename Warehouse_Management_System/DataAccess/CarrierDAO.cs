using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CarrierDAO
    {
        // static object of carrierdao
        private static CarrierDAO _instance = null;
        private static MyDbContext _context = null;
        private CarrierDAO()
        {
        }
        // singleton pattern
        public static CarrierDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CarrierDAO();
                }

                // refresh mydbcontext
                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Carrier> GetCarriers() => _context.Carriers.ToList();
        public Carrier GetCarrierById(int id) => _context.Carriers.SingleOrDefault(c => c.Id == id);
        public Carrier GetCarrierByLastId() => _context.Carriers.OrderBy(c => c.Id).LastOrDefault();

        public void SaveCarrier(Carrier carrier)
        {
            try
            {
                _context.Carriers.Add(carrier);
                _context.SaveChanges();
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
                _context.Entry<Carrier>(carrier).State = EntityState.Modified;
                _context.SaveChanges();
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
                _context.Carriers.Remove(carrier);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
