namespace Shamazon.Interfaces;

/// <summary>
/// Represents an image loader for the Shamazon system.
/// </summary>
public interface IImageLoader
{
    /// <summary>
    /// Loads an image from the given URL.
    /// </summary>
    /// <param name="url">The URL containing the image.</param>
    /// <returns>The byte array of the image data.</returns>
    Task<byte[]> LoadImageAsync(string? url);
}