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

namespace DarkKnight.core
{
    class ClientSignal
    {
        private static Dictionary<int, ClientSignal> _clientSignal = new Dictionary<int, ClientSignal>();

        private static Object locker = new Object();

        private Client _client;
        private DateTime time = DateTime.Now.AddMilliseconds(2400);

        public ClientSignal(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// Update the time of this client to identify is alive
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public static void udpate(Client client)
        {
            // we garanted one thread per time
            lock (ThreadLocker.sync("ClientSignal::update"))
            {
                if (!_clientSignal.ContainsKey(client.Id))
                    _clientSignal[client.Id] = new ClientSignal(client);
            }

            try
            {
                _clientSignal[client.Id].time = DateTime.Now.AddMilliseconds(2400);
            }
            catch
            {
                // the client id not found in Dictionary, is okay
            }
        }

        public static void DiscardInactives()
        {
            try
            {
                var clients = _clientSignal.Where(x => x.Value.time < DateTime.Now).Select(x => x.Value._client).ToList();
                foreach (var client in clients)
                    client.Close();
            }
            catch
            {
                DarkKnight.Utils.Log.Write("Several error in core.ClientSignal.DiscardInactives()", Utils.LogLevel.WARNING);
            }
        }

        public static void Remove(int clientId)
        {
            try
            {
                _clientSignal.Remove(clientId);
            }
            catch
            {
                // the client id not found in Dictionary, is okay
            }
        }

    }
}
