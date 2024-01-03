using Microsoft.AspNetCore.Components;
using Shipwrecked.Models;

namespace Shipwrecked.Components;

/// <summary>
/// Code-behind class for the Button component
/// </summary>
public partial class Button
{
    /// <summary>
    /// Text content of the button
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; } 
    
    /// <summary>
    /// Disables the button
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// The type of the button. Changes the styling
    /// </summary>
    [Parameter] public ButtonType Type { get; set; } = ButtonType.Primary;
}