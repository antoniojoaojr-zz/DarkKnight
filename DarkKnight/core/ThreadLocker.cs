using System.Collections.Generic;

namespace DarkKnight.core
{
    class ThreadLocker
    {
        private static Dictionary<string, ThreadLocker> lockers = new Dictionary<string, ThreadLocker>();

        public static ThreadLocker sync(string locker)
        {
            if (!lockers.ContainsKey(locker))
                lockers.Add(locker, new ThreadLocker());

            return lockers[locker];
        }
    }
}
