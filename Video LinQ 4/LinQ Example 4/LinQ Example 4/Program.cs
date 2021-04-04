using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Linq_Example_3
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            //Equality Operator
            //SequenceEqual
            var integerlist1 = new List<int> { 1, 2, 3, 4, 5 };
            var integerlist2 = new List<int> { 1, 2, 3, 4, 5 };
            var boolsquence = integerlist1.SequenceEqual(integerlist2);
            if (boolsquence)
            {
                Console.WriteLine("Both are Equal");
            } 
            else
            {
                Console.WriteLine("Not Equal");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");
            var employeelistcompare = Data.GetEmployees();
            var boolsequenceobject = employeeList.SequenceEqual(employeelistcompare);
            if (boolsequenceobject)
            {
                Console.WriteLine("Both are Equal");
            }
            else
            {
                Console.WriteLine("Not Equal");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");
            var boolsequenceobjectcompare = employeeList.SequenceEqual(employeelistcompare,new compareEmployee());
            if (boolsequenceobjectcompare)
            {
                Console.WriteLine("Both are Equal");
            }
            else
            {
                Console.WriteLine("Not Equal");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");
            //2-concatenation
            List<Employee> employeeslist2 = new List<Employee>
            {
                new Employee
                {
                    Id=5,
                    FirstName="Employee5",
                    LastName="Employee5 last name",
                },
                new Employee
                {
                     Id=6,
                    FirstName="Employee6",
                    LastName="Employee6 last name",
                }
            };
            var contactEmployee = employeeList.Concat(employeeslist2);
            foreach (var emp in contactEmployee)
            {
                Console.WriteLine($"{emp.FirstName} - {emp.LastName} Id : {emp.Id}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");
            //3-Average
            var Averagesalary = employeeList.Where(e=>e.DepartmentId==3).Average(e => e.AnuualSalary);
            Console.WriteLine($"The Average Salary {Averagesalary}");
            Console.WriteLine($"-----------------------------------------------------------------------");
            //4-count
            int employeecount = employeeList.Count();
            Console.WriteLine($"Count of the employees {employeecount}");
            Console.WriteLine($"-----------------------------------------------------------------------");
            //5-sum
            int employeecountid = employeeList.Sum(e=>e.Id);
            Console.WriteLine($"sum of the employees id {employeecountid}");
            Console.WriteLine($"-----------------------------------------------------------------------");
            //6-Max
            int max = (int)employeeList.Max(e => e.AnuualSalary);
            Console.WriteLine($"Max Salaries of the employee are {max}");
            Console.WriteLine($"-----------------------------------------------------------------------");
            /////////////////////////////////////////////Generation Operation /////////////////////////////////////////
            ///// Empty
            List<Employee> employeeslist = Enumerable.Empty<Employee>().ToList();
            employeeslist.Add(new Employee
            {
                Id = 7,
                FirstName = "empty",
                LastName = "emptylast"
            });
            foreach (var item in employeeslist)
            {
                Console.WriteLine($"Empty id: {item.Id} - {item.FirstName} - {item.LastName} -salary: {item.AnuualSalary} Manager: {item.IsManager} departmentId: {item.DepartmentId}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");

            //Range
            var collectionRange = Enumerable.Range(10, 5);
            foreach (var item in collectionRange)
            {
                Console.WriteLine($"Range - {item}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");
            //Repeat
            var collectionRepeat = Enumerable.Repeat("test",4);
            foreach (var item in collectionRepeat)
            {
                Console.WriteLine($"Repeat - {item}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");

            /// set operator (distinct – union – except – intersect)
            /// Distinct
            var employeelistdistinct = employeeList.Distinct(new compareEmployee());
            foreach (var emp in employeelistdistinct)
            {
                Console.WriteLine($"Distinct : {emp.FirstName}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");
            //Except
            List<Employee> employeeList2 = Data.GetEmployees();//contains all the data like employeelist
            var employeeExcept = employeeList.Except(employeeList2, new compareEmployee());
            foreach (var emp in employeeExcept)
            {
                Console.WriteLine($"except {emp.FirstName} - {emp.LastName} Id : {emp.Id}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------");
            //intersect
            List<Employee> employeeList3 = new List<Employee>();//contains all the data like employeelist
            employeeList3.Add(new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Gendy",
                AnuualSalary = 4000.7m,
                IsManager = true,
                DepartmentId = 3
            });
            employeeList3.Add(new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnuualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            });
            var employeeintersect = employeeList.Intersect(employeeList3,new compareEmployee());
            foreach (var emp in employeeintersect)
            {
                Console.WriteLine($"Intersect {emp.FirstName} - {emp.LastName} Id : {emp.Id}");
            }

            Console.WriteLine($"-----------------------------------------------------------------------");

            //union
            var employeeunion = employeeList.Union(employeeList3,new compareEmployee());
            foreach (var emp in employeeunion)
            {
                Console.WriteLine($"union {emp.FirstName} - {emp.LastName} Id : {emp.Id}");
            }
  
            Console.ReadKey();

        }

    }


    ///// Data 
    public class compareEmployee : IEqualityComparer<Employee>
    {
        public bool Equals([AllowNull] Employee x, [AllowNull] Employee y)
        {
            if (x.Id == y.Id && x.FirstName.ToLower() == y.FirstName.ToLower() && x.LastName.ToLower() == y.LastName.ToLower())
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.Id.GetHashCode();
        }




    }
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal AnuualSalary { get; set; }

        public bool IsManager { get; set; }

        public int DepartmentId { get; set; }

    }

    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }

    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee()
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnuualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
             employee = new Employee()
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnuualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee()
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Gendy",
                AnuualSalary = 4000.7m,
                IsManager = true,
                DepartmentId = 3
            };
            employees.Add(employee);
            employee = new Employee()
            {
                Id = 3,
                FirstName = "John",
                LastName = "Gad",
                AnuualSalary = 5000.7m,
                IsManager = false,
                DepartmentId = 3
            };
            employees.Add(employee);
            employee = new Employee()
            {
                Id = 4,
                FirstName = "Peter",
                LastName = "Samuel",
                AnuualSalary = 8000.7m,
                IsManager = false,
                DepartmentId = 1
            };
            employees.Add(employee);
            return employees;
        }


        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            Department department = new Department()
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resource"
            };
            departments.Add(department);
            department = new Department()
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Financie"
            };
            departments.Add(department);
            department = new Department()
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);
            return departments;
        }


    }

}
