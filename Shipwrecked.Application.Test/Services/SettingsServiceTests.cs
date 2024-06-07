using FluentAssertions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;

namespace Shipwrecked.Application.Test.Services;

/// <summary>
/// Unit tests for the <see cref="SettingsService"/> class
/// </summary>
public class SettingsServiceTests
{
    private readonly ISettingsService _settingsService;

    public SettingsServiceTests()
    {
        _settingsService = new SettingsService();
    }
    
    [Theory]
    [InlineData(GameDifficulty.Normal)]
    [InlineData(GameDifficulty.Easy)]
    [InlineData(GameDifficulty.Difficult)]
    public void Create_ShouldReturnGameSettings(GameDifficulty difficulty)
    {
        // Act
        var result = _settingsService.GetSettings(difficulty);
        
        // Assert
        result.Should().NotBeNull();

    }
}