using System;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class MockQuery : Query<MockQueryResponse>
    {
        public override string DisplayName => "Mock Query";
        private readonly string _id;
        private readonly bool _shouldFailValidation;
        private readonly string _errorValidationMessage;
        public override string Id { get => _id; }

        public MockQuery() { }

        public MockQuery(string id)
        {
            _id = id;
            _shouldFailValidation = false;
            _errorValidationMessage = null;
        }

        public MockQuery(string id, string errorValidationMessage)
            :this(id)
        {
            _shouldFailValidation = true;
            _errorValidationMessage = errorValidationMessage;

        }

        public override bool Validate(out string message)
        {
            message = _errorValidationMessage;
            return !_shouldFailValidation;
        }
    }
}
