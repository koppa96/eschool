using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Configuration
{
    public class SqlServerOutboxConfiguration
    {
        public Action<SqlServerDbContextOptionsBuilder> ServerConfig { get; set; }
    }
}