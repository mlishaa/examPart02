using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examPart02
{
    class TimeStamp
    {

        private int _hours;
        private int _minutes;
        private int _seconds;


        public int Hours
        {
            get => _hours;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hour can't be negative");
                }
                _hours = value;
            }
        }


        public int Minutes
        {
            get => _minutes;
            set
            {
                if (value < 0 || value > 60)
                    throw new ArgumentException("Minutes can't be less zero or above 60");
                _minutes = value;
            }
        }

        public int Seconds
        {
            get => _seconds;
            set
            {
                if (value < 0 || value > 60)
                    throw new ArgumentException("Seconds can't be less zero or above 60");
                _seconds = value;
            }
        }


        public TimeStamp() { }

        public TimeStamp(int _seconds, int _minutes, int _hours)
        {
            Seconds = _seconds;
            Minutes = _minutes;
            Hours = _hours;
        }

        public TimeStamp ConvertFromSeconds(int SecondsToConvert)
        {
            Hours = SecondsToConvert / 3600;
            Minutes = (SecondsToConvert - Hours * 3600) / 60;
            Seconds = (SecondsToConvert - Hours * 3600) - Minutes * 60;

            return this;
        }


        public int ConvertToSeconds()
        {
            return (Hours * 3600) + (Minutes * 60) + Seconds;

        }



        public void AddSeconds(int TheSeconds)
        {
            ConvertFromSeconds(TheSeconds + ConvertToSeconds());
        }


        public void ReadFromConsole()
        {

            Hours = GetIntegerInput("Please enter the number of hours", "Hours", 0, 23);
            Minutes = GetIntegerInput("Please enter the number of Minutes", "Minutes", 0, 59);
            Seconds = GetIntegerInput("Please enter the number of Seconds", "Seconds", 0, 59);

        }



        private int GetIntegerInput(string customMessage, string name, int min, int max)
        {
            int input;
            Console.Write("{0}:\t", customMessage);
            while (int.TryParse(Console.ReadLine(), out input) == false || input < min || input >= max)
            {
                Console.Write(string.Format("Please Enter {0} between ({1} and {2}) \t", name, min, max));
            }
            return input;
        }


        static public TimeStamp AddTwoTimeStamps(TimeStamp TimeStampOne, TimeStamp TimeStampTwo)
        {
            int seconds = TimeStampOne.ConvertToSeconds() + TimeStampTwo.ConvertToSeconds();
            TimeStamp timeSampt = new TimeStamp();
            timeSampt.ConvertFromSeconds(seconds);
            return timeSampt;
        }

        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds);
        }

    }
}
