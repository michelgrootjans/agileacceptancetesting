using System;
using System.Collections.Generic;

namespace Snacks_R_Us.AcceptanceTests.Extensions
{
    public static class IEnumerableExtensions
    {
        public static List<Object> ToRowFixture<T>(this IEnumerable<T> list)
        {
            var result = new List<object>();
            
            foreach (var item in list)
                result.Add(new RowFixtureItemHelper(item).PropertyValues);

            return result;
        }

        private class RowFixtureItemHelper
        {
            private readonly List<object> propertyValues = new List<object>();

            public RowFixtureItemHelper(object item)
            {
                CalculatePropertyValues(item);
            }

            public object PropertyValues
            {
                get { return propertyValues; }
            }

            private void CalculatePropertyValues(object theObject)
            {
                var objectProperties = theObject.GetType().GetProperties();

                foreach (var currentInfo in objectProperties)
                {
                    var getterProperty = currentInfo.GetGetMethod();
                    var propertyName = getterProperty.Name.Substring(4);
                    // Remove the 'get_' prefix from the property name.
                    var propertyValue = getterProperty.Invoke(theObject, null);

                    // TODO: Determine if Slim handles a NULL or not
                    // If so, is there a special keyword?
                    var propertyValueString = (propertyValue == null)
                                                  ? "null"
                                                  : propertyValue.ToString();

                    propertyValues.Add(GetPropertyNameAndValue(propertyName, propertyValueString));
                    propertyValues.Add(GetPropertyNameAndValue(propertyName.ToLower(), propertyValueString));
                    propertyValues.Add(GetPropertyNameAndValue(propertyName.ToTitle(), propertyValueString));

                    var propertyNamesWithSpaces = propertyName.ToSentence();
                    propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces, propertyValueString));
                    propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces.ToLower(), propertyValueString));
                    propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces.ToTitle(), propertyValueString));
                }
            }

            private static List<object> GetPropertyNameAndValue(string propertyName, string propertyValueString)
            {
                return new List<object> {propertyName, propertyValueString};
            }
        }
    }
}