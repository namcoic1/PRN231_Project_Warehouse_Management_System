using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.ReportRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class ReportsController : ODataController
    {
        private readonly IReportRepository repository;
        private IMapper Mapper { get; }

        public ReportsController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new ReportRepository();
        }

        [EnableQuery]
        public ActionResult<List<ReportDTO>> Get()
        {
            return Ok(Mapper.Map<List<ReportDTO>>(repository.GetReports()));
        }
        [EnableQuery]
        public ActionResult<ReportDTO> Get([FromODataUri] int key)
        {
            var report = Mapper.Map<ReportDTO>(repository.GetReportById(key));

            if (report == null)
            {
                return new NotFoundResult();
            }

            return Ok(report);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] ReportRequestDTO reportRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = Mapper.Map<Report>(reportRequest);
            repository.SaveReport(report);

            return Ok(Mapper.Map<ReportRequestDTO>(repository.GetReportByLastId()));
        }
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] ReportRequestDTO reportRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _report = repository.GetReportById(key);
            var report = Mapper.Map<Report>(reportRequest);

            if (_report == null || report.Id != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateReport(report);

            return Ok(Mapper.Map<ReportRequestDTO>(repository.GetReportById(key)));
        }
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var report = repository.GetReportById(key);
            var reportResponse = Mapper.Map<ReportDTO>(report);

            if (report == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteReport(report);

            return Ok(reportResponse);
        }
    }
}
