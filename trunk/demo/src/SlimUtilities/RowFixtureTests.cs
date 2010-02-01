using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SlimUtilities
{
    [TestFixture]
    public class RowFixtureTests
    {
        private List<RowFixtureTestClass> sut;
        private const int TEST_AGE = 21;
        private const string TEST_FIRST_NAME = "FirstName";
        private const string TEST_NAME = "Name";

        [SetUp]
        public void SetUp()
        {
            sut = new List<RowFixtureTestClass>
                      {
                          new RowFixtureTestClass {Name = TEST_NAME, FirstName = TEST_FIRST_NAME, Age = TEST_AGE}
                      };
        }

        [Test]
        public void ReadStringProperty()
        {
            sut.ToRowFixture()
                .ShouldContainColumn("Name", TEST_NAME)
                .ShouldContainColumn("FirstName", TEST_FIRST_NAME)
                .ShouldContainColumn("Age", TEST_AGE);
        }

        [Test]
        public void ReadLowercaseProperty()
        {
            sut.ToRowFixture()
                .ShouldContainColumn("name", TEST_NAME)
                .ShouldContainColumn("firstname", TEST_FIRST_NAME)
                .ShouldContainColumn("age", TEST_AGE);
        }

        [Test]
        public void ReadPropertyWithSpaces()
        {
            sut.ToRowFixture().ShouldContainColumn("First Name", TEST_FIRST_NAME);
        }

        [Test]
        public void ReadTilteCaseWithSpaces()
        {
            sut.ToRowFixture().ShouldContainColumn("first name", TEST_FIRST_NAME);
        }

        [Test]
        public void ReadLowerCaseWithSpaces()
        {
            sut.ToRowFixture().ShouldContainColumn("First name", TEST_FIRST_NAME);
        }

        [Test]
        public void Read_Custom_FullName_Column()
        {
            var expectedFullName = TEST_NAME + TEST_FIRST_NAME;
            sut.ToRowFixture()
                .AddColumn("FullName", dto => dto.Name + dto.FirstName)
                .ShouldContainColumn("FullName", expectedFullName)
                .ShouldContainColumn("Full Name", expectedFullName)
                .ShouldContainColumn("Full name", expectedFullName)
                .ShouldContainColumn("full name", expectedFullName);
        }

        [Test]
        public void Read_Custom_Adult_Column()
        {
            sut.ToRowFixture()
                .AddColumn("IsAdult", dto => dto.Age >= 21)
                .ShouldContainColumn("IsAdult", true)
                .ShouldContainColumn("Is Adult", true)
                .ShouldContainColumn("Is adult", true)
                .ShouldContainColumn("is adult", true);
        }
    }

    internal class RowFixtureTestClass
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public int Age { get; set; }
    }

    internal static class TestExtensions
    {
        public static IRowFixture<T> ShouldContainColumn<T>(this IRowFixture<T> source, string columnName, object columnValue)
        {
            var foundColumn = false;
            foreach (var item in source.ToList())
            {
                var listOfProperties = item as List<Object>;
                foreach (var keyValuePair in listOfProperties)
                {
                    var property = keyValuePair as List<Object>;
                    
                    if (!property[0].Equals(columnName)) continue;
                    
                    foundColumn = true;
                    if (property[1].Equals(columnValue))
                        return source;
                }
            }
            if (foundColumn)
                Assert.Fail(string.Format("Couldn't find value [{1}] in column [{0}]", columnName, columnValue));
            else
                Assert.Fail(string.Format("Couldn't find column [{0}]", columnName));
            return source;
        }
    }
}