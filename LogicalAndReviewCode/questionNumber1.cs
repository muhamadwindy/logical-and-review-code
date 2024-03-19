using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LogicalAndReviewCode
{
    public class questionNumber1
    {
        private Application application;

        public questionNumber1()
        {
            application = new Application();
        }

        public DateTime question()
        {
            DateTime result = DateTime.Now;
            if (application != null)
            {
                if (application.Protected != null)
                {
                    result = application.Protected.shieldLastRun;
                }
            }

            return result;
        }

        public DateTime answer()
        {
            DateTime result = DateTime.Now;
            result = application?.Protected?.shieldLastRun ?? DateTime.Now;
            return result;
        }

        private class Application
        {
            public ProtectedInfo Protected { get; set; }
        }

        private class ProtectedInfo
        {
            public DateTime shieldLastRun { get; set; }
        }
    }
}