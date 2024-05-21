namespace Shipwrecked.UI.Store;

/// <summary>
/// Basic state props shared among all slices of state
/// </summary>
public class BaseState
{
    public bool Loading { get; set; }
    public bool Loaded { get; set; }
}