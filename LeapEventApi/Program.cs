
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LeapEventApi.Mapping;
using LeapEventApi.Models;
using LeapEventApi.Repositories;
using LeapEventApi.Services;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

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

            builder.Services.AddSingleton(factory =>
            {
                return Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard
                        .ConnectionString("Data Source=skillsAssessmentEvents.db;Version=3;")
                        .Dialect<SQLiteDialect>()
                        .Driver<SQLite20Driver>())
                    .Mappings(m =>
                    {
                        m.FluentMappings.AddFromAssemblyOf<EventsMap>();
                    })
                    .BuildSessionFactory();
            });

            builder.Services.AddScoped(factory =>
            {
                var sessionFactory = factory.GetRequiredService<ISessionFactory>();
                return sessionFactory.OpenSession();
            });

            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
