using System;

namespace Pokok.Models
{
    public class Tree
    {
        public Guid TreeId { get; set; }
        public Location Location { get; set; }
        public string Species { get; set; }

        public Tree(double latitude, double longitude, string species)
        {
            TreeId = Guid.NewGuid();
            Location = new Location(latitude, longitude);
            Species = species;
        }

        public Tree(Guid treeId, double latitude, double longitude, string species)
        {
            TreeId = treeId;
            Location = new Location(latitude, longitude);
            Species = species;
        }
    }
}
