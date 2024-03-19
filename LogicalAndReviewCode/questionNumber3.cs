using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LogicalAndReviewCode
{
    public class questionNumber3
    {
        public questionNumber3()
        {
            var laptop = new Laptop("macOs");
            Console.WriteLine(laptop.GetOs()); // Laptop os: macOs
        }
    }

    public class Laptop
    {
        private string _os;

        public Laptop(string os)
        {
            _os = os;
        }

        public string GetOs()
        {
            return _os;
        }
    }
}