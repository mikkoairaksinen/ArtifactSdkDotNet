using System;

namespace ArtifactSdkDotNet.Core.Exceptions
{
    public class InvalidDeckCodeException : Exception
    {
        public InvalidDeckCodeException()
        {
        }

        public InvalidDeckCodeException( string message ) : base(message)
        {
        }
    }
}