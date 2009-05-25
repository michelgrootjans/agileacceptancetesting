using fitlibrary;
using Snacks_R_Us.Domain;

namespace Snacks_R_Us.AcceptanceTests
{
	public class Insert : DoFixture
	{
		private const string Ok = "Ok";

		public Insert()
		{
			Fitnesse.Reset();
		}

		public string SampleData()
		{
			SampleUsers();
			SampleSnacks();
			return Ok;
		}

		public string SampleUsers()
		{
			DemoData.AddDemoUsers();
			return Ok;
		}

		public string SampleSnacks()
		{
			DemoData.AddDemoSnacks();
			return Ok;
		}

		public void AddSnackWithPrice(string snackName, double price)
		{
			DemoData.AddSnack(snackName, price);
		}

		public void AddUserWithPassword(string userName, string password)
		{
			DemoData.AddDeveloper(userName, password, null, 0);
		}
	}
}