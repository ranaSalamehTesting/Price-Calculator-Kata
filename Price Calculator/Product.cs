﻿using System;

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

        public static decimal Tax { get; set; } = 20;
        public static decimal Discount { get; set; } = 0;

        private static decimal UPCDiscount { get; set; } = 0;  // Percentage 
        private static int DiscountedUPC { get; set; } = 0;  // applied for this UPC only


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
        private decimal PriceCalculation(decimal percentage)
        {
            decimal amount = Amount(percentage);
            decimal totalPrice = Price + amount;
            return decimal.Round(totalPrice, 2);

        }
        private decimal Amount(decimal percentage)
        {
            return (Price * (percentage / 100));
        }
        public void ProductWithFlatRateTax()
        {
            ProductWithCalculatedTax(Tax); // This TAX Value for All Product
        }
        public void ProductWithCalculatedTax(decimal taxPercentage)
        {
            decimal totalPrice = PriceCalculation(taxPercentage);
            Console.WriteLine($" Product price reported as ${Price }  before tax " +
                         $"and ${totalPrice} after {taxPercentage}% tax.");
        }
        public void ProductWithFlatRateTaxAndDiscount()
        {
            ProductWithCalculatedTaxAndDiscount(Tax, Discount);  // these Values for All Products
        }
        public void ProductWithCalculatedDiscount(decimal discountPercentage)
        {
            ProductWithCalculatedTaxAndDiscount(Tax, discountPercentage);
        }
        public void ProductWithCalculatedTaxAndDiscount(decimal taxPercentage, decimal discountPercentage)
        {
            decimal pricewithTax = PriceCalculation(taxPercentage);
            decimal priceWithDiscount = PriceCalculation(discountPercentage);
            decimal totalPrice = pricewithTax - priceWithDiscount + Price;

            Console.WriteLine($" Product price reported as ${Price }  before tax and discount " +
                         $"and ${totalPrice} after {taxPercentage}% tax and {discountPercentage}% discount.");
        }
        public void Reoprt()
        {

            if (Discount == 0)
            {
                ProductWithCalculatedTax(Tax);
                return;
            }

            if (UPC == DiscountedUPC)
            {
                DisplyFullInformation(UPCDiscount);
                return;
            }

            DisplyFullInformation(0);

        }

        private void DisplyFullInformation(decimal uPCDiscount )
        {
            ProductWithCalculatedTaxAndDiscount(Tax, Discount + uPCDiscount);

            decimal totalDiscountAmount = Amount(Discount + uPCDiscount);
            totalDiscountAmount = decimal.Round(totalDiscountAmount, 2);
            Console.WriteLine($" The total discount amount is ${totalDiscountAmount}");
        }

        public static void AddUPCDiscount(int discountedUPC , decimal uPCDiscountPercentage)
        {
            DiscountedUPC = discountedUPC;
            UPCDiscount = uPCDiscountPercentage;
        }


    }
}
