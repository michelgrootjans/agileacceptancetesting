using System;
using System.Collections.Generic;

namespace Snacks_R_Us.WebApp.IoC
{
    public class DictionaryContainer : IContainer
    {
        private readonly IEnumerable<object> services;

        public DictionaryContainer(IEnumerable<object> services)
        {
            this.services = services;
        }

        public T GetImplementationOf<T>()
        {
            foreach (var service in services)
            {
                if (typeof(T).IsAssignableFrom(service.GetType()))
                    return (T) service;
            }
            throw new ArgumentException(string.Format("Couldn't find an implementation of '{0}'", typeof(T).FullName));
        }
    }
}