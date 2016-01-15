using System;

namespace DerAlbert
{

    public interface IDateTime
    {
        DateTime Now { get; }
    }

    public class Date : IDateTime
    {
        static IDateTime _dateTime = new Date();

        public static DateTime Now => _dateTime.Now;

        public static DateTime UtcNow => _dateTime.Now.ToUniversalTime();

        DateTime IDateTime.Now => DateTime.Now;

        public static void SetDateTimeProvider(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }
    }
}