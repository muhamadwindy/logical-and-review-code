using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LogicalAndReviewCode
{
    public class questionNumber4
    {
        public questionNumber4()
        {
            var myList = new List<Product>();
            while (true)
            {
                // populate list with 1000 integers
                for (int i = 0; i < 1000; i++)
                {
                    myList.Add(new Product(Guid.NewGuid().ToString(), i));
                }
                // do something with the list object
                Console.WriteLine(myList.Count);

                myList.Clear();
            }
        }

        private class Product
        {
            public Product(string sku, decimal price)
            {
                SKU = sku;
                Price = price;
            }

            public string SKU { get; set; }
            public decimal Price { get; set; }
        }
    }
}