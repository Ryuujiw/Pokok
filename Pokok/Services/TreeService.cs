using System;
using System.Collections.Generic;
using System.Linq;
using Hangfire;
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

        public int CreateTree(Tree tree)
        {
            string sql = @"insert into dbo.Tree (Id, Species, Latitude, Longitude)
                            values (@TreeId, @Species, @Latitude, @Longitude);";

            int rowsAffected = DataAccess.SaveData(sql, new { tree.TreeId, tree.Species, tree.Location.Latitude, tree.Location.Longitude });

            BackgroundJob.Enqueue(() => UpdateWeight(tree.TreeId));

            return rowsAffected;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            string sql = @"select latitude, longitude, weight from dbo.Tree";

            return DataAccess.LoadData<Location>(sql);
        }

        public Location GetLocationById(Guid id)
        {
            string sql = @"select latitude, longitude from dbo.Tree where Id = @Id";

            return DataAccess.LoadData<Location>(sql).First();
        }

        public void UpdateWeight(Guid id)
        {
            Console.WriteLine("Updating weight");
            /**
             * 1. Query all the trees within 500m radius
             * 2. Add weight to trees (assume constant for now)
             */
        }


    }
}
