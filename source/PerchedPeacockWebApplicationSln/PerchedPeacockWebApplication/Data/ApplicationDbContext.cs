﻿using PerchedPeacockWebApplication.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerchedPeacockWebApplication.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<PerchedPeacockWebApplication.Models.ParkingLot> ParkingLot { get; set; }
        public DbSet<PerchedPeacockWebApplication.Models.Booking> Booking { get; set; }
        public DbSet<PerchedPeacockWebApplication.Models.Location> Location { get; set; }
    }
}
