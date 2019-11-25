using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace examPart02
{


    class Program
    {
        static void Main(string[] args)
        {
            // mike lishaa
            List<Employee> employeeArray = new List<Employee>();

            generateEmployeeListFromFile(employeeArray, "emp.txt");


            processTimeWorkedFile(employeeArray, "hours.txt");


            printReport(employeeArray, "Report.txt");
            Console.ReadKey();
        }

        public static void generateEmployeeListFromFile(List<Employee> employeeArray, string fileName)
        {

            // reading the employees info 
           
            string line;
            string[] words;
            StreamReader input = null;
            try
            {
                using (input = new StreamReader(fileName))

                {
                    while ((line = input.ReadLine()) != null)
                    {
                        words = line.Split('|');
                        int id = int.Parse(words[0]);
                        string first = words[1];
                        string last = words[2];
                       

                        double rate = double.Parse(words[3]);
                     
                      

                        

                        Employee temp = new Employee(id, first, last, rate,new TimeStamp());
                        employeeArray.Add(temp);
                    }
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("There's an error reading from the file" + e.Message);
            }
        }


        // reading timestamp and hourlyRate
        public static void processTimeWorkedFile(List<Employee> employeeArray, string fileName)
        {

            string line;
            string[] words;
            string[] stamps;
            StreamReader input = null;
            try
            {
                using (input = new StreamReader(fileName))

                {
                    while ((line = input.ReadLine()) != null)
                    {
                        words = line.Split('|');
                        int id = int.Parse(words[0]);
                        // has to call the helper method to compare the numbers

                        TimeStamp tempTimeStamp = new TimeStamp();
                        stamps = words[1].Split(':');
                        tempTimeStamp.Hours = int.Parse(stamps[0]);
                        tempTimeStamp.Minutes = int.Parse(stamps[1]);
                        tempTimeStamp.Seconds = int.Parse(stamps[2]);

                        addTimeWorkedToEmployee(employeeArray, id, tempTimeStamp);
                    }
                }
            }catch
            (IOException e)
            {
                Console.WriteLine("There's an error reading from the file" + e.Message);
            }
        }


        // helper method to add time stamp together
        public static void addTimeWorkedToEmployee(List<Employee> employeeArray, int employeeNumber, TimeStamp timeWorked)
        {
            foreach(Employee emp in employeeArray)
            {
                if (emp.EmployeeNo == employeeNumber)
                {
                   
                    emp.timeStamp = TimeStamp.AddTwoTimeStamps(timeWorked,emp.timeStamp);

                }
               
               
            }
        }

        // to print the report
        public static void printReport(List<Employee> employeeArray, string fileName)
        {
            StreamWriter output = null;
            double total=0;
            TimeStamp timeStamp=new TimeStamp();
            try
            {
               

                using (output = new StreamWriter(fileName))

                {
                    output.WriteLine(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5,-20}", "Emp #", "Last name", "First name", "Time", "Hourly wage","Pay"));
                    output.WriteLine(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5,-20}", "-----", "----------", "----------", "-----", "--------", "-----"));

                    foreach (Employee emp in employeeArray)
                    {
                        TimeStamp temp = new TimeStamp();
                        output.Write(string.Format("{0,-20}",emp.EmployeeNo ));
                        output.Write(string.Format("{0,-20}", emp.LastName ));
                        output.Write(string.Format("{0,-20}", emp.FirstName ));
                        output.Write(string.Format("{0,-20}", emp.timeStamp ));
                        timeStamp = TimeStamp.AddTwoTimeStamps(emp.timeStamp, timeStamp);
                        output.Write(string.Format("{0,-20}", emp.Rate));
                        double payPerSec = emp.Rate / 3600;
                        int seconds= emp.timeStamp.ConvertToSeconds();
                        output.Write(string.Format("{0,-20}", (payPerSec*seconds).ToString("c2")));
                        total += payPerSec * seconds;
                        output.WriteLine();

                    }
                    output.Write("Total Hours Worked =");
                    output.WriteLine(timeStamp.ToString());
                    output.Write("Total Paid =");
                    output.WriteLine(total.ToString("c2"));
                }

            }catch(IOException e)
            {
                Console.WriteLine("There's an error writing into the file" + e.Message);
            }
        }
    }

    class Employee
    {

        private int employeeNo;
        private string firstName;
        private string lastName;
        private double rate;
        private int time;
        public TimeStamp timeStamp;




        public int EmployeeNo
        {
            get => employeeNo;
            set => employeeNo = value;
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public double Rate
        {
            get => rate;
            set => rate = value;
        }

        public int Time
        {
            get => time;
            set => time = value;
        }

        public Employee(int _employeeNo,
                             string _firstName,
                             string _lastName,
                             double _rate, TimeStamp timeStamp
                              )
        {
            this.employeeNo = _employeeNo;
            this.firstName = _firstName;
            this.lastName = _lastName;
            this.rate = _rate;
            this.timeStamp = timeStamp;


        }







    }

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
                if (value < 0 || value >= 60)
                    throw new ArgumentException("Minutes can't be less zero or above 60");
                _minutes = value;
            }
        }

        public int Seconds
        {
            get => _seconds;
            set
            {
                if (value < 0 || value >= 60)
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
