using Shamazon.Models;

namespace Shamazon.Dto;

public static class ProductDtoExtensions
{
    public static Dimension FromDimensionDto(this DimensionWrapper wrapper)
    {
        return new Dimension
        {
            Height = wrapper.Height,
            Width = wrapper.Width,
            Depth = wrapper.Depth
        };
    }
    
    public static ProductMetadata FromMetadataDto(this ProductMetadataWrapper wrapper)
    {
        return new ProductMetadata
        {
            CreatedAt = wrapper.CreatedAt,
            UpdatedAt = wrapper.UpdatedAt,
            Barcode = wrapper.Barcode,
            QrCodeUrl = wrapper.QrCodeUrl
        };
    }
    
    public static Review FromReviewDto(this ReviewJsonWrapper wrapper)
    {
        return new Review
        {
            Rating = wrapper.Rating,
            Comment = wrapper.Comment,
            Date = wrapper.Date,
            ReviewerName = wrapper.ReviewerName,
            ReviewerEmail = wrapper.ReviewerEmail
        };
    }
    
    // Extension method that maps a ProductJsonWrapper to a Product object.
    public static Product FromProductDto(this ProductJsonWrapper wrapper)
    {
        return new Product
        {
            Id = wrapper.Id,
            Title = wrapper.Title,
            Description = wrapper.Description,
            Category = wrapper.Category,
            Price = wrapper.Price,
            DiscountPercentage = wrapper.DiscountPercentage,
            Rating = wrapper.Rating,
            Stock = wrapper.Stock,
            Tags = wrapper.Tags,
            Brand = wrapper.Brand,
            Sku = wrapper.Sku,
            Weight = wrapper.Weight,
            Dimensions = wrapper.Dimensions.FromDimensionDto(),
            WarrantyInformation = wrapper.WarrantyInformation,
            ShippingInformation = wrapper.ShippingInformation,
            AvailabilityStatus = wrapper.AvailabilityStatus,
            Reviews = wrapper.Reviews!.Select(x => x.FromReviewDto()).ToList(),
            ReturnPolicy = wrapper.ReturnPolicy,
            MinimumOrderQuantity = wrapper.MinimumOrderQuantity,
            Metadata = wrapper.Metadata.FromMetadataDto(),
            ImageUrls = wrapper.ImageUrls,
            ThumbnailUrl = wrapper.ThumbnailUrl
        };
    }
}