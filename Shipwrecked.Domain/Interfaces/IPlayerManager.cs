using Shipwrecked.Domain.Models;

namespace Shipwrecked.Domain.Interfaces;

/// <summary>
/// Interface for altering player objects
/// </summary>
public interface IPlayerManager
{
    /// <summary>
    /// Decrease a players' stamina.
    /// </summary>
    /// <remarks>Amount is based off difficulty settings</remarks>
    public Player DecreaseStamina(Player player, Settings settings);
    
    /// <summary>
    /// Increase a players exp. Amount is based off
    /// </summary>
    /// <remarks>Amount is based off difficulty settings</remarks>
    public Player IncreaseExp(Player player, Settings settings);
}