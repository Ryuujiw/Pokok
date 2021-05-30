using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Hangfire;
using Pokok.DataAccess;
using Pokok.Interfaces;
using Pokok.Models;

namespace Pokok.Services
{
    public class TreeService : ITreeService
    {
        public readonly IDataAccess _dataAccess;

        public TreeService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public int CreateTree(Tree tree)
        {
            string sql = @"insert into dbo.Tree (Id, Species, Latitude, Longitude, Weight)
                            values (@Id, @Species, @Latitude, @Longitude, @Weight);";

            int rowsAdded = _dataAccess.SaveData(sql, new { tree.Id, tree.Species, tree.Location.Latitude, tree.Location.Longitude, tree.Location.Weight });

            UpdateWeight(tree);

            return rowsAdded;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            string sql = @"select latitude, longitude, weight from dbo.Tree";

            return _dataAccess.LoadData<Location>(sql);
        }

        public Location GetLocationById(Guid id)
        {
            string sql = @"select latitude, longitude from dbo.Tree where Id = @Id";

            // Initialize parameters
            var dbArgs = new DynamicParameters();
            dbArgs.Add("Id", id);

            return _dataAccess.LoadData<Location>(sql, dbArgs).First();
        }

        public bool UpdateWeight(Tree tree)
        {
            /**
             * The cosine law is: d = acos( sin(φ1)⋅sin(φ2) + cos(φ1)⋅cos(φ2)⋅cos(Δλ) ) ⋅ R
             * where:	φ = latitude, λ = longitude (in radians)
             * R = radius of earth
             * d = distance between the points (in same units as R)

             * 1. Query all the trees within 500m radius
             * 2. Add weight to trees (assume constant for now)
             */

            const int earthRadius = 6371;
            const double boundingRadius = 0.5;

            double maxLat = tree.Location.Latitude + RadianToDegree(boundingRadius/earthRadius);
            double minLat = tree.Location.Latitude - RadianToDegree(boundingRadius/earthRadius);
            double maxLng = tree.Location.Longitude + RadianToDegree(Math.Asin(boundingRadius/earthRadius)) / Math.Cos(DegreeToRadian(tree.Location.Latitude));
            double minLng = tree.Location.Longitude - RadianToDegree(Math.Asin(boundingRadius/earthRadius)) / Math.Cos(DegreeToRadian(tree.Location.Latitude));

            UpdateCurrentTree(tree.Id, maxLat, minLat, maxLng, minLng);
            UpdateSurroundingTrees(tree.Id, maxLat, minLat, maxLng, minLng);

            return true;
        }

        private void UpdateSurroundingTrees(Guid id, double maxLat, double minLat, double maxLng, double minLng)
        {
            string sql = @"update dbo.Tree set Weight = Weight + 0.1
                           where (Latitude <= @maxLat and Latitude >= @minLat) and 
                                 (Longitude <= @maxLng and Longitude >= @minLng) and 
                                 Id <> @Id";

            var dbArgs = new DynamicParameters();
            dbArgs.Add("Id", id);
            dbArgs.Add("maxLat", maxLat);
            dbArgs.Add("minLat", minLat);
            dbArgs.Add("maxLng", maxLng);
            dbArgs.Add("minLng", minLng);

            _dataAccess.SaveData<Tree>(sql, dbArgs);
        }

        private void UpdateCurrentTree(Guid id, double maxLat, double minLat, double maxLng, double minLng)
        {
            string sql = @"update A set A.weight = case when b.weight = 0 then 0.1 else b.weight end
                           from dbo.Tree A
                           CROSS JOIN
                           (
                            select avg(weight) weight from tree where 
                            (Latitude <= @maxLat and Latitude >= @minLat) and 
                            (Longitude <= @maxLng and Longitude >= @minLng)
                           ) B where A.Id = @Id";

            var dbArgs = new DynamicParameters();
            dbArgs.Add("Id", id);
            dbArgs.Add("maxLat", maxLat);
            dbArgs.Add("minLat", minLat);
            dbArgs.Add("maxLng", maxLng);
            dbArgs.Add("minLng", minLng);

            _dataAccess.SaveData<Tree>(sql, dbArgs);
        }

        private double RadianToDegree(double value)
        {
            return value * 180 / Math.PI;
        }

        private double DegreeToRadian(double value)
        {
            return value * Math.PI / 180;
        }
    }
}
