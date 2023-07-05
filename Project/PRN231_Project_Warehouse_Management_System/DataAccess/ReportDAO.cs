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

        public List<Report> GetReports() => MyDB_Context.GetInstance().Reports.ToList();
        public Report GetReportById(int id) => MyDB_Context.GetInstance().Reports.SingleOrDefault(c => c.ReportID == id);

        public void SaveReport(Report report)
        {
            try
            {
                MyDB_Context.GetInstance().Reports.Add(report);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Report>(report).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Reports.Remove(report);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
