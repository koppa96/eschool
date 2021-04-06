using System;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Outbox.EntityFrameworkCore
{
    public interface IOutboxDbContext
    {
        DbSet<OutboxEntry> OutboxEntries { get; set; }
    }
}