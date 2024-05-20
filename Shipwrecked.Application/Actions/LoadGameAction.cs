using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Application.Actions;

/// <summary>
/// Action triggered when starting to load a game from storage
/// </summary>
[ExcludeFromCodeCoverage]
public class LoadGameAction(Guid id)
{
    public Guid Id { get; set; } = id;
}