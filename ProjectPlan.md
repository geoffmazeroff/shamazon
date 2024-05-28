# Project Plan

## To Do
- Sort header returns by rating; add text on page to mention this
- Address any compiler warnings or other IDE suggestions
- Add error handling 
  - Handle missing field values on product details page
  - Page models need try-catches around service calls
  - Decide on approach and implement error handling from the backend (alerts, toast notifications)
  - In C# code
  - Decide on approach and implement error handling / validation for the 3rd-party API
  - Add more information logging messages
- Add unit tests
- Check Lighthouse for accessibility score (508 compliance)
- Figure out how to change injected repository service based on environment
- Create content for README.md so that others can run this app locally (test on another machine that this indeed works)

## Future Items
- Pagination for main page
- Sort by {ColumnName}
- Search by fields other than title and description
- Currency setting (i.e., assume raw data is in USD then allow conversion to other currencies such as DKK)
- Better UX
- Deploy to Azure (manually)
- Add a CI/CD pipeline (ideas: containerize, generate Software Bill of Materials (SBOM))
- Add data cleaning on 3rd-party API data ingestion (e.g., `16GB` vs `16 GB`)
- There are some business rules the UI could show (e.g., 5 in stock combined with a minimum buy of 24 means the user can't do anything)
- Create Playwright tests
- It would be useful if the 3rd-party had more granular endpoints (e.g., one to get the headers for a product and another to get the details)
- Add an actual cache (e.g., Redis) for caching results from 3rd-party API (30 second expiry); used meta.updatedAt to determine when to refresh cache

## Completed
- Explore data returned from https://dummyjson.com/products
  - Verify lookup with `/products/{id}`
  - Verify search with `/products/search?q={searchString}`
- Decide what kind of ASP.NET Core UI to use
  - Intentionally avoiding Angular to keep things simple.
  - https://learn.microsoft.com/en-us/aspnet/core/tutorials/choose-web-ui?view=aspnetcore-8.0
  - Choosing **Razor pages**
    - I've never used it before.
    - MVC is fairly standard, and Blazor seems like too much for this simple learning project.
  - Confirmed I can "File > New Project" in JetBrains Rider to get a template Razor project created and running in the browser.
- Use ChatGPT to explore names for this project
  - `I'm building a demo website that lists products for sale. Can you help me find a funny name for this website? What about some plays on Amazon.com where the goods are of suspicious origin.`
- Create a GitHub repo with an MIT license
- "File > New Project" a Razor page template project
- Start a `CHANGELOG.md` file
- Create data models for items returned from 3rd-party API
- Implement a logger service (log to console for now)
- Create a response model for the listing page
- Build backend service to return hard-coded list for the index page
- Create mock data service to generate full product data models
- MVP details page should display all fields for a single product
- Create image loader service
- Modify mock product repo to take a constant JSON string with 2 products and convert that into two product models
- Map the JSON wrapper instance to the Product model the UI binds to
- Build out service that communicates with 3rd-party API
- Improve format of index page (thumbnails use Bootstrap class and are clickable)
- Add navbar with search functionality to index page
- Add cache duration of 30 seconds for product data to support faster load times
- Implement search functionality 
- Set up a Bootstrap carousel for the product images
- Fix the carousel image size (too large)
- Improve the visual layout of the product details page
- Improve UX for when search returns no results
- Handle product ID being missing from query string
- Handle product ID not being found in the product list