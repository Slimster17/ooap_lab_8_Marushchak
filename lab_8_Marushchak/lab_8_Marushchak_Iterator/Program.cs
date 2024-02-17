using System;
using System.Collections.Generic;

namespace lab_8_Marushchak
{
    class Employee
    {
        public string Name { get; set; }
        public double Salary { get; set; }
        
        public string Status { get; set; }

        public Employee(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }
    }
    
    class SelfEmployee : Employee
    {
        public SelfEmployee(string name, double salary) : base(name, salary)
        {
            Status = "Self employeed";
        }
        
    }
    
    class SalariedEmployee: Employee
    {
        public SalariedEmployee(string name, double salary) : base(name, salary)
        {
            Status = "Salaried";
        }
    }
    
    class UnemployedEmployee : Employee
    {
        public UnemployedEmployee(string name) : base(name, 0)
        {
            Status = "Unemployeed";
        }
    }

    class EmployeeIterator
    {
        private List<Employee> _employees;
        private int _currentIndex;

        public EmployeeIterator(List<Employee> employees)
        {
            _employees = employees;
            _employees.Sort((emp1,emp2) => emp1.Salary.CompareTo(emp2.Salary));
            _currentIndex = 0;
        }

        public bool HasNext()
        {
            return _currentIndex < _employees.Count;
        }

        public Employee Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more elements in the collection");
            }

            Employee nextEmployee = _employees[_currentIndex];
            _currentIndex++;
            return nextEmployee;
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>
            {
                new SelfEmployee("Ivan", 1700),
                new SelfEmployee("Taras", 1250),
                new SalariedEmployee("Mykola", 3500),
                new UnemployedEmployee("Petro")
            };

            EmployeeIterator employeeIterator = new EmployeeIterator(employees);

            while (employeeIterator.HasNext())
            {
                Employee employee = employeeIterator.Next();
                Console.WriteLine($"Name: {employee.Name}; Salary: {employee.Salary}; Status: {employee.Status}");
            }
        }
    }
}