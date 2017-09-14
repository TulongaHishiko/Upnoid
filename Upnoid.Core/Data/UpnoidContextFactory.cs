using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Upnoid.Core.Configurations;

namespace Upnoid.Core.Data
{
    public class UpnoidContextFactory : IDesignTimeDbContextFactory<UpnoidContext>
    {
        public UpnoidContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UpnoidContext>();
            builder.UseSqlServer(DatabaseSettings.LocalConnection, option => option.EnableRetryOnFailure());
            return new UpnoidContext(builder.Options);
        }
    }
}
