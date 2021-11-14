using JetBrains.Annotations;

namespace advent.Util.Exceptions
{
    public class BadDataException : Exception
    {
        /// <inheritdoc />
        [UsedImplicitly]
        public BadDataException() : base(@"bad data provided")
        {
        }

        /// <inheritdoc />
        [UsedImplicitly]
        public BadDataException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        [UsedImplicitly]
        public BadDataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}