using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ReportDAO
    {
        private static ReportDAO _instance = null;
        private ReportDAO() { }
        public static ReportDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReportDAO();
            }
            return _instance;
        }

        public List<Report> GetReports() => MyDbContext.GetInstance().Reports.ToList();
        public Report GetReportById(int id) => MyDbContext.GetInstance().Reports.SingleOrDefault(c => c.ID == id);

        public void SaveReport(Report report)
        {
            try
            {
                MyDbContext.GetInstance().Reports.Add(report);
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Entry<Report>(report).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Reports.Remove(report);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
