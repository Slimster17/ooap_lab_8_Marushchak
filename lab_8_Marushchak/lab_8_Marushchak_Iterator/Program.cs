using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_8_Marushchak
{
    abstract class Employee
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

 class EmployeeSalaryComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return x.Salary.CompareTo(y.Salary);
        }
    }

    abstract class Iterator : IEnumerable<Employee>
    {
        public abstract Employee CurrentItem();
        public abstract bool MoveNext();

        public abstract IEnumerator<Employee> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    class ConcreteAggregate : Aggregate
    {
        private List<Employee> _items = new List<Employee>();

        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public Employee this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }

        public void AddEmployee(Employee employee)
        {
            _items.Add(employee);
        }

        public void SortBySalary()
        {
            _items.Sort(new EmployeeSalaryComparer());
        }
    }

    class ConcreteIterator : Iterator
    {
        private ConcreteAggregate _aggregate;
        private int _current;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this._aggregate = aggregate;
            _current = -1;
        }
        
        public override bool MoveNext()
        {
            _current++;
            return _current < _aggregate.Count;
        }
        
        public override Employee CurrentItem()
        {
            return _aggregate[_current];
        }

        public override IEnumerator<Employee> GetEnumerator()
        {
            for (int i = 0; i < _aggregate.Count; i++)
            {
                yield return _aggregate[i];
            }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            ConcreteAggregate a = new ConcreteAggregate();
            a.AddEmployee(new SelfEmployee("Ivan", 1700));
            a.AddEmployee(new SelfEmployee("Taras", 1250));
            a.AddEmployee(new SalariedEmployee("Mykola", 3500));
            a.AddEmployee(new UnemployedEmployee("Petro"));
            

            Iterator i = a.CreateIterator();

            while (i.MoveNext())
            {
                Employee employee = i.CurrentItem();
                Console.WriteLine($"Name: {employee.Name}; " +
                                  $"Salary: {employee.Salary}; " +
                                  $"Status: {employee.Status}");
            }

            Console.WriteLine(new string('*', 50));
            
            a.SortBySalary();
            

            foreach (Employee employee in i)
            {
                Console.WriteLine($"Name: {employee.Name}; " +
                                  $"Salary: {employee.Salary}; " +
                                  $"Status: {employee.Status}");
            }

            Console.ReadKey();
        }
    }
}