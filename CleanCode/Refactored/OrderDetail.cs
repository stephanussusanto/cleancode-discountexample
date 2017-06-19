using System;
using System.Collections.Generic;

namespace CleanCode.Refactored
{
    public class OrderDetail
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        private double TotalPriceBeforeDiscount => Product?.Price * Quantity ?? 0;

        private double TotalPriceWithDiscount
        {
            get
            {
                var discount = GetDiscount();
                return TotalPriceBeforeDiscount - (discount * TotalPriceBeforeDiscount) / 100;
            }
        }

        public double GetTotalPrice(Customer customer)
        {
            ValidateData();
            
            return IsMeetGetDiscountRule(customer) == false ? TotalPriceBeforeDiscount : TotalPriceWithDiscount;
        }

        private void ValidateData()
        {
            if (Product == null)
            {
                throw new NullReferenceException("Product can not be null.");
            }

            if (Quantity == 0)
            {
                throw new Exception("Quantity should be more than 0");
            }
        }

        private double GetDiscount()
        {
            const int maxDiscount = 25;

            var discount = 0;

            discount += GetDiscountFromQuantities();
            discount += GetDiscountFromCategories();
            discount += GetDiscountFromManufacturers();

            return discount > maxDiscount ? maxDiscount : discount;
        }

        private int GetDiscountFromQuantities()
        {
            if (Quantity >= 50)
            {
               return 20;
            }

            if (Quantity >= 20)
            {
                return 10;
            }

            return Quantity >= 10 ? 5 : 0;
        }

        private int GetDiscountFromCategories()
        {
            var discountCategories = new List<string> { "Fashion", "Food" };
            return discountCategories.Contains(Product.Category) ? 5 : 0;
        }

        private int GetDiscountFromManufacturers()
        {
            var discountManufacturers = new List<string> { "X Electronic" };
            return discountManufacturers.Contains(Product.Manufacturer) ? 10 : 0;
        }

        private bool IsMeetGetDiscountRule(Customer customer)
        {
            const int minJoinedYearLengthGetDiscount = 1;

            return GetCustomerJoinedYearLength(customer.JoinDate) > minJoinedYearLengthGetDiscount || customer.IsGoldCustomer;
        }

        private int GetCustomerJoinedYearLength(DateTime joinDate)
        {
            if (DateTime.Now.DayOfYear >= joinDate.DayOfYear)
            {
                return DateTime.Now.Year - joinDate.Year;
            }

            return DateTime.Now.Year - joinDate.Year - 1;
        }
    }
}
