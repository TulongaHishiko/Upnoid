using System;
using System.Collections.Generic;
using System.Text;

namespace Upnoid.Core.Configurations
{
    public static class DatabaseSettings
    {
        internal static string LocalConnection => "Server=(localdb)\\mssqllocaldb;Database=UpnoidDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static string  ServerConnection => "Data Source=SQL6003.SmarterASP.NET;Initial Catalog=DB_A271F3_cenoredtic;User Id=DB_A271F3_cenoredtic_admin;Password=cenoredticdb1;";
    }
}
