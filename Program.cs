using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualBasic;

// Simple program CRUD Employee Management System
namespace simpleCrudEmployee
{
    public class Employee{
        public string? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }

    // Class interface program
    public interface IOperation {
        void AddEmployee(Employee employee);
        void ViewEmployee();
        void DeleteEmployee(string employeeId);
    }

    // Class to implement the IOperation interface
    public class EmployeeManagement : IOperation
    {
        private readonly Dictionary<string, Employee> _employees = new Dictionary<string, Employee>();

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            if (employee.EmployeeId != null && _employees.ContainsKey(employee.EmployeeId))
                throw new InvalidOperationException("An employee with this ID already exists");

            if (employee.EmployeeId != null)
            {
                _employees[employee.EmployeeId] = employee;
            }
            else
            {
                throw new ArgumentNullException(nameof(employee.EmployeeId), "Employee ID cannot be null");
            }
            Console.WriteLine("Employee added successfully");
        }

        public void DeleteEmployee(string employeeId)
        {

            if (string.IsNullOrEmpty(employeeId))
            {
                throw new ArgumentException("EmployeeId cannot be null or empty", nameof(employeeId));
            }

            if (!_employees.ContainsKey(employeeId))
            {
                throw new InvalidOperationException("Employee not found");
            }

            if (_employees.ContainsKey(employeeId))
            {
                _employees.Remove(employeeId);
                Console.WriteLine("Employee deleted successfully");
            }
        }

        public void ViewEmployee()
        {
            if (_employees.Count == 0)
            {
                Console.WriteLine("No employee found");
            }
            else
            {
                foreach (var employee in _employees.Values)
                {
                    Console.WriteLine($"| ID: {employee.EmployeeId} | Name: {employee.FullName} | Birth Date: {employee.BirthDate.ToString("dd-MMM-yyyy")}|");
                }
            }
        }
    }

    // Main program application to run the Employee Management System
    class Program {
        static void Main(string[] args) {
            var manage = new EmployeeManagement();

            string dateFormat = "dd-MMM-yyyy";

            // function to check input
            static string checkInput(string inputFromConsole) {
                string? input;
            
                do {
                    Console.Write(inputFromConsole);
                    input = Console.ReadLine();
            
                    if (string.IsNullOrWhiteSpace(input)) {
                        Console.WriteLine("Input cannot be empty");
                    }
            
                } while (string.IsNullOrWhiteSpace(input));
            
                return input!;
            }

            while (true) {
                Console.WriteLine("\nEmployee Management System");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. View Employees");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                var option = Console.ReadLine();

                try {
                    switch (option) {
                        case "1":
                            var employeeId = checkInput("Enter Employee ID: ");

                            var fullName = checkInput("Enter Full Name: ");

                            string birthDateStr;
                            DateTime birthDate;

                            // Validate date format
                            do {
                                birthDateStr = checkInput($"Enter Birth Date ({dateFormat}): (17-Aug-2001): ");
                                if (!DateTime.TryParseExact(birthDateStr, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
                                {
                                    Console.WriteLine("Invalid date format");
                                }
                            } while (!DateTime.TryParseExact(birthDateStr, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate));

                            var employee = new Employee {
                                EmployeeId = employeeId,
                                FullName = fullName,
                                BirthDate = birthDate
                            };
                            manage.AddEmployee(employee);
                            break;
                        case "2":
                            manage.ViewEmployee();
                            break;
                        case "3":
                            Console.Write("Enter Employee ID: ");
                            var id = checkInput("Enter Employee ID: ");
                            manage.DeleteEmployee(id);
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                } catch (Exception e) {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }
    }
}
