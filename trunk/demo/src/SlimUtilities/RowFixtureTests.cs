using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SlimUtilities
{
    [TestFixture]
    public class RowFixtureTests
    {
        private List<RowFixtureTestClass> sut;
        private const int TestAge = 21;
        private const string TestFirstName = "FirstName";
        private const string TestName = "Name";

        [SetUp]
        public void SetUp()
        {
            sut = new List<RowFixtureTestClass>
                      {
                          new RowFixtureTestClass {Name = TestName, FirstName = TestFirstName, Age = TestAge}
                      };
        }

        [Test]
        public void ReadStringProperty()
        {
            sut.ToRowFixture()
                .ShouldContainColumn("Name", TestName)
                .ShouldContainColumn("FirstName", TestFirstName)
                .ShouldContainColumn("Age", TestAge.ToString());
        }

        [Test]
        public void ReadLowercaseProperty()
        {
            sut.ToRowFixture()
                .ShouldContainColumn("name", TestName)
                .ShouldContainColumn("firstname", TestFirstName)
                .ShouldContainColumn("age", TestAge.ToString());
        }

        [Test]
        public void ReadPropertyWithSpaces()
        {
            sut.ToRowFixture().ShouldContainColumn("First Name", TestFirstName);
        }

        [Test]
        public void ReadTilteCaseWithSpaces()
        {
            sut.ToRowFixture().ShouldContainColumn("first name", TestFirstName);
        }

        [Test]
        public void ReadLowerCaseWithSpaces()
        {
            sut.ToRowFixture().ShouldContainColumn("First name", TestFirstName);
        }

        [Test]
        public void Read_Custom_FullName_Column()
        {
            var expectedFullName = TestName + TestFirstName;
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
                .ShouldContainColumn("IsAdult", "True")
                .ShouldContainColumn("Is Adult", "True")
                .ShouldContainColumn("Is adult", "True")
                .ShouldContainColumn("is adult", "True");
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