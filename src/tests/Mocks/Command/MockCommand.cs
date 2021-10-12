using System;
using System.Diagnostics.CodeAnalysis;

namespace CQRS.Mediatr.Lite.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class MockCommand : Command<MockCommandResult>
    {
        private readonly string _id;
        public override string Id { get => _id; }

        private readonly bool _shouldFailValidation;
        private readonly string _errorValidationMessage;
        
        public MockCommand() { }
        
        public MockCommand(string commandId)
        {
            _id = commandId;
            _shouldFailValidation = false;
            _errorValidationMessage = null;
        }

        public MockCommand(string commandId, string errorValidationMessage)
            :this (commandId)
        {
            _shouldFailValidation = true;
            _errorValidationMessage = errorValidationMessage;
        }

        public override string DisplayName => "Mock Command";

        public override bool Validate(out string ValidationErrorMessage)
        {
            ValidationErrorMessage = _errorValidationMessage;
            return !_shouldFailValidation;
        }
    }
}
