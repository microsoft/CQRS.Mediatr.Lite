using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockRequestHandlerResolver : IRequestHandlerResolver
    {
        private readonly MockHandlerGenerator _mockHandlerGenerator;

        public MockRequestHandlerResolver(MockHandlerGenerator mockHandlerGenerator)
        {
            _mockHandlerGenerator = mockHandlerGenerator;
        }

        public T Resolve<T>()
        {
            var func = _mockHandlerGenerator.GetResolverFunc();
            return (T)func(typeof(T));
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            var func = _mockHandlerGenerator.GetResolverFunc();
            return (IEnumerable<T>)func(typeof(IEnumerable<T>));
        }
    }
}
