using System;

namespace Eden.Test
{
    [Serializable]
    public class FailedToRegisterEmitterException : Exception
    {
        public FailedToRegisterEmitterException()
            : base("Failed to register particle emitter")
        {}
    }
}
