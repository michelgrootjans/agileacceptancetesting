using System.Collections.Generic;

namespace Snacks_R_Us.AcceptanceTests
{
    public static class ReflectionHelper
    {
        public static object AddObjectToQueryResult(object theObject)
        {
            var objectProperties = theObject.GetType().GetProperties();
            var oneResult = new List<object>();

            foreach (var currentInfo in objectProperties)
            {
                var oneProperty = new List<object>();
                var getMethod = currentInfo.GetGetMethod();
                var currName = getMethod.Name;
                var currValue = getMethod.Invoke(theObject, null);

                // TODO: Determine if Slim handles a NULL or not
                //       If so, is there a special keyword?
                var currValueString = (currValue == null)
                                          ? "null"
                                          : currValue.ToString();

                // Remove the 'get_' prefix from the property name.
                oneProperty.Add(currName.Substring(4));
                oneProperty.Add(currValueString);

                oneResult.Add(oneProperty);
            }
            return oneResult;
        }
    }
}