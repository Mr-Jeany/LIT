using System;

namespace _3_4
{
    public class Weekdays : IPrinter
    {
        public void Print()
        {
            foreach (var i in Enum.GetValues(typeof(listOfWeekDays)))
            {
                Console.WriteLine(i);
            }
        }
        
        public enum listOfWeekDays
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
    }

    
}