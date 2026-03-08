/* SPDX-License-Identifier: MIT
 * Copyright (c) 2026 Florian Wilhelm
 * Description: Entity Framework Core database context for the server.
 */
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets should be added here. Example:
        // public DbSet<FirstTable> Table { get; set; }
    }
}
