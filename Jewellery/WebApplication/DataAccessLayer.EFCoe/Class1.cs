using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EFCoe
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }

    public enum MetalType
    {
        Gold,
        Silver,
        Cupper

    }

    public class Product
    {
        private decimal? _labourCharge;
        private decimal? _unitPrice;

        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string ProductName { get; set; }
        public short? UnitsInStock { get; set; }
        public MetalType MetalType { get; set; }
        public string Photo { get; set; }

        public bool ManMade { get; set; }

        public decimal? LabourCharge
        {
            get => _labourCharge.HasValue ? Math.Round(_labourCharge.Value, 3) : default;
            set => _labourCharge = value;
        }

        public decimal? UnitPrice
        {
            get => _unitPrice.HasValue ? Math.Round(_unitPrice.Value, 3) : default;
            set => _unitPrice = value;
        }

        private decimal? _weight;

        public decimal? Weight
        {
            get => _weight.HasValue ? Math.Round(_weight.Value, 3) : default;
            set => _weight = value;
        }
    }

}
