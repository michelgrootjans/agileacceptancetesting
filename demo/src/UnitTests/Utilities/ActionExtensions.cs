using System;

namespace Snacks_R_Us.UnitTests.Utilities
{
    public static class ActionExtensions
    {
        public static T ShouldThrow<T>(this Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch(T t)
            {
                return t;
            }
            catch(Exception e)
            {
                throw new ArgumentException(string.Format("Expected an exception of type {0}, but got an exception of type {1}: {2}",
                    typeof(T), e.Message, e));
            }
            throw new ArgumentException(string.Format("Expected an exception of type {0}, but got none.", typeof(T)));
        }
    }
}