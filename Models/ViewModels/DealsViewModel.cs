using lab_5.Models;
using System.Collections.Generic;

namespace lab_5.Models.ViewModels
{
    public class DealsViewModel
    {
        public IEnumerable<FoodDeliveryService> FoodDeliveryServices { get; set; } = new List<FoodDeliveryService>();
        public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
        public IEnumerable<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public string? SelectedServiceId { get; set; }
    }
}
