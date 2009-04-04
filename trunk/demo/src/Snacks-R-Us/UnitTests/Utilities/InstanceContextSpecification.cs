using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using Snacks_R_Us.WebApp.IoC;

namespace Snacks_R_Us.UnitTests.Utilities
{
    [TestFixture]
    public abstract class InstanceContextSpecification<SUT>
    {
        protected SUT sut;
        private List<object> dependencies;

        [SetUp]
        public void SetUp()
        {
            dependencies = new List<object>();
            Container.InitializeWith(new DictionaryContainer(dependencies));
            
            Arrange();
            sut = CreateSystemUnderTest();
            Act();
        }

        [TearDown]
        public void TearDown()
        {
            AfterEachTest();
        }

        protected virtual void Arrange() {}
        protected abstract SUT CreateSystemUnderTest();
        protected virtual void Act() {}
        protected void AfterEachTest() {}

        protected T Dependency<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        protected T RegisterDependencyInContainer<T>() where T : class
        {
            var dependency = MockRepository.GenerateStub<T>();
            dependencies.Add(dependency);
            return dependency;
        }

        protected IStubSpecification<Target> When<Target>(Target target) where Target : class
        {
            return new StubSpecification<Target>(target);
        }
    }
}