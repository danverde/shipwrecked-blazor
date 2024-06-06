using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Services;

/// <summary>
/// Service used to manage toast messages
/// </summary>
public interface IAlertService : IDisposable
{
    /// <summary>
    /// Event used to trigger changes to the alerts from an external source
    /// </summary>
    public event Action OnChange;

    /// <summary>
    /// Get all current alerts
    /// </summary>
    public List<Alert> GetAlerts();
    
    /// <summary>
    /// Create a new informational toast
    /// </summary>
    public void Info(string message);

    /// <summary>
    /// Create a new Success toast
    /// </summary>
    public void Success(string message);

    /// <summary>
    /// Create a new Warning toast
    /// </summary>
    public void Warn(string message);

    /// <summary>
    /// Create a new Error toast
    /// </summary>
    public void Error(string message);

    /// <summary>
    /// Delete a toast notification
    /// </summary>
    public void Delete(Alert alert);

    /// <summary>
    /// Delete the timer when deleting the service
    /// </summary>
    public void Dispose();
}