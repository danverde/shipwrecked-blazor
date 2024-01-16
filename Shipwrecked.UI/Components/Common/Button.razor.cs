using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Components.Common;

/// <summary>
/// Code-behind class for the Button component
/// </summary>
public partial class Button
{
    /// <summary>
    /// The navigation manager used to navigate to different pages
    /// </summary>
    [Inject]
    private NavigationManager NavManager { get; set; } = default!;
    
    #region Parameters
    
    /// <summary>
    /// Text content of the button
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string Styles { get; set; } = "";
    
    /// <summary>
    /// Corresponds with the HTML button type attribute
    /// </summary>
    [Parameter] public string Type { get; set; } = "button";
    
    /// <summary>
    /// Corresponds with the HTML button value attribute
    /// </summary>
    [Parameter] public string Value { get; set; } = "";
    
    /// <summary>
    /// Corresponds with the Form button attribute 
    /// </summary>
    [Parameter] public string FormId { get; set; } = "";
    
    /// <summary>
    /// Disables the button
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// The type of the button. Changes the styling
    /// </summary>
    [Parameter] public ButtonType Variant { get; set; } = ButtonType.Primary;
    
    /// <summary>
    /// The location to navigate to on button click
    /// </summary>
    [Parameter] public string? Route { get; set; }

    /// <summary>
    /// Callback letting parent component know the onClick event has been triggered
    /// </summary>
    [Parameter] public EventCallback OnClickCallback { get; set; }

    #endregion

    /// <summary>
    /// Handle the button's onClick event
    /// </summary>
    private void HandleClick()
    {
        OnClickCallback.InvokeAsync();
        
        if (!string.IsNullOrWhiteSpace(Route))
        {
            NavManager.NavigateTo(Route);
        }
    }
}