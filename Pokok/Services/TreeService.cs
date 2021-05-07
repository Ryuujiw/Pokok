using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Pokok.DataAccess;
using Pokok.Interfaces;
using Pokok.Models;

namespace Pokok.Services
{
    public class TreeService : ITreeService
    {
        public int CreateTree(string species, double latitude, double longitude)
        {
            Guid id = Guid.NewGuid();

            Tree tree = new Tree(id, latitude, longitude, species);

            string sql = @"insert into dbo.Tree (Id, Species, Latitude, Longitude)
                            values (@Id, @Species, @Latitude, @Longitude);";

            return SqlDataAccess.SaveData(sql, tree);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocationById(int id)
        {
            throw new NotImplementedException();
        }


    }
}
