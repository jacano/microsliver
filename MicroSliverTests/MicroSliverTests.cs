using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroSliver;
using MicroSliverTests.TestClasses;

namespace TestProject1
{
    [TestClass]
    public class MicroSliverTests
    {
        [TestMethod]
        public void TestShallowInstancesWithoutCreator()
        {
            var ioc = new IoC();
            ioc.Map<IDependencyA, ClassDependencyA>();

            var classA1 = ioc.Get<ClassParentA>();
            var classA2 = ioc.Get<ClassParentA>();

            Assert.AreNotEqual(classA1.A, classA2.A);
        }

        [TestMethod]
        public void TestShallowSingletonsWithoutCreator()
        {
            var ioc = new IoC();
            ioc.MapToSingleton<IDependencyA, ClassDependencyA>();

            var classA1 = ioc.Get<ClassParentA>();
            var classA2 = ioc.Get<ClassParentA>();

            Assert.AreEqual(classA1.A, classA2.A);
        }

        [TestMethod]
        public void TestShallowMultipleInstancesWithoutCreator()
        {
            var ioc = new IoC();
            ioc.Map<IDependencyA, ClassDependencyA>();
            ioc.Map<IDependencyB, ClassDependencyB>();


            var classB1 = ioc.Get<ClassParentB>();
            var classB2 = ioc.Get<ClassParentB>();

            Assert.AreNotEqual(classB1.A, classB2.A);
            Assert.AreNotEqual(classB1.B, classB2.B);
        }

        [TestMethod]
        public void TestShallowMultipleSingletonsWithoutCreator()
        {
            var ioc = new IoC();
            ioc.MapToSingleton<IDependencyA, ClassDependencyA>();
            ioc.MapToSingleton<IDependencyB, ClassDependencyB>();

            var classB1 = ioc.Get<ClassParentB>();
            var classB2 = ioc.Get<ClassParentB>();

            Assert.AreEqual(classB1.A, classB2.A);
            Assert.AreEqual(classB1.B, classB2.B);
        }

        [TestMethod]
        public void TestShallowMultipleSingletonsWithCreator()
        {
            var ioc = new IoC();
            ioc.MapToSingleton<IDependencyA>(new ClassDependencyACreator());
            ioc.MapToSingleton<IDependencyB>(new ClassDependencyBCreator());

            var classB1 = ioc.Get<ClassParentB>();
            var classB2 = ioc.Get<ClassParentB>();

            Assert.AreEqual(classB1.A, classB2.A);
            Assert.AreEqual(classB1.B, classB2.B);
        }

        [TestMethod]
        public void TestShallowMultipleDifferentsWithCreator()
        {
            var ioc = new IoC();
            ioc.MapToSingleton<IDependencyA>(new ClassDependencyACreator());
            ioc.Map<IDependencyB>(new ClassDependencyBCreator());

            var classB1 = ioc.Get<ClassParentB>();
            var classB2 = ioc.Get<ClassParentB>();

            Assert.AreEqual(classB1.A, classB2.A);
            Assert.AreNotEqual(classB1.B, classB2.B);
        }


        [TestMethod]
        public void TestDeepSingleInstanceWithoutCreator()
        {
            var ioc = new IoC();
            ioc.Map<IDependencyC, ClassDependencyC>();
            ioc.Map<IDependencyE, ClassDependencyE>();

            var classC1 = ioc.Get<ClassParentC>();
            var classC2 = ioc.Get<ClassParentC>();

            Assert.AreNotEqual(classC1.C, classC2.C);
            Assert.AreNotEqual(classC1.C.E, classC2.C.E);
        }

        [TestMethod]
        public void TestDeepSingleSingletonsWithoutCreator()
        {
            var ioc = new IoC();
            ioc.MapToSingleton<IDependencyC, ClassDependencyC>();
            ioc.MapToSingleton<IDependencyE, ClassDependencyE>();

            var classC1 = ioc.Get<ClassParentC>();
            var classC2 = ioc.Get<ClassParentC>();

            Assert.AreEqual(classC1.C, classC2.C);
            Assert.AreEqual(classC1.C.E, classC2.C.E);
        }

        [TestMethod]
        public void TestDeepSingleSingletonsWithCreator()
        {
            var ioc = new IoC();
            ioc.MapToSingleton<IDependencyC, ClassDependencyC>();
            ioc.MapToSingleton<IDependencyE>(new ClassDependencyECreator());

            var classC1 = ioc.Get<ClassParentC>();
            var classC2 = ioc.Get<ClassParentC>();

            Assert.AreEqual(classC1.C, classC2.C);
            Assert.AreEqual(classC1.C.E, classC2.C.E);
        }

        [TestMethod]
        public void TestDeepSingleDifferentWithCreator()
        {
            var ioc = new IoC();
            ioc.Map<IDependencyC, ClassDependencyC>();
            ioc.MapToSingleton<IDependencyE>(new ClassDependencyECreator());

            var classC1 = ioc.Get<ClassParentC>();
            var classC2 = ioc.Get<ClassParentC>();

            Assert.AreNotEqual(classC1.C, classC2.C);
            Assert.AreEqual(classC1.C.E, classC2.C.E);
        }
    }

    public class ClassDependencyACreator : ICreator
    {
        public object Create()
        {
            return new ClassDependencyA();
        }
    }

    public class ClassDependencyBCreator : ICreator
    {
        public object Create()
        {
            return new ClassDependencyB();
        }
    }

    public class ClassDependencyECreator : ICreator
    {
        public object Create()
        {
            return new ClassDependencyE();
        }
    }
}
