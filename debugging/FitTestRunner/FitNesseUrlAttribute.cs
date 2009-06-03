namespace Fit.TestRunner
{
	using System;

	public class FitNesseUrlAttribute : Attribute
	{
		string url;

		public FitNesseUrlAttribute(string url)
		{
			this.url = url;
		}

		public string Url
		{
			get
			{
				return this.url;
			}
		}
	}
}
