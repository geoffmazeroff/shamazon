# Completed
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

# To Do
- "File > New Project" a Razor page template project
- Start a `CHANGELOG.md` file
- Implement a logger service (log to console for now)
- Create data model for items returned from 3rd-party API
- Build backend service to return hard-coded list
- Add unit tests for backend logic
- Create mock data service to generate data models
- Wire up backend to depend on dependency-injected service
- Have default service be the mock service
- Main page should get all products and display in a table and searching based on a query string
  - REQUIREMENTS HERE (e.g., input sanitization)
- Decide on approach and implement error handling from the backend (alerts, toast notifications)
- Build out service that communicates with 3rd-party API
- Decide on approach and implement error handling / validation for the 3rd-party API
- Set up a lightbox such that when the user clicks on the thumbnail, a larger image is shown
- Check Lighthouse for accessibility score (508 compliance)
- Add Redis for caching results from 3rd-party API (60 second expiry)
- Create content for README.md so that others can run this app locally (test on another machine that this indeed works)
- Data cleaning on 3rd-party API data ingestion (e.g., `16GB` vs `16 GB`)
- Playwright tests

# Future Items
- Bootstrap for better UX (layout, colors)
- Pagination for main page
- Sort by {ColumnName}
- Search by fields other than title and description
- Currency setting (i.e., assume raw data is in USD then allow conversion to other currencies such as DKK)
- Deploy to Azure