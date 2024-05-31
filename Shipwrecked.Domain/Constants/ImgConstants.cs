namespace Shipwrecked.Domain.Constants;

/// <summary>
/// Constants that point to the location of the sprite URL's.
/// </summary>
/// <remarks>
/// TODO Doesn't belong in the domain layer. At all.
/// UI Should use gender & type properties on the domain obj to determine the img 
/// </remarks>
public static class ImgConstants
{
    // Profile Images
    public const string ManProfileImg = "/img/profiles/man.png";
    public const string WomanProfileImg = "/img/profiles/woman.png";
    
    // Sprites
    public const string ManSprite = "/img/sprites/man/man.gif";
    public const string WomanSprite = "/img/sprites/woman/woman.gif";
}