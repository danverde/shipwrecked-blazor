namespace Shipwrecked.Domain.Models;

public class Map
{
    public List<Location> Locations { get; set; }
    
    public bool EnableFog { get; set; }
}