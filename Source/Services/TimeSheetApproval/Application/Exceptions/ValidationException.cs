using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TimeSheetApproval.Application.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}