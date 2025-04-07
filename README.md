Section 1: Coding Challenge (ASP.NET Core & JavaScript)

Given database schema from skillsAssessmentEvents.db, I was able to use AI to generate a lot of the boiler plate code for the data access layer as well as the necessary services and api controller.

Using Visual Studio 2022, I began with the ASP.NET Core Web API template.  Then installed the necessary packages such as NHibernate & SQLite

I chose to use Angular for the frontend, which again a lot of the code was generated using AI.
I added the use of indexDB for caching the results for 30/60/180 days however I did add a refresh button that would allow user to refetch as needed.

I felt like more time was spent setting up the projects and deploying the app/api to Azure, compared to the amount of time spent coding, but in my experience, this is common.
Now that the projects and environments are set up, adding more logic and features will be a breeze.

Final app can be view at:
https://leapeventapp-ftauffe0gugahjfu.canadacentral-01.azurewebsites.net/














