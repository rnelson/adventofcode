using System;
using JetBrains.Annotations;

namespace advent.Exceptions
{
    public class AnswerNotFoundException : Exception
    {
        /// <inheritdoc />
        public AnswerNotFoundException() : base(@"the answer could not be found")
        {
        }

        /// <inheritdoc />
        public AnswerNotFoundException([CanBeNull] string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public AnswerNotFoundException([CanBeNull] string? message, [CanBeNull] Exception? innerException) : base(message, innerException)
        {
        }
    }
}