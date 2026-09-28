using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Session_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 1. Get first Product out of Stock
            var product1 = ListGenerator.ProductsList.First(p => p.UnitsInStock == 0);
            Console.WriteLine(product1);
            #endregion

            #region 2. Return first product whose Price > 1000, otherwise null
            var product2 = ListGenerator.ProductsList.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine(product2);
            #endregion

            #region 3. Retrieve the second number greater than 5
            int[] numbers3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var number3 = numbers3.Where(n => n > 5).Skip(1).FirstOrDefault();
            Console.WriteLine(number3);
            #endregion

            #region 4. Count the number of odd numbers
            int[] numbers4 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var oddCount4 = numbers4.Count(n => n % 2 != 0);
            Console.WriteLine(oddCount4);
            #endregion

            #region 5. Return customers and how many orders each has
            var customers5 = ListGenerator.CustomersList.Select(c => new
            {
                Customer = c.CustomerName,
                OrdersCount = c.Orders.Length
            });
            foreach (var customer in customers5)
                Console.WriteLine($"{customer.Customer} - {customer.OrdersCount}");
            #endregion

            #region 6. Return categories and how many products each has
            var categories6 = ListGenerator.ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    ProductsCount = g.Count()
                });
            foreach (var category in categories6)
                Console.WriteLine($"{category.Category} - {category.ProductsCount}");
            #endregion

            #region 7. Get the total of the numbers in an array
            int[] numbers7 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var total7 = numbers7.Sum();
            Console.WriteLine(total7);
            #endregion

            #region 8. Get the total number of characters of all words
            var words8 = File.ReadAllLines("dictionary_english.txt");
            var totalCharacters8 = words8.Sum(w => w.Length);
            Console.WriteLine(totalCharacters8);
            #endregion

            #region 9. Get the length of the shortest word
            var words9 = File.ReadAllLines("dictionary_english.txt");
            var shortest9 = words9.Min(w => w.Length);
            Console.WriteLine(shortest9);
            #endregion

            #region 10. Get the length of the longest word
            var words10 = File.ReadAllLines("dictionary_english.txt");
            var longest10 = words10.Max(w => w.Length);
            Console.WriteLine(longest10);
            #endregion

            #region 11. Get the average length of the words
            var words11 = File.ReadAllLines("dictionary_english.txt");
            var average11 = words11.Average(w => w.Length);
            Console.WriteLine(average11);
            #endregion

            #region 12. Get the total units in stock for each product category
            var stock12 = ListGenerator.ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalUnitsInStock = g.Sum(p => p.UnitsInStock)
                });
            foreach (var category in stock12)
                Console.WriteLine($"{category.Category} - {category.TotalUnitsInStock}");
            #endregion

            #region 13. Get the cheapest price among each category's products
            var cheapest13 = ListGenerator.ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    CheapestPrice = g.Min(p => p.UnitPrice)
                });
            foreach (var category in cheapest13)
                Console.WriteLine($"{category.Category} - {category.CheapestPrice}");
            #endregion

            #region 14. Get the products with the cheapest price in each category
            var cheapestProducts14 = from p in ListGenerator.ProductsList
                                     group p by p.Category into g
                                     let minPrice = g.Min(p => p.UnitPrice)
                                     select new
                                     {
                                         Category = g.Key,
                                         Products = g.Where(p => p.UnitPrice == minPrice)
                                     };
            foreach (var category in cheapestProducts14)
            {
                Console.WriteLine(category.Category);
                foreach (var product in category.Products)
                    Console.WriteLine(product);
            }
            #endregion

            #region 15. Get the most expensive price among each category's products
            var expensive15 = ListGenerator.ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    MostExpensivePrice = g.Max(p => p.UnitPrice)
                });
            foreach (var category in expensive15)
                Console.WriteLine($"{category.Category} - {category.MostExpensivePrice}");
            #endregion

            #region 16. Get the products with the most expensive price in each category
            var expensiveProducts16 = from p in ListGenerator.ProductsList
                                      group p by p.Category into g
                                      let maxPrice = g.Max(p => p.UnitPrice)
                                      select new
                                      {
                                          Category = g.Key,
                                          Products = g.Where(p => p.UnitPrice == maxPrice)
                                      };
            foreach (var category in expensiveProducts16)
            {
                Console.WriteLine(category.Category);
                foreach (var product in category.Products)
                    Console.WriteLine(product);
            }
            #endregion

            #region 17. Get the average price of each category's products
            var average17 = ListGenerator.ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    AveragePrice = g.Average(p => p.UnitPrice)
                });
            foreach (var category in average17)
                Console.WriteLine($"{category.Category} - {category.AveragePrice}");
            #endregion

            #region 18. Find the unique Category names from Product List
            var categories18 = ListGenerator.ProductsList
                .Select(p => p.Category)
                .Distinct();
            foreach (var category in categories18)
                Console.WriteLine(category);
            #endregion

            #region 19. Unique first letter from product and customer names
            var letters19 = ListGenerator.ProductsList
                .Select(p => p.ProductName[0])
                .Union(ListGenerator.CustomersList.Select(c => c.CustomerName[0]));
            foreach (var letter in letters19)
                Console.WriteLine(letter);
            #endregion

            #region 20. Common first letter from product and customer names
            var letters20 = ListGenerator.ProductsList
                .Select(p => p.ProductName[0])
                .Intersect(ListGenerator.CustomersList.Select(c => c.CustomerName[0]));
            foreach (var letter in letters20)
                Console.WriteLine(letter);
            #endregion

            #region 21. Product first letters not found in customer names
            var letters21 = ListGenerator.ProductsList
                .Select(p => p.ProductName[0])
                .Except(ListGenerator.CustomersList.Select(c => c.CustomerName[0]));
            foreach (var letter in letters21)
                Console.WriteLine(letter);
            #endregion

            #region 22. Last three characters of all customer and product names
            var lastThree22 = ListGenerator.CustomersList
                .Select(c => c.CustomerName.Substring(c.CustomerName.Length - 3))
                .Concat(ListGenerator.ProductsList.Select(p => p.ProductName.Substring(p.ProductName.Length - 3)));
            foreach (var value in lastThree22)
                Console.WriteLine(value);
            #endregion

            #region 23. Get the first 3 orders from customers in Washington
            var orders23 = ListGenerator.CustomersList
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Take(3);
            foreach (var order in orders23)
                Console.WriteLine(order);
            #endregion

            #region 24. Get all but the first 2 orders from customers in Washington
            var orders24 = ListGenerator.CustomersList
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Skip(2);
            foreach (var order in orders24)
                Console.WriteLine(order);
            #endregion

            #region 25. Return elements until a number is less than its position
            int[] numbers25 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var result25 = numbers25.TakeWhile((number, index) => number >= index);
            foreach (var number in result25)
                Console.WriteLine(number);
            #endregion

            #region 26. Get elements starting from the first element divisible by 3
            int[] numbers26 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var result26 = numbers26.SkipWhile(number => number % 3 != 0);
            foreach (var number in result26)
                Console.WriteLine(number);
            #endregion

            #region 27. Get elements starting from the first element less than its position
            int[] numbers27 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var result27 = numbers27.SkipWhile((number, index) => number >= index);
            foreach (var number in result27)
                Console.WriteLine(number);
            #endregion

            #region 28. Determine if any word contains the substring ei
            var words28 = File.ReadAllLines("dictionary_english.txt");
            var result28 = words28.Any(w => w.Contains("ei"));
            Console.WriteLine(result28);
            #endregion

            #region 29. Categories that have at least one product out of stock
            var categories29 = ListGenerator.ProductsList
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0));
            foreach (var category in categories29)
            {
                Console.WriteLine(category.Key);
                foreach (var product in category)
                    Console.WriteLine(product);
            }
            #endregion

            #region 30. Categories that have all products in stock
            var categories30 = ListGenerator.ProductsList
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0));
            foreach (var category in categories30)
            {
                Console.WriteLine(category.Key);
                foreach (var product in category)
                    Console.WriteLine(product);
            }
            #endregion

            #region 31. Partition numbers by remainder when divided by 5
            List<int> numbers31 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var groups31 = numbers31.GroupBy(n => n % 5);
            foreach (var group in groups31)
            {
                Console.WriteLine(group.Key);
                foreach (var number in group)
                    Console.WriteLine(number);
            }
            #endregion

            #region 32. Partition words by their first letter
            var words32 = File.ReadAllLines("dictionary_english.txt");
            var groups32 = words32.GroupBy(w => w[0]);
            foreach (var group in groups32)
            {
                Console.WriteLine(group.Key);
                foreach (var word in group)
                    Console.WriteLine(word);
            }
            #endregion

            #region 33. Group words that consist of the same characters
            string[] words33 = { "from", "salt", "earn", "last", "near", "form" };
            var groups33 = words33.GroupBy(w => w, new SameCharactersComparer());
            foreach (var group in groups33)
            {
                Console.WriteLine(group.Key);
                foreach (var word in group)
                    Console.WriteLine(word);
            }
            #endregion
        }
    }

    public class SameCharactersComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            if (x == null || y == null)
                return x == y;
            return string.Concat(x.OrderBy(c => c)) == string.Concat(y.OrderBy(c => c));
        }

        public int GetHashCode(string obj)
        {
            return string.Concat(obj.OrderBy(c => c)).GetHashCode();
        }
    }
}