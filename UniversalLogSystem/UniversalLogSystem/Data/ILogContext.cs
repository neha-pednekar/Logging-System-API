using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversalLogSystem.Models;

namespace UniversalLogSystem.Data
{
    public interface ILogContext
    {
        DbSet<LogEntity> LogEntity { get; }
    }
}
