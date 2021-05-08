using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Pokok.DataAccess;
using Pokok.Models;

namespace Pokok.Services
{
    public class TreeService
    {
        public SqlDataAccess DataAccess { get; set; }

        public TreeService(string connectionString)
        {
            DataAccess = new SqlDataAccess(connectionString);
        }

        public int CreateTree(string species, double latitude, double longitude)
        {
            Guid id = Guid.NewGuid();

            Tree tree = new Tree(id, latitude, longitude, species);

            string sql = @"insert into dbo.Tree (Id, Species, Latitude, Longitude)
                            values (@Id, @Species, @Latitude, @Longitude);";

            return DataAccess.SaveData(sql, tree);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            string sql = @"select latitude, longitude from dbo.Tree";

            return DataAccess.LoadData<Location>(sql);
        }

        public Location GetLocationById(Guid id)
        {
            string sql = @"select latitude, longitude from dbo.Tree where Id = @Id";

            return DataAccess.LoadData<Location>(sql);
        }


    }
}
