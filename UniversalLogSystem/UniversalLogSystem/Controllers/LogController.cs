using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversalLogSystem.Data;
using UniversalLogSystem.Models;

namespace UniversalLogSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/Log")]
    public class LogController : Controller
    {
        private readonly LogContext _context;

        public LogController(LogContext context)
        {
            _context = context;
        }

        // GET: api/Log
        [HttpGet]
        public IEnumerable<LogEntity> GetLogEntity()
        {
            return _context.LogEntity;
        }

        // GET: api/Log/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogEntity([FromRoute] string id, string sortBy, string filterBy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //string sortBy = HttpContext.Request.Query["sortBy"];
            //string filterBy = HttpContext.Request.Query["filterBy"];

            if (sortBy != null)
            {
                if (sortBy.ToLower().Equals("date"))
                {
                    var logEntityBySort = _context.LogEntity.Where(m => m.UserId == id).OrderBy(s => s.LoggingDate);

                    if (logEntityBySort == null)
                    {
                        return NotFound();
                    }

                    return Ok(logEntityBySort);
                }
            }

            if(filterBy != null)
            {
                int level = Int32.Parse(filterBy);
                //string s = "Error";
                //Enum.Parse<LogLevel>(s);
                //LogLevel level = (LogLevel)Int32.Parse(filterBy);
                var logEntityBySort = _context.LogEntity.Where(m => m.UserId == id && (int)m.Level == level);

                    if (logEntityBySort == null)
                    {
                        return NotFound();
                    }

                    return Ok(logEntityBySort);
            }

            var logEntity = _context.LogEntity.Where(m => m.UserId == id);

            return Ok(logEntity);
        }

        [HttpGet("{id}/GetReport", Name = "GetReport")]
       public async Task<IActionResult> GetReport([FromRoute]string id)
      {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string from = HttpContext.Request.Query["from"];
            string to = HttpContext.Request.Query["to"];

            CultureInfo provider = CultureInfo.InvariantCulture;

            if (from != null && to != null)
            {
                DateTime fromDate = DateTime.ParseExact(from, "MM/dd/yyyy", provider);
                DateTime toDate = DateTime.ParseExact(to, "MM/dd/yyyy", provider);

                var logEntity = _context.LogEntity.Where(m => m.UserId == id && m.LoggingDate >= fromDate && m.LoggingDate <= toDate);

                if (logEntity == null)
                {
                    return NotFound();
                }

                ReportResponse report = new ReportResponse();
                report.UserId = id;
                report.TotalLogCount = logEntity.Count();

                foreach (var log in logEntity)
                {
                    if (log.Level == LogLevel.Fatal)
                    {
                        report.LogCountForFatalLevel++;
                    }
                    else if (log.Level == LogLevel.Error)
                    {
                        report.LogCountForErrorLevel++;
                    }
                    else if (log.Level == LogLevel.Warning)
                    {
                        report.LogCountForWarningLevel++;
                    }
                    else if (log.Level == LogLevel.Information)
                    {
                        report.LogCountForInformationLevel++;
                    }
                    else if (log.Level == LogLevel.Debug)
                    {
                        report.LogCountForDebugLevel++;
                    }
                    else if (log.Level == LogLevel.Trace)
                    {
                        report.LogCountForTraceLevel++;
                    }

                }

                return Ok(report);
            }
            else
            {
                var logEntity = _context.LogEntity.Where(m => m.UserId == id);

                if (logEntity == null)
                {
                    return NotFound();
                }

                ReportResponse report = new ReportResponse();
                report.UserId = id;
                report.TotalLogCount = logEntity.Count();

                foreach (var log in logEntity)
                {
                    if (log.Level == LogLevel.Fatal)
                    {
                        report.LogCountForFatalLevel++;
                    }
                    else if (log.Level == LogLevel.Error)
                    {
                        report.LogCountForErrorLevel++;
                    }
                    else if (log.Level == LogLevel.Warning)
                    {
                        report.LogCountForWarningLevel++;
                    }
                    else if (log.Level == LogLevel.Information)
                    {
                        report.LogCountForInformationLevel++;
                    }
                    else if (log.Level == LogLevel.Debug)
                    {
                        report.LogCountForDebugLevel++;
                    }
                    else if (log.Level == LogLevel.Trace)
                    {
                        report.LogCountForTraceLevel++;
                    }

                }

                return Ok(report);
            }
    }

    // POST: api/Log
    [HttpPost]
        public async Task<IActionResult> PostLogEntity([FromBody] LogEntity logEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LogEntity.Add(logEntity);
            await _context.SaveChangesAsync();

            return Json(logEntity);
        }



    }
}