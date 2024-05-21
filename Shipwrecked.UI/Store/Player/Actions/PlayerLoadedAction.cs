using System.Diagnostics.CodeAnalysis;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Player.Actions;

[ExcludeFromCodeCoverage]
public class PlayerLoadedAction
{
    public D.Player Player { get; set; } = default!;
}