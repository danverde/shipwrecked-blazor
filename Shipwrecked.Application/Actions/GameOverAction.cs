using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Application.Actions;

/// <summary>
/// Action triggered when the game ends.
/// </summary>
/// <remarks>
/// TODO this is currently used to reset the state, same as quitGameAction, but IT SHOULD NOT!
/// This should trigger a modal which forces the user to trigger the quit game action!
/// </remarks>
[ExcludeFromCodeCoverage]
public class GameOverAction;