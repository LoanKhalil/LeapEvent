## Section 1: Coding Challenge (ASP.NET Core & JavaScript)

Given database schema from skillsAssessmentEvents.db, I was able to use AI to generate a lot of the boiler plate code for the data access layer as well as the necessary services and api controller.

Using Visual Studio 2022, I began with the ASP.NET Core Web API template.  Then installed the necessary packages such as NHibernate & SQLite

I chose to use Angular for the frontend, which again a lot of the code was generated using AI.
I added the use of indexDB for caching the results for 30/60/180 days; however, I did add a refresh button that would clear cache and refetch as needed.

I felt like more time was spent setting up the projects and deploying the app/api to Azure, compared to the amount of time spent coding, but in my experience, this is common.
Now that the projects and environments are set up, adding more logic and features will be a breeze.

Final app can be viewed at:
https://leapeventapp-ftauffe0gugahjfu.canadacentral-01.azurewebsites.net/


## Section 2: System Design & Troubleshooting

I have not used Redis before, but for high-traffic, I would imagine writing to database would be a problem.
We had this issue with oil and gas sensors, IoT devices.  I did a sandbox implementation of MQTT subsriber and broker, which might also work.  
Data are written to database in batches as oppose to each discreet value. 

So a quick Google about Redis, 3 caching strategies are presented:
1. Cache-Aside:
Concept: The application checks the cache first. If the data is found, it's returned; otherwise, it's fetched from the database, stored in the cache, and then returned to the application.
Pros: Simple to implement and well-suited for read-heavy applications.
Cons: Requires application-level handling of cache interactions. 
2. Write-Through:
Concept: Every write operation to the database is also written to the cache.
Pros: Ensures cache data is always consistent with the database.
Cons: Can lead to increased write load on the database and cache. 
3. Write-Behind (or Write-Back):
Concept: Writes are initially made to the cache, and then, asynchronously, to the database.
Pros: Reduces write load on the database, as writes can be batched.
Cons: Can lead to temporary data inconsistency if the cache fails before writes to the database.

I think #3 is the most relevant caching strategy for the purpose of a ticketing system.

Looks like Azure offers Redis cache, but not having used it before, I can't really add much.











