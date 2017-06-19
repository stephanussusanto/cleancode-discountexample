using System;
using System.Collections.Generic;

namespace CleanCode.Origin
{
    public class OrderDetail
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double GetTotalPrice(Customer customer)
        {
            var dcList = new List<string> { "Fashion", "Food"};
            var dmList = new List<string> { "X Electronic" };
            var d = 0;
            var ok = false;
            var y = DateTime.Now.Year - customer.JoinDate.Year;
            if (DateTime.Now.DayOfYear < customer.JoinDate.DayOfYear)
            {
                y = y - 1;
            }

            //// give discount if customer joined more than 1 year
            if (y > 1)
            {
                ok = true;
            }

            //// give discount if customer is gold member
            if (customer.IsGoldCustomer)
            {
                ok = true;
            }

            if (Product != null)
            {
                if (ok)
                {
                    if (Quantity > 0)
                    {
                        if (Quantity >= 50)
                        {
                            d = 20;
                        }
                        else if (Quantity >= 20)
                        {
                            d = 10;
                        }
                        else if (Quantity >= 10)
                        {
                            d = 5;
                        }

                        foreach (var dc in dcList)
                        {
                            if (dc.Equals(Product.Category))
                            {
                                d += 5;
                                break;
                            }
                        }

                        foreach (var dc in dmList)
                        {
                            if (dc.Equals(Product.Manufacturer))
                            {
                                d += 10;
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Quantity should be more than 0");
                    }
                }
            }
            else
            {
                throw new NullReferenceException("Product can not be null.");
            }

            if (d > 25)
            {
                d = 25;
            }

            return (Product.Price * Quantity) - (d * Product.Price * Quantity) / 100;
        }
    }
}
