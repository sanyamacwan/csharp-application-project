using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab_5.Models
{
    public class FoodDeliveryService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Registration Number")]
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Fee { get; set; }

        // Nav
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
