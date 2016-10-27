using DarkKnight.Network;

namespace DarkKnight.Utils
{
    public class Unauthenticated
    {
        public static void add(Client client)
        {
            UnauthenticatedTable.add(client.IPAddress.ToString());
        }
    }
}
