using FluentNHibernate.Mapping;
using LeapEventApi.Models;

namespace LeapEventApi.Mapping
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Table("Events");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.StartDateTime);
            Map(x => x.EndDateTime);
        }
    }
}
