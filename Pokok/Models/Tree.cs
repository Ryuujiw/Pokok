using System;

namespace Pokok.Models
{
    public class Tree
    {
        public Guid Id { get; set; }
        public Location Location { get; set; }
        public string Species { get; set; }

        public Tree(double latitude, double longitude, string species)
        {
            Id = Guid.NewGuid();
            Location = new Location(latitude, longitude);
            Species = species;
        }

        public Tree(Guid id, double latitude, double longitude, string species)
        {
            Id = id;
            Location = new Location(latitude, longitude);
            Species = species;
        }

        public Tree() {}
    }
}
