# Project Plan

## To Do
- MVP details page should display all fields for a single product
- Create mock data service to generate full product data models
- Improved details page using Bootstrap
- Figure out how to change injected service based on environment
- Add search functionality
- Error handling everywhere (preferably gracefully)
- Add UI for dealing with null values on objects
- Decide on approach and implement error handling from the backend (alerts, toast notifications)
- Build out service that communicates with 3rd-party API; *note: figure out how to resolve name differences between JSON and models*
- Add unit tests for backend logic
- Decide on approach and implement error handling / validation for the 3rd-party API
- Set up a lightbox such that when the user clicks on the thumbnail, a larger image is shown
- Check Lighthouse for accessibility score (508 compliance)
- Add Redis for caching results from 3rd-party API (60 second expiry); used meta.updatedAt to determine when to refresh cache
- Create content for README.md so that others can run this app locally (test on another machine that this indeed works)
- Data cleaning on 3rd-party API data ingestion (e.g., `16GB` vs `16 GB`)
- Create Playwright tests

## Future Items
- Bootstrap for better UX (layout, colors)
- Pagination for main page
- Sort by {ColumnName}
- Search by fields other than title and description
- Currency setting (i.e., assume raw data is in USD then allow conversion to other currencies such as DKK)
- Deploy to Azure
- It would be useful if the 3rd-party had more granular endpoints (e.g., something to get the headers for a product, then another to get the details)

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