using CQRS.Mediatr.Lite.SDK.Domain;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks.Domain
{
    [ExcludeFromCodeCoverage]
    internal class MockEnumType: Enumeration
    {
        public static MockEnumType TypeI = new MockEnumType1();
        public static MockEnumType TypeII = new MockEnumType2();

        public MockEnumType(): base() { }
        protected MockEnumType(int code, string name) : base(code, name) { }

        private class MockEnumType1: MockEnumType
        {
            public MockEnumType1() : base(1, "Type-I") { }
        }

        private class MockEnumType2 : MockEnumType
        {
            public MockEnumType2() : base(2, "Type-II") { }
        }
    }
}
