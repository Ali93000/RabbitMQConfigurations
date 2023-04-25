using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfigurations.Entities.DBEntities
{
    public class DB_ProductsChart
    {
        public int Id { get; set; }
        public string? Referece { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsSubmited { get; set; }
        public bool? IsActive { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual DB_Product? Product { get; set; }
        public virtual DB_User? User { get; set; }
    }
}
