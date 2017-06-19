using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderDetail = CleanCode.Origin.OrderDetail;

namespace CleanCode.Test
{
    [TestClass]
    public class OrderDetailTest
    {
        private static readonly List<Product> Products = new List<Product>()
            {
                new Product() { Name = "Clean Code Practice", Price = 0.5, Category = "Books", Manufacturer = "1 Publishing"},
                new Product() { Name = "Blue Shirt", Price = 20, Category = "Fashion", Manufacturer = "York Fashion" },
                new Product() { Name = "XNote GX-JW551", Price = 1000, Category = "Electronic", Manufacturer = "X Electronic" }
            };

        private static readonly List<Customer> Customers = new List<Customer>()
            {
                new Customer() { FullName = "NewCustomer", JoinDate = DateTime.Now, IsGoldCustomer = false},
                new Customer() { FullName = "3YearCustomer", JoinDate = DateTime.Now.AddYears(-3), IsGoldCustomer = false},
                new Customer() { FullName = "GoldCustomer", JoinDate = DateTime.Now, IsGoldCustomer = true}
            };

        [TestMethod]
        public void TestDiscount10()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("3YearCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("Clean Code Practice")),
                Quantity = 10
            };
            
            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 4.75);
        }

        [TestMethod]
        public void TestDiscount20()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("3YearCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("Clean Code Practice")),
                Quantity = 20
            };
            
            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 9);
        }

        [TestMethod]
        public void TestDiscount50()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("3YearCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("Clean Code Practice")),
                Quantity = 50
            };

            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 20);
        }

        [TestMethod]
        public void TestManufactureDiscount()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("3YearCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("XNote GX-JW551")),
                Quantity = 1
            };

            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 900);
        }

        [TestMethod]
        public void TestCategoryDiscount()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("3YearCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("Blue Shirt")),
                Quantity = 10
            };

            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 180);
        }

        [TestMethod]
        public void TestGoldCustomer()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("GoldCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("Clean Code Practice")),
                Quantity = 10
            };

            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 4.75);
        }

        [TestMethod]
        public void TestNewCustomer()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("NewCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("Clean Code Practice")),
                Quantity = 10
            };

            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 5);
        }

        [TestMethod]
        public void TestMaxDiscount()
        {
            var customer = Customers.FirstOrDefault(customerItem => customerItem.FullName.Equals("3YearCustomer"));
            var orderDetail = new OrderDetail()
            {
                Product = Products.FirstOrDefault(product => product.Name.Equals("XNote GX-JW551")),
                Quantity = 50
            };

            Assert.IsTrue(orderDetail.GetTotalPrice(customer) == 37500);
        }
    }
}
