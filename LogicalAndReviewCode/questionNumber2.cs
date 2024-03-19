using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LogicalAndReviewCode
{
    public class questionNumber2
    {
        public ApplicationInfo GetInfo()
        {
            var application = new ApplicationInfo
            {
                Name = "Shield.exe",
                Path = "C:/apps/"
            };
            return application;
        }

        public (string, string) GetInfoAnswer()
        {
            return ("Shield.exe", "C:/apps/");
        }
    }

    public class ApplicationInfo
    {
        public string Name { get; init; }
        public string Path { get; init; }
    }
}