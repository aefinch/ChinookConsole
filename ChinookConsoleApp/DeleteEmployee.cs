using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ChinookConsoleApp
{
	class DeleteEmployee
	{
		public void Delete()
		{
			var employeeList = new ListEmployees();
			var firedEmployee = employeeList.List("Pick an employee to fire");
			Console.WriteLine($"You picked {firedEmployee}");
			using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
			{
				
				try
				{
					connection.Open();
					var rowsAffected = connection.Execute("Delete from Employee " +
														  "Where EmployeeId = @employeeId", new { employeeId = firedEmployee });

					/*Console.WriteLine("Select the employee record to delete.");
					Console.Write(">");
					var selectedEmployee = Convert.ToInt32(Console.ReadLine());*/
					
					
					Console.WriteLine(rowsAffected != 1 ? "Delete Failed" : "Success!");
					Console.WriteLine("Press eneter to return to the menu");
					Console.ReadLine();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.StackTrace);
				}
				Console.WriteLine("Press eneter to return to the menu");
				Console.ReadLine();
			}
		}
	}
}
