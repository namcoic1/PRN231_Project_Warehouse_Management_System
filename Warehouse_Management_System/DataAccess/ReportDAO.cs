using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ReportDAO
    {
        private static ReportDAO _instance = null;
        private static MyDbContext _context = null;
        private ReportDAO()
        {
        }
        public static ReportDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReportDAO();
                }
                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Report> GetReports() => _context.Reports.Include(c => c.User).Include(c => c.Transaction)
            //.ThenInclude(c => c.Customer)
            //.Include(c => c.Transaction).ThenInclude(c => c.Carrier)
            //.Include(c => c.Transaction).ThenInclude(c => c.Supplier)
            //.Include(c => c.Transaction).ThenInclude(c => c.Location)
            //.Include(c => c.Transaction).ThenInclude(c => c.Product)
            .ToList();
        public Report GetReportById(int id) => _context.Reports.Include(c => c.User).Include(c => c.Transaction)
            .SingleOrDefault(c => c.Id == id);
        public Report GetReportByLastId() => _context.Reports.Include(c => c.User).Include(c => c.Transaction)
            .OrderBy(c => c.Id).LastOrDefault();

        public void SaveReport(Report report)
        {
            try
            {
                _context.Reports.Add(report);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateReport(Report report)
        {
            try
            {
                _context.Entry<Report>(report).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteReport(Report report)
        {
            try
            {
                _context.Reports.Remove(report);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
