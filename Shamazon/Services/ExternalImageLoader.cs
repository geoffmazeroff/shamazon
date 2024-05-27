using Shamazon.Interfaces;

namespace Shamazon.Services;

/// <summary>
/// A service for loading images from external URLs.
/// </summary>
public class ExternalImageLoader : IImageLoader 
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ILogger<ExternalImageLoader> _logger;
    
    public ExternalImageLoader(IWebHostEnvironment hostEnvironment, ILogger<ExternalImageLoader> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }
    
    public async Task<byte[]> LoadImageAsync(string? url)
    {
        try
        {
            var client = new HttpClient();
            return await client.GetByteArrayAsync(url);
        }
        catch 
        {
            _logger.LogWarning("Failed to load image from {Url}. Using default image instead", url);
            
            // Load a default image if the image fails to load.
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "defaultProduct.png");
            return await File.ReadAllBytesAsync(imagePath);
        }
    }
}