using BusinessObjects;

namespace Repositories.ReportRepo
{
    public interface IReportRepository
    {
        List<Report> GetReports();
        Report GetReportById(int id);
        void SaveReport(Report report);
        void UpdateReport(Report report);
        void DeleteReport(Report report);
    }
}
