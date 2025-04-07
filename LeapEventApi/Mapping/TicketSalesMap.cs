using FluentNHibernate.Mapping;
using LeapEventApi.Models;

namespace LeapEventApi.Mapping
{
    public class TicketSalesMap : ClassMap<TicketSales>
    {
        public TicketSalesMap()
        {
            Table("TicketSales");
            Id(x => x.Id);
            Map(x => x.EventId);
            Map(x => x.PriceInCents);
            Map(x => x.PurchaseDate);
            Map(x => x.UserId);
        }
    }
}
