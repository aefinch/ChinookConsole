using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace ChinookConsoleApp
{
	public class SalesByEmployeeResults
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Sales { get; set; }
	}
	internal class EmployeeSales
	{
		public object SumSales()
		{
			Console.WriteLine("Enter a date range to review employee sales:");
			Console.Write("Beginning Date: ");
			var firstDate = Console.ReadLine();
			Console.Write("End Date: ");
			var secondDate = Console.ReadLine();
			using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
			{
				try
				{
					connection.Open();
					var results = connection.Query<SalesByEmployeeResults>("Select e.employeeId as Id, " +
													"e.firstname + ' ' + e.lastname as fullname, " +
													"sum(i.Total) as Sales" +
						                            " from Employee as e " +
													"join Customer as c on c.SupportRepId = e.EmployeeId " +
													"join Invoice as i on i.CustomerId = c.CustomerId " +
													"Where i.InvoiceDate Between @firstDate And @secondDate " +
													"Group by e.EmployeeId, e.FirstName, e.LastName", new { @firstDate = firstDate, @secondDate = secondDate });

					/*if (Convert.ToInt32(whichYear) < 2009 || Convert.ToInt32(whichYear) > 2013)
					{
						Console.WriteLine($"There were no sales in year {whichYear}.");
					}
					else
					{
					}*/
						foreach (var employee in results)
						{
							Console.WriteLine($"{employee.Id}.) {employee.FullName} {employee.Sales}");
						}

					Console.WriteLine("Press enter to return to the menu");
					Console.ReadLine();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.StackTrace);
				}
				
				return 0;
			}
		}
	}
}