using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Starbender.Romi.WebApi.Data
{
    using Starbender.Romi.Data;

    public class ApplicationDbContext : RomiDbContext
    {
        public ApplicationDbContext(DbContextOptions<RomiDbContext> options)
            : base(options)
        {
        }
    }
}
