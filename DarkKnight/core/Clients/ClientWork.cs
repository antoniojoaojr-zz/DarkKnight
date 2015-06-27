using DarkKnight.Network;
using System;
using System.Collections.Generic;
using System.Linq;

#region License Information
/* ************************************************************
 * 
 *    @author AntonioJr <antonio@emplehstudios.com.br>
 *    @copyright 2015 Empleh Studios, Inc
 * 
 * 	  Project Folder: https://github.com/antoniojoaojr/DarkKnight
 * 
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion

namespace DarkKnight.core.Clients
{
    class ClientWork
    {
        private static Dictionary<int, ClientWork> clientsWork = new Dictionary<int, ClientWork>();

        public Client _client;
        public DateTime time;

        public ClientWork(Client client)
        {
            _client = client;
        }

        public static void udpate(Client client)
        {
            lock (client)
            {
                if (!clientsWork.ContainsKey(client.Id))
                    clientsWork[client.Id] = new ClientWork(client);

                clientsWork[client.Id].time = DateTime.Now.AddMilliseconds(2400);
            }
        }

        public static void RemoveInactiveClients()
        {
            try
            {
                var clients = clientsWork.Where(x => x.Value.time < DateTime.Now).Select(x => new { client = x.Value._client }).ToList();
                foreach (var resource in clients)
                    resource.client.Close();
            }
            catch
            {
                Console.WriteLine("[WARNING] Several error in core.Clients.ClientWork.RemoveInactiveClients()");
            }
        }

        public static void RemoveClientId(int clientId)
        {
            try
            {
                clientsWork.Remove(clientId);
            }
            catch
            {

            }
        }

    }
}
