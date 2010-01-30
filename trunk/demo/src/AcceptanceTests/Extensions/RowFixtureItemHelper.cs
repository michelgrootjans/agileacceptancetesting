using System;
using System.Collections.Generic;
using System.Reflection;

namespace Snacks_R_Us.AcceptanceTests.Extensions
{
    internal class RowFixtureItemHelper<T>
    {
        private readonly IDictionary<string, Func<T, object>> customConverters;
        private readonly List<object> propertyValues = new List<object>();

        public RowFixtureItemHelper(T item, IDictionary<string, Func<T, object>> customConverters)
        {
            this.customConverters = customConverters;
            CalculateProperties(item);
        }

        public List<Object> Properties
        {
            get { return propertyValues; }
        }

        private void CalculateProperties(T theObject)
        {
            AddDefaultColumns(theObject);
            AddCustomColumns(theObject);
        }

        private void AddDefaultColumns(T theObject)
        {
            var objectProperties = theObject.GetType().GetProperties();

            foreach (var currentInfo in objectProperties)
            {
                var getterProperty = currentInfo.GetGetMethod();
                // Remove the 'get_' prefix from the property name.
                var propertyName = getterProperty.Name.Substring(4);
                var propertyValue = GetPropertyValue(getterProperty, theObject);

                AddProperty(propertyName, propertyValue);
            }
        }

        private void AddCustomColumns(T theObject)
        {
            foreach (var converter in customConverters)
            {
                AddProperty(converter.Key, converter.Value(theObject).ToString());
            }
        }

        private string GetPropertyValue(MethodInfo getterProperty, object theObject)
        {
            var propertyValue = getterProperty.Invoke(theObject, null);

            // TODO: Determine if Slim handles a NULL or not
            // If so, is there a special keyword?
            return (propertyValue == null)
                       ? "null"
                       : propertyValue.ToString();
        }

        private void AddProperty(string propertyName, string propertyValueString)
        {
            propertyValues.Add(GetPropertyNameAndValue(propertyName, propertyValueString));
            propertyValues.Add(GetPropertyNameAndValue(propertyName.ToLower(), propertyValueString));
            propertyValues.Add(GetPropertyNameAndValue(propertyName.ToTitle(), propertyValueString));

            var propertyNamesWithSpaces = propertyName.ToSentence();
            propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces, propertyValueString));
            propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces.ToLower(), propertyValueString));
            propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces.ToTitle(), propertyValueString));
        }

        private static List<object> GetPropertyNameAndValue(string propertyName, string propertyValueString)
        {
            return new List<object> { propertyName, propertyValueString };
        }
    }
}