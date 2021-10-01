using System;

namespace Eden.Test
{
    [Serializable]
    public class FailedToUnregisterEmitterException : Exception
    {
        public FailedToUnregisterEmitterException()
            : base("Failed to unregister particle emitter")
        {}
    }
}
