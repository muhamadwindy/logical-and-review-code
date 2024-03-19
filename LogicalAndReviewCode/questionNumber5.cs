using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LogicalAndReviewCode
{
    public class questionNumber5
    {
        public questionNumber5()
        {
            var publisher = new EventPublisher();
            while (true)
            {
                var subscriber = new EventSubscriberAnswer(publisher);
                // do something with the publisher and subscriber objects
                subscriber.Dispose();
            }
        }
    }

    internal class EventPublisher
    {
        public event EventHandler MyEvent;

        public void RaiseEvent()
        {
            MyEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    internal class EventSubscriber
    {
        public EventSubscriber(EventPublisher publisher)
        {
            publisher.MyEvent += OnMyEvent;
        }

        private void OnMyEvent(object sender, EventArgs e)
        {
            Console.WriteLine("MyEvent raised");
        }
    }

    internal class EventSubscriberAnswer : IDisposable
    {
        private EventPublisher publisher;
        private bool isDisposed = false;

        public EventSubscriberAnswer(EventPublisher publisher)
        {
            this.publisher = publisher;
            this.publisher.MyEvent += OnMyEvent;
        }

        private void OnMyEvent(object sender, EventArgs e)
        {
            Console.WriteLine("MyEvent raised");
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            publisher.MyEvent -= OnMyEvent;
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}