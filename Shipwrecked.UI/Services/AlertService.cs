using System.Timers;
using Ardalis.GuardClauses;
using Shipwrecked.UI.Models;
using Timer = System.Timers.Timer;

namespace Shipwrecked.UI.Services;

/// <summary>
/// Service used to manage toast messages
/// </summary>
public class AlertService : IDisposable
{
    private const int Duration = 5000;
    private readonly Timer _timer;

    /// <summary>
    /// Public constructor configuring the timer
    /// </summary>
    public AlertService()
    {
        _timer = new Timer(Duration);
        _timer.Elapsed += RemoveExpiredAlerts;
        _timer.AutoReset = true;
        _timer.Stop();
    }
    
    public List<Alert> Alerts { get; } = new();

    public event Action OnChange = default!;

    /// <summary>
    /// Create a new informational toast
    /// </summary>
    public void Info(string message)
    {
        Guard.Against.NullOrWhiteSpace(message);
        Create(message, AlertType.Info);
    }

    /// <summary>
    /// Create a new Success toast
    /// </summary>
    public void Success(string message)
    {
        Guard.Against.NullOrWhiteSpace(message);
        Create(message, AlertType.Success);
    }
    
    /// <summary>
    /// Create a new Warning toast
    /// </summary>
    public void Warn(string message)
    {
        Guard.Against.NullOrWhiteSpace(message);
        Create(message, AlertType.Warning);
    }
    
    /// <summary>
    /// Create a new Error toast
    /// </summary>
    public void Error(string message)
    {
        Guard.Against.NullOrWhiteSpace(message);
        Create(message, AlertType.Error);
    }
    
    /// <summary>
    /// Delete a toast notification
    /// </summary>
    public void Delete(Alert alert)
    {
        Guard.Against.Null(alert);
        
        Alerts.Remove(alert);
        
        if (Alerts.Count == 0)
            _timer.Stop();
        
        OnChange.Invoke();
    }
    
    /// <summary>
    /// Delete the timer when deleting the service
    /// </summary>
    public void Dispose()
    {
        _timer.Dispose();
    }

    #region Private Methods

    /// <summary>
    /// Generate and render a new toast notification
    /// </summary>
    private void Create(string message, AlertType type)
    {
        Guard.Against.NullOrWhiteSpace(message);
        
        var alert = new Alert
        {
            Message = message,
            Type = type
        };
        
        Alerts.Add(alert);
        
        if (!_timer.Enabled)
            _timer.Start();
            
        OnChange.Invoke();
    }
    
    /// <summary>
    /// CB used to handle the timer elapsed event
    /// </summary>
    private void RemoveExpiredAlerts(object? src, ElapsedEventArgs args)
    {
        foreach (var alert in Alerts)
        {
            if (DateTime.Now - alert.CreatedOn >= TimeSpan.FromMilliseconds(Duration))
            {
                Delete(alert);
            }
        }
    }

    #endregion
    
}