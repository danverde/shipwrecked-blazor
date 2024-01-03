namespace Shipwrecked.Models;

public class DrawerArgs : EventArgs
{
    public DrawerArgs(DrawerId drawerId, bool isOpen)
    {
        Id = drawerId;
        IsOpen = isOpen;
    }

    // public string Message { get; set; }
    
    public DrawerId Id { get; set; }
    public bool IsOpen { get; set; }
}