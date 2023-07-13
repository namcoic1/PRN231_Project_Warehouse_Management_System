using BusinessObjects;
using DataAccess;

namespace Repositories.ReportRepo
{
    public class ReportRepository : IReportRepository
    {
        public void SaveReport(Report report) => ReportDAO.GetInstance.SaveReport(report);

        public void DeleteReport(Report report) => ReportDAO.GetInstance.DeleteReport(report);

        public Report GetReportById(int id) => ReportDAO.GetInstance.GetReportById(id);

        public Report GetReportByLastId() => ReportDAO.GetInstance.GetReportByLastId();

        public List<Report> GetReports() => ReportDAO.GetInstance.GetReports();

        public void UpdateReport(Report report) => ReportDAO.GetInstance.UpdateReport(report);
    }
}
