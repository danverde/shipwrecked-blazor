using FluentAssertions;
using Shared;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Services;

namespace Shipwrecked.UI.Test.Services;

/// <summary>
/// Unit Tests for the <see cref="AlertService"/> class
/// </summary>
public class AlertServiceTests
{
    private readonly IAlertService _alertService;

    public AlertServiceTests()
    {
        _alertService = new AlertService();
    }

    #region Info

    [Fact]
    public void Info_ValidParams_ShouldCreateAnAlert()
    {
        // Arrange
        const string message = "message";
        
        List<Alert> alerts = _alertService.GetAlerts();
        var expectedAlert = new Alert
        {
            Message = message,
            Type = AlertType.Info
        };
        
        // Act
        _alertService.Info(message);
        
        // Assert
        alerts.Should().ContainEquivalentOf(expectedAlert, config => config.Excluding(a => a.CreatedOn));
    }

    [Theory]
    [ClassData(typeof(NullAndWhitespaceStrings))]
    public void Info_NullOrWhitespaceMessage_ShouldThrow(string message)
    {
        // Arrange
        Action act = () => _alertService.Info(message);
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("message");
    }

    #endregion

    #region Success

    [Fact]
    public void Success_ValidParams_ShouldCreateAnAlert()
    {
        // Arrange
        const string message = "message";
        
        List<Alert> alerts = _alertService.GetAlerts();
        var expectedAlert = new Alert
        {
            Message = message,
            Type = AlertType.Success
        };
        
        // Act
        _alertService.Success(message);
        
        // Assert
        alerts.Should().ContainEquivalentOf(expectedAlert, config => config.Excluding(a => a.CreatedOn));
    }
    
    [Theory]
    [ClassData(typeof(NullAndWhitespaceStrings))]
    public void Success_NullOrWhitespaceMessage_ShouldThrow(string message)
    {
        // Arrange
        Action act = () => _alertService.Success(message);
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("message");
    }
    
    #endregion

    #region Warn

    [Fact]
    public void Warn_ValidParams_ShouldCreateAnAlert()
    {
        // Arrange
        const string message = "message";
        
        List<Alert> alerts = _alertService.GetAlerts();
        var expectedAlert = new Alert
        {
            Message = message,
            Type = AlertType.Warning
        };
        
        // Act
        _alertService.Warn(message);
        
        // Assert
        alerts.Should().ContainEquivalentOf(expectedAlert, config => config.Excluding(a => a.CreatedOn));
    }
    
    [Theory]
    [ClassData(typeof(NullAndWhitespaceStrings))]
    public void Warn_NullOrWhitespaceMessage_ShouldThrow(string message)
    {
        // Arrange
        Action act = () => _alertService.Warn(message);
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("message");
    }
    
    #endregion

    #region Error

    [Fact]
    public void Error_ValidParams_ShouldCreateAnAlert()
    {
        // Arrange
        const string message = "message";
        
        List<Alert> alerts = _alertService.GetAlerts();
        var expectedAlert = new Alert
        {
            Message = message,
            Type = AlertType.Error
        };
        
        // Act
        _alertService.Error(message);
        
        // Assert
        alerts.Should().ContainEquivalentOf(expectedAlert, config => config.Excluding(a => a.CreatedOn));
    }
    
    [Theory]
    [ClassData(typeof(NullAndWhitespaceStrings))]
    public void Error_NullOrWhitespaceMessage_ShouldThrow(string message)
    {
        // Arrange
        Action act = () => _alertService.Error(message);
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("message");
    }
    
    #endregion

    #region Delete

    [Fact]
    public void Delete_ValidAlert_ShouldDelete()
    {
        // Arrange
        const string message = "message";
        
        _alertService.Success(message);
        List<Alert> alerts = _alertService.GetAlerts();
        Alert alert = alerts.Single(a => a.Message == message && a.Type == AlertType.Success);
        
        // Act
        _alertService.Delete(alert);

        // Assert
        alerts.Should().BeEmpty();
    }
    
    [Fact]
    public void Delete_NullAlert_ShouldThrow()
    {
        // Arrange
        Action act = () => _alertService.Delete(null!);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("alert");
    }

    #endregion
    
}