﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }

        private decimal _price;

        public decimal Price
        {
            get => _price;
            set => _price = decimal.Round(value, 2);
        }

        public static decimal TAX { get; set; } = 20;
        public static decimal Discount { get; set; } = 0;


        public Product(string name, int uPC, decimal price)
        {
            Name = name;
            UPC = uPC;
            Price = price;   
        }
       
        public override string ToString()
        {
            return $"Product: {Name}, (UPC: {UPC}) , Base price: {Price:C2}";
        }

        private decimal PriceCalculation(decimal Percentage)
        {
            decimal amount = Price * (Percentage / 100);
            decimal totalPrice = Price + amount;
            return decimal.Round(totalPrice, 2);

        }

        public void ProductWithFlatRateTax()
        {
            ProductWithCalculatedTax(TAX); // This TAX Value for All Product
        }

       
        public void ProductWithCalculatedTax(decimal taxPercentage)
        {
            decimal totalPrice = PriceCalculation(taxPercentage);
            Console.WriteLine($" Product price reported as ${Price }  before tax " +
                         $"and ${totalPrice} after {taxPercentage}% tax.");
        }

        public void ProductWithFlatRateTaxAndDiscount()
        {
            ProductWithCalculatedTaxAndDiscount(TAX, Discount);  // these Values for All Products
        }

        public void ProductWithCalculatedDiscount(decimal discountPercentage)
        {
            ProductWithCalculatedTaxAndDiscount(TAX, discountPercentage); 
        }
        public void ProductWithCalculatedTaxAndDiscount(decimal taxPercentage, decimal discountPercentage)
        {
            decimal pricewithTax = PriceCalculation(taxPercentage);
            decimal priceWithDiscount= PriceCalculation(discountPercentage);
            decimal totalPrice = pricewithTax - priceWithDiscount + Price;

            Console.WriteLine($" Product price reported as ${Price }  before tax and discount " +
                         $"and ${totalPrice} after {taxPercentage}% tax and {discountPercentage}% discount.");
        }
    }
}
