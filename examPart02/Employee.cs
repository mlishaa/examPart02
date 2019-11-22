using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examPart02
{
    class Employee
    {
      
            private int employeeNo;
            private string firstName;
            private string lastName;
            private double rate;
            private int time;
        public TimeStamp timeStamp;
            
       

        public int EmployeeNo { get => employeeNo;
            set => employeeNo = value; }

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
                             double _rate,TimeStamp timeStamp
                              )
            {
                this.employeeNo = _employeeNo;
                this.firstName = _firstName;
                this.lastName = _lastName;
                this.rate = _rate;
            this.timeStamp = timeStamp;
                

            }

   


    }
    
}
