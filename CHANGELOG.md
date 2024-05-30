# CHANGELOG

### [0.15.0] - 2024-05-29
### Added
- Unit tests for external product repository and Razor page models

### [0.14.0] - 2024-05-29
### Added
- Infrastructure for unit tests and instructions on running them in the README

### [0.13.0] - 2024-05-29
### Added
- Instructions on running the app locally in the README file

### Changed
- Product repository is based on `ASPNETCORE_ENVIRONMENT` value

### [0.12.0] - 2024-05-28
### Changed
- Improved error handling and logging

### [0.11.0] - 2024-05-28
### Deprecated
- Privacy policy page is no longer available

### Fixed
- Addressed compiler warnings and IDE suggestions for code changes

### [0.10.0] - 2024-05-28
### Changed
- Product listing is sorted by product rating

### [0.9.0] - 2024-05-28
### Added
- Alert if no results were returned from search

### Fixed
- User sees an appropriate message when no search results are found or if the ID is missing from the query string

### [0.8.0] - 2024-05-28
### Changed
- Improved layout of the product details page

### Fixed
- Carousel size reduced to fit better within the product details page

### [0.7.0] - 2024-05-27
### Added
- Cache duration of 30 seconds for product data
- Support for searching by description on the main product page
- Image carousel on the product details page

## [0.6.0] - 2024-05-27
### Changed
- Product listing is based on 3rd-party API data
- Product details page uses data from 3rd-party API

## [0.5.0] - 2024-05-27
### Added
- Mock data produced by hard-coded JSON parsing for a single product
- Separate DTOs for product components represented in JSON
- DTO extensions to convert from DTO to bound data models

## [0.4.0] - 2024-05-27
### Added
- Product details page to display all fields for a single product
- Mock data service to generate product details

### Changed
- Index page now displays product details when a product is clicked

## [0.3.0] - 2024-05-25
### Added
- Mock data service to generate index listing view models

## [0.2.0] - 2024-05-25
### Added
- Console logger service
- Data models for products returned from 3rd-party API

## [0.1.0] - 2024-05-25
### Added
- Initial template project for ASP.NET Code Razor site