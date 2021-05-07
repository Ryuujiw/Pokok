using System.Collections.Generic;
using Pokok.Models;

namespace Pokok.Interfaces
{
    public interface ITreeService
    {
        IEnumerable<Location> GetAllLocations();
        IEnumerable<Location> GetLocationById(int id);
        int CreateTree(string name, double latitude, double longitude);
    }
}
