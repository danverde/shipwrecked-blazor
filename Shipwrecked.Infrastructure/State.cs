using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure;

public class State
{
    public Game Game { get; set; }

    public Map Map { get; set; }
    
    public Player Player { get; set; }

    public Inventory Inventory { get; set; }
}