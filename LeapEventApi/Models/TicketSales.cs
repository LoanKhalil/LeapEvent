namespace LeapEventApi.Models
{
    public class TicketSales
    {
        public virtual string Id { get; set; }
        public virtual string EventId { get; set; }
        public virtual string UserId { get; set; }
        public virtual DateTime PurchaseDate { get; set; }
        public virtual Int32 PriceInCents { get; set; }
    }
}
