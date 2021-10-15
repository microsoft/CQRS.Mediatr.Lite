using System;
using CQRS.Mediatr.Lite.SDK.Domain;
using System.Diagnostics.CodeAnalysis;
using CQRS.Mediatr.Lite.Tests.Mocks.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CQRS.Mediatr.Lite.Tests.UnitTests.Domain
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EnumerationUnitTests
    {
        [TestMethod]
        public void Enumeration_ShouldHaveBasicProperties()
        {
            MockEnumType type1 = MockEnumType.TypeI;
            Assert.AreEqual(1, type1.Code);
            Assert.AreEqual("Type-I", type1.Name);
        }

        [TestMethod]
        public void Enumeration_ToString_ShouldReturnName()
        {
            MockEnumType type1 = MockEnumType.TypeI;
            Assert.AreEqual(type1.Name, type1.ToString());
        }

        [TestMethod]
        public void Enumeration_ShouldGetCreated_FromCode()
        {
            MockEnumType type1 = Enumeration.FromCode<MockEnumType>(1);
            
            
            Assert.AreEqual(1, type1.Code);
            Assert.AreEqual("Type-I", type1.Name);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Enumeration_ShouldThrowException_ForInvalidCode()
        {
            MockEnumType _ = Enumeration.FromCode<MockEnumType>(100);
        }

        [TestMethod]
        public void Enumeration_ShouldGetCreated_FromName()
        {
            MockEnumType type1 = Enumeration.FromName<MockEnumType>("Type-I");


            Assert.AreEqual(1, type1.Code);
            Assert.AreEqual("Type-I", type1.Name);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Enumeration_ShouldThrowException_ForInvalidName()
        {
            MockEnumType _ = Enumeration.FromName<MockEnumType>("INVALID");
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Enumeration_ShouldThrowException_ForNullName()
        {
            MockEnumType _ = Enumeration.FromName<MockEnumType>(null);
        }

        [TestMethod]
        public void Enumeration_ShouldCreateDefault_WhenNameDoesntExist()
        {
            MockEnumType @default = MockEnumType.TypeII;
            MockEnumType type1 = Enumeration.FromName<MockEnumType>("INVALID", @default);


            Assert.AreEqual(2, type1.Code);
            Assert.AreEqual("Type-II", type1.Name);
        }

        [TestMethod]
        public void Enumeration_Equals_ShouldReturnTrue_ForSameEnum()
        {
            MockEnumType type1_obj1 = Enumeration.FromName<MockEnumType>("Type-I");
            MockEnumType type1_obj2 = Enumeration.FromName<MockEnumType>("Type-I");

            Assert.AreEqual(type1_obj1, type1_obj2);
        }

        [TestMethod]
        public void Enumeration_Equals_ShouldReturnFalse_ForDiffEnum()
        {
            MockEnumType type1 = Enumeration.FromName<MockEnumType>("Type-I");
            MockEnumType type2 = Enumeration.FromName<MockEnumType>("Type-II");

            Assert.AreNotEqual(type1, type2);
        }

        [TestMethod]
        public void Enumeration_Equals_ShouldReturnFalse_ForDiffObjects()
        {
            MockEnumType type1 = Enumeration.FromName<MockEnumType>("Type-I");
            Assert.AreNotEqual(type1, new { });
        }

        [TestMethod]
        public void Enumeration_ShouldGetSameHashCode_ForSameEnum()
        {
            MockEnumType type1_obj1 = Enumeration.FromName<MockEnumType>("Type-I");
            MockEnumType type1_obj2 = Enumeration.FromName<MockEnumType>("Type-I");

            Assert.AreEqual(type1_obj1.GetHashCode(), type1_obj2.GetHashCode());
        }

        [TestMethod]
        public void Enumeration_Equals_ShouldCompare_SameEnum()
        {
            MockEnumType type1_obj1 = Enumeration.FromName<MockEnumType>("Type-I");
            MockEnumType type1_obj2 = Enumeration.FromName<MockEnumType>("Type-I");

            Assert.AreEqual(0, type1_obj1.CompareTo(type1_obj2));
        }
    }
}
