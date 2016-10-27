using DarkKnight.core;
using System.Collections.Generic;

namespace DarkKnight.Network
{
    class UnauthenticatedTable
    {
        private static Dictionary<string, int> table = new Dictionary<string, int>();

        public static void add(string ip)
        {
            if (table.ContainsKey(ip))
            {
                if (table[ip] <= ServerController.config.MaxUnauthenticatedAccept)
                    table[ip]++;
            }
            else
                table.Add(ip, 1);
        }

        public static int get(string ip)
        {
            if (table.ContainsKey(ip))
                return table[ip];
            return 0;
        }
    }
}
