using NUnit.Framework;

namespace Snacks_R_Us.UnitTests.Utilities
{
    public static class ObjectExtensions
    {
        public static void ShouldBeEqualTo(this object actual, object expected)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldNotBeEqualTo(this object actual, object expected)
        {
            Assert.AreNotEqual(expected, actual);
        }

        public static void ShouldBeSameAs(this object actual, object expected)
        {
            Assert.AreSame(expected, actual);
        }

        public static T ShouldBeOfType<T>(this object actual)
        {
            Assert.IsInstanceOfType(typeof(T), actual);
            return (T) actual;
        }

        public static void ShouldBeNull(this object actual)
        {
            Assert.IsNull(actual);
        }

        public static void ShouldNotBeNull(this object actual)
        {
            Assert.IsNotNull(actual);
        }
    }
}