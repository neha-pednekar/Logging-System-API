using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalLogSystem.Models
{
    public class LogEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Title { get; set; }

        public LogLevel Level { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public DateTime LoggingDate { get; set; }
       
    }


    public enum LogLevel
    {
        Fatal,
        Error,
        Warning,
        Information,
        Debug,
        Trace
    }

}
