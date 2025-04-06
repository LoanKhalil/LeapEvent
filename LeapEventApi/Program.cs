
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LeapEventApi.Models;
using LeapEventApi.Repositories;
using LeapEventApi.Services;
using NHibernate;
using NHibernate.Cfg;

namespace LeapEventApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<ISessionFactory>(factory =>
                Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(Path.Combine(AppContext.BaseDirectory,
                        "skillsAssessmentEvents.db")))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Event>())
                    .BuildSessionFactory());

            builder.Services.AddScoped(factory =>
            {
                var sessionFactory = factory.GetRequiredService<ISessionFactory>();
                return sessionFactory.OpenSession();
            });

            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
