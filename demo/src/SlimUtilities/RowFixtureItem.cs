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
                AddProperty(converter.Key, converter.Value(item));
        }

        private object GetPropertyValue(MethodInfo getterProperty)
        {
            return getterProperty.Invoke(item, null);
        }

        private void AddProperty(string propertyName, object propertyValue)
        {
            propertyValues.Add(GetPropertyNameAndValue(propertyName, propertyValue));
            propertyValues.Add(GetPropertyNameAndValue(propertyName.ToLower(), propertyValue));
            propertyValues.Add(GetPropertyNameAndValue(propertyName.ToTitle(), propertyValue));

            var propertyNamesWithSpaces = propertyName.ToSentence();
            propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces, propertyValue));
            propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces.ToLower(), propertyValue));
            propertyValues.Add(GetPropertyNameAndValue(propertyNamesWithSpaces.ToTitle(), propertyValue));
        }

        private static List<object> GetPropertyNameAndValue(string propertyName, object propertyValueString)
        {
            return new List<object> { propertyName, propertyValueString };
        }
    }
}