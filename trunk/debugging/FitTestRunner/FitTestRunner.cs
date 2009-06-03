using System;
using System.Collections.Generic;
using System.Text;
using TestDriven.Framework;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using fit;

namespace Fit.TestRunner
{
	public class FitTestRunner : ITestRunner
	{
		public const string ReportHeader = @"<html>
	<head>
		<title>Test Results: FrontPage.OrderCreation</title>
		<link rel=""stylesheet"" type=""text/css"" href=""{0}/files/css/fitnesse.css"" media=""screen""/>
		<link rel=""stylesheet"" type=""text/css"" href=""{0}/files/css/fitnesse_print.css"" media=""print""/>
		<script src=""{0}/files/javascript/fitnesse.js"" type=""text/javascript""></script>
	</head>
	<body>";

		public const string ReportFooter = @"</body>
</html>";

		#region ITestRunner Members

		public TestRunState RunAssembly(ITestListener testListener, System.Reflection.Assembly assembly)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public TestRunState RunMember(ITestListener testListener, System.Reflection.Assembly assembly, System.Reflection.MemberInfo member)
		{
			Type type = member as Type;

			if (type == null)
			{
				type = member.ReflectedType;
			}

			if (type == null)
			{
				return TestRunState.NoTests;
			}

			object[] attributes = type.GetCustomAttributes(typeof(FitNesseUrlAttribute), true);
			if(attributes == null || attributes.Length == 0)
			{
				return TestRunState.NoTests;
			}
			FitNesseUrlAttribute attribute = (FitNesseUrlAttribute)attributes[0];
			string url = attribute.Url;

			Counts counts = this.RunFitnesse(testListener, url);

			TestResult testResult = new TestResult();
			testResult.FixtureType = type;
			
			testResult.Name = type.FullName;
			if (counts.Wrong > 0 || counts.Exceptions > 0)
				testResult.State = TestState.Failed;
			else
				testResult.State = TestState.Passed;

			testResult.TotalTests = counts.Exceptions + counts.Ignores + counts.Right + counts.Wrong;
			
			testListener.TestFinished(testResult);

			testListener.TestResultsUrl(url);

			return TestRunState.Success;
		}

		private Counts RunFitnesse(ITestListener testListener, string url)
		{
			Uri uri = new Uri(url);
			string pageName = uri.AbsolutePath.TrimStart("/".ToCharArray());
			string[] args = new string[] { "-v", uri.Host, uri.Port.ToString(), pageName };
			fitnesse.fitserver.TestRunner runner = new fitnesse.fitserver.TestRunner();
			StringBuilder ResultBuilder = new StringBuilder();
			//runner.cacheWriter = new StringWriter(ResultBuilder);
			runner.Run(args);
			this.CreateHtmlReport(testListener, ResultBuilder, string.Format("http://{0}:{1}", uri.Host, uri.Port));
			return runner.pageCounts;
		}

		private void CreateHtmlReport(ITestListener testListener, StringBuilder ResultBuilder, string fitNesseBaseUrl)
		{
			try
			{
				string ReportFilePath = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
					"FitReport.html");

				using (TextWriter ReportWriter = new StreamWriter(ReportFilePath))
                {
					ReportWriter.Write(string.Format(ReportHeader, fitNesseBaseUrl));
					ReportWriter.Write(ResultBuilder.ToString());
					ReportWriter.Write(ReportFooter);
                }


				Uri ReportUri = new Uri("file:" + Path.GetFullPath(ReportFilePath));
				testListener.TestResultsUrl(ReportUri.AbsoluteUri);
			}
			catch (Exception ex)
			{
				testListener.WriteLine("failed to create reports", Category.Error);
				testListener.WriteLine(ex.ToString(), Category.Error);
			}
		}

		public TestRunState RunNamespace(ITestListener testListener, System.Reflection.Assembly assembly, string ns)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
