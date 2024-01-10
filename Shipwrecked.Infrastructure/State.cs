using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure;

/// <summary>
/// Stores the state of the entire application
/// (seems like a bad idea, right?)
/// </summary>
public static class State
{
    // TODO there is nothing stopping views from changing these directly w/out calling the services...
    public static Game Game { get; set; } = new Game();

    public static Map Map { get; set; } = new Map();
    
    public static Player Player { get; set; } = new Player();

    public static Inventory Inventory { get; set; } = new Inventory();

}