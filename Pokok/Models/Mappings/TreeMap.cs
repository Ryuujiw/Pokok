using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokok.Models.Mappings
{
    internal class TreeMap : EntityMap<Tree>
    {
        internal TreeMap()
        {
            Map(u => u.TreeId).ToColumn("Id");
            Map(u => u.Location.Latitude).ToColumn("Latitude");
            Map(u => u.Location.Longitude).ToColumn("Longitude");
            Map(u => u.Species).ToColumn("Species");
        }
    }
}
