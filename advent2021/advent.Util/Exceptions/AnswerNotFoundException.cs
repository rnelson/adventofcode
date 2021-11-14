using JetBrains.Annotations;

namespace advent.Util.Exceptions
{
    public class AnswerNotFoundException : Exception
    {
        /// <inheritdoc />
        [UsedImplicitly]
        public AnswerNotFoundException() : base(@"the answer could not be found")
        {
        }

        /// <inheritdoc />
        [UsedImplicitly]
        public AnswerNotFoundException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        [UsedImplicitly]
        public AnswerNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}