using Pokok.Models;
using System;
using System.Collections.Generic;

namespace Pokok.Interfaces
{
    public interface ITreeService
    {
        int CreateTree(Tree tree);
        IEnumerable<Location> GetAllLocations();
        Location GetLocationById(Guid id);
    }
}
