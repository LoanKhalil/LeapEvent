using FluentNHibernate.Mapping;
using LeapEventApi.Models;

namespace LeapEventApi.Mapping
{
    public class TicketMap : ClassMap<Ticket>
    {
        public TicketMap()
        {
            Table("Tickets");
            Id(x => x.Id);
            Map(x => x.EventId);
            Map(x => x.Price);
        }
    }
}
