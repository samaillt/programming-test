using System;

namespace Eden.Test
{
    [Serializable]
    public class FailedToSpawnObjectFromPoolException : Exception
    {
        public FailedToSpawnObjectFromPoolException()
            : base("Failed to spawn Object from its pool")
        {}

        public FailedToSpawnObjectFromPoolException(ObjectPooler.ObjectPoolType objectPoolType)
            : base(String.Format("Failed to spawn {0} Object from its pool", objectPoolType.ToString()))
        { }

        public FailedToSpawnObjectFromPoolException(ObjectPooler.ObjectPoolType objectPoolType, string message)
            : base(String.Format("Failed to spawn {0} Object from its pool. {1}", objectPoolType.ToString(), message))
        { }
    }
}
