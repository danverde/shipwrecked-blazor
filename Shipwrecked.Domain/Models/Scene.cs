using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

[ExcludeFromCodeCoverage]
public class Scene
{
    public bool IsTraversable { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}