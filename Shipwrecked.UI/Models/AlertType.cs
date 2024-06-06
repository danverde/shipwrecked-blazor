using System.ComponentModel.DataAnnotations;

namespace Shipwrecked.UI.Models;

/// <summary>
/// Enum describing the different types of toast notifications available
/// </summary>
public enum AlertType
{
    [Display(Name = "Success")]
    Success,
    
    [Display(Name = "Warning")]
    Warning,
    
    [Display(Name = "Info")]
    Info,
    
    [Display(Name = "Error")]
    Error
}