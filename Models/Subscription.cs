using lab_5.Models;

namespace lab_5.Models
{
    public class Subscription
    {
        public int CustomerId { get; set; }
        public string FoodDeliveryServiceId { get; set; } = string.Empty;

        // Nav
        public Customer Customer { get; set; } = default!;
        public FoodDeliveryService FoodDeliveryService { get; set; } = default!;
    }
}
