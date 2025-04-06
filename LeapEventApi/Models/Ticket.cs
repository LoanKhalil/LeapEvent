namespace LeapEventApi.Models
{
    public class Ticket
    {
        public virtual int Id { get; set; }
        public virtual int EventId { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int QuantitySold { get; set; }
    }
}
