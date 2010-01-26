using System.Collections.Generic;
using System.Reflection;

namespace Snacks_R_Us.AcceptanceTests
{
	public static class ReflectionHelper
	{
		public static object AddObjectToQueryResult(object theObject)
		{
			PropertyInfo[] objectProperties = theObject.GetType().GetProperties();
			var oneResult = new List<object>();

			foreach (PropertyInfo currentInfo in objectProperties)
			{
				var oneProperty = new List<object>();
				MethodInfo getMethod = currentInfo.GetGetMethod();
				string currName = getMethod.Name;
				object currValue = getMethod.Invoke(theObject, null);

				// TODO: Determine if Slim handles a NULL or not
				//       If so, is there a special keyword?
				string currValueString = (currValue == null)
										 ? "null" : currValue.ToString();

				// Remove the 'get_' prefix from the property name.
				oneProperty.Add(currName.Substring(4));
				oneProperty.Add(currValueString);

				oneResult.Add(oneProperty);
			}
			return oneResult;
		}
	}
}