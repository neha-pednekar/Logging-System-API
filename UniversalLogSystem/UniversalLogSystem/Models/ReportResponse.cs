using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalLogSystem.Models
{
    public class ReportResponse
    {
        public string UserId { get; set; }

        public int TotalLogCount { get; set; }

        public int LogCountForFatalLevel { get; set; }

        public int LogCountForErrorLevel { get; set; }

        public int LogCountForWarningLevel { get; set; }

        public int LogCountForInformationLevel { get; set; }

        public int LogCountForDebugLevel { get; set; }

        public int LogCountForTraceLevel { get; set; }
    }
}
