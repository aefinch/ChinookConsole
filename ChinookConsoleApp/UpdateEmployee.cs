using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ChinookConsoleApp
{
	class UpdateEmployee
	{
		public void Update()
		{
			var whichRecord = new ListEmployees().List("Select the employee record to update");
			using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
			{
				try
				{
					connection.Open();

					/*Console.WriteLine("Select the employee record to update.");
					Console.Write(">");*/
					Console.WriteLine("Enter first name:");
					var x = Console.ReadLine();
					Console.WriteLine("Enter last name:");
					var y = Console.ReadLine();
					
					var rowsAffected = connection.Execute( "Update Employee " +
																"Set FirstName = @firstname, LastName = @lastname " +
																"Where EmployeeId = @employeeId", new { FirstName = x, LastName = y, EmployeeId = whichRecord });
					Console.WriteLine(rowsAffected != 1 ? "Update Failed" : "Success!");

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.StackTrace);
				}
			}
		}
	}
}
