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
            try
            {
               

                using (output = new StreamWriter(fileName))

                {
                    output.WriteLine("Emp #\t Last Name \t First Name \t Time Worked \t Hourly Wage \t Pay ");
                    foreach (Employee emp in employeeArray)
                    {
                        TimeStamp temp = new TimeStamp();
                        output.Write(emp.EmployeeNo + "\t");
                        output.Write(emp.LastName + "\t");
                        output.Write(emp.FirstName + "\t");
                        output.Write(emp.timeStamp + "\t");
                        output.Write(emp.Rate + "\t");
                        double payPerSec = emp.Rate / 3600;
                        int seconds= emp.timeStamp.ConvertToSeconds();
                        output.Write((payPerSec*seconds).ToString("c2"));
                        output.WriteLine();
                    }
                }

            }catch(IOException e)
            {
                Console.WriteLine("There's an error writing into the file" + e.Message);
            }
        }
    }
}
