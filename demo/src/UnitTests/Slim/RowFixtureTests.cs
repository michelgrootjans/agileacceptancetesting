using System;
using System.Collections.Generic;
using NUnit.Framework;
using Snacks_R_Us.AcceptanceTests.Extensions;

namespace Snacks_R_Us.UnitTests.Slim
{
    [TestFixture]
    public class RowFixtureTests
    {
        private List<RowFixtureTestClass> sut;
        private const string TestFirstName = "FirstName";
        private const string TestName = "Name";

        [SetUp]
        public void SetUp()
        {
            var item1 = new RowFixtureTestClass {Name = TestName, FirstName = TestFirstName};
            sut = new List<RowFixtureTestClass> {item1};
        }

        [Test]
        public void ReadStringProperty()
        {
            sut.ToRowFixture().ShouldContainColumn("Name", TestName);
            sut.ToRowFixture().ShouldContainColumn("FirstName", TestFirstName);
        }

        [Test]
        public void ReadLowercaseProperty()
        {
            sut.ToRowFixture().ShouldContainColumn("name", TestName);
            sut.ToRowFixture().ShouldContainColumn("FirstName", TestFirstName);
            sut.ToRowFixture().ShouldContainColumn("Firstname", TestFirstName);
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
    }

    internal class RowFixtureTestClass
    {
        public string Name { get; set; }

        public string FirstName { get; set; }
    }

    internal static class TestExtensions
    {
        public static void ShouldContainColumn(this List<Object> source, string columnName, object columnValue)
        {
            var foundColumn = false;
            foreach (var item in source)
            {
                var listOfProperties = item as List<Object>;
                foreach (var keyValuePair in listOfProperties)
                {
                    var property = keyValuePair as List<Object>;
                    
                    if (!property[0].Equals(columnName)) continue;
                    
                    foundColumn = true;
                    if (property[1].Equals(columnValue))
                        return;
                }
            }
            if (foundColumn)
                Assert.Fail(string.Format("Couldn't find value [{1}] in column [{0}]", columnName, columnValue));
            else
                Assert.Fail(string.Format("Couldn't find column [{0}]", columnName));
        }
    }
}