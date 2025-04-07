using FluentNHibernate.Mapping;
using LeapEventApi.Models;

namespace LeapEventApi.Mapping
{
    public class EventsMap : ClassMap<Events>
    {
        public EventsMap()
        {
            Table("Events");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.StartsOn);
            Map(x => x.EndsOn);
            Map(x => x.Location);
        }
    }
}
