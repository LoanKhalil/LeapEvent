using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using LeapEventApi.Models;
using NHibernate;
using ISession = NHibernate.ISession;

namespace LeapEventApi
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        
        public static ISessionFactory SessionFactory =>
            _sessionFactory ??= Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(Path.Combine(AppContext.BaseDirectory, "skillsAssessmentEvents.db")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Event>())
                .BuildSessionFactory();

        public static ISession OpenSession() => SessionFactory.OpenSession();
    }
}
