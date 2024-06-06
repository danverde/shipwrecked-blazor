namespace Shipwrecked.UI.Models;

/// <summary>
/// Alert obj used to create toasts
/// </summary>
public class Alert
{
    public string Message { get; set; } = "";
    public AlertType Type { get; set; } = AlertType.Info;
    public DateTime CreatedOn = DateTime.Now;
    public string AlertTypeString => Type.ToString().ToLower();
}