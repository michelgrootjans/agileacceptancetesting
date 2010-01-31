using System;
using System.Collections.Generic;
using System.Reflection;

namespace SlimUtilities
{
    internal class RowFixtureItem<T>
    {
        private readonly IDictionary<string, Func<T, object>> customConverters;
        private readonly List<object> propertyValues = new List<object>();
        private readonly T item;

        public RowFixtureItem(T item, IDictionary<string, Func<T, object>> customConverters)
        {
            this.customConverters = customConverters;
            this.item = item;
            CalculateProperties();
        }

        public List<Object> Properties
        {
            get { return propertyValues; }
        }

        private void CalculateProperties()
        {
            AddDefaultColumns();
            AddCustomColumns();
        }

        private void AddDefaultColumns()
        {
            var objectProperties = item.GetType().GetProperties();

            foreach (var currentInfo in objectProperties)
            {
                var getterProperty = currentInfo.GetGetMethod();
                // Remove the 'get_' prefix from the property name.
                var propertyName = getterProperty.Name.Substring(4);
                var propertyValue = GetPropertyValue(getterProperty);

                AddProperty(propertyName, propertyValue);
            }
        }

        private void AddCustomColumns()
        {
            foreach (var converter in customConverters)
                AddProperty(converter.Key, converter.Value(item).ToString());
        }

        private string GetPropertyValue(MethodInfo getterProperty)
        {
            var propertyValue = getterProperty.Invoke(item, null);

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