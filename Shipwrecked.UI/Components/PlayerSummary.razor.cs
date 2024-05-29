using Fluxor;
using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Store.Player;

namespace Shipwrecked.UI.Components;

public partial class PlayerSummary
{
    [Inject] private IState<PlayerState> PlayerState { get; set; } = default!;
}