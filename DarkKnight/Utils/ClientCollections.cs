using DarkKnight.core;
using DarkKnight.Data;
using DarkKnight.Network;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace DarkKnight.Utils
{
    /// <summary>
    /// Dynamic client collections,
    /// This collection makes the internal management automatically discarding client who are disconnected.
    /// Sending data to all clients of the collection at once
    /// </summary>
    public class ClientCollections
    {
        private Dictionary<int, Client> c = new Dictionary<int, Client>();

        /// <summary>
        /// Add a client to a dictionary
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void Add(Client client)
        {
            c[client.Id] = client;
        }

        /// <summary>
        /// Remove a client from a dictionary
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void Remove(Client client)
        {
            try
            {
                if (c.ContainsKey(client.Id))
                    c.Remove(client.Id);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Get all clients in dictionary
        /// </summary>
        /// <returns>List<DarkKnight.Network.Client> object</returns>
        public List<Client> getClients()
        {
            List<Client> clients = new List<Client>();

            foreach (Client client in c.Values)
            {
                if (!client.IsConnected)
                    Remove(client);
                else clients.Add(client);
            }

            return clients;
        }

        /// <summary>
        /// Send a byte to all clients in list
        /// </summary>
        /// <param name="toSend">byte to send</param>
        public void Send(byte toSend)
        {
            Send(new byte[] { toSend });
        }

        /// <summary>
        /// Send a array of bytes 8-bits to the all clients in list
        /// </summary>
        /// <param name="toSend">The array of bytes to send</param>
        public void Send(byte[] toSend)
        {
            callMethod("Send", new object[] { toSend });
        }

        /// <summary>
        /// Send a UTF8 String to the all clients in list
        /// </summary>
        /// <param name="toSend">The string to send</param>
        public void SendString(string toSend)
        {
            Send(Encoding.UTF8.GetBytes(toSend));
        }

        /// <summary>
        /// Send a mapped with format to the all clients in list
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        public void SendFormated(PacketFormat format)
        {
            callMethod("SendFormated", new object[] { format });
        }

        /// <summary>
        /// Send a array of bytes 8-bits mapped with format to the all clients in list
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        /// <param name="toSend">the int to send</param>
        public void SendFormated(PacketFormat format, byte[] toSend)
        {
            callMethod("SendFormated", new object[] { format, toSend });
        }

        /// <summary>
        ///  Send a UTF8 String mapped with format to the all clients in list
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        /// <param name="toSend">the string to send</param>
        public void SendFormatedString(PacketFormat format, string toSend)
        {
            SendFormated(format, Encoding.UTF8.GetBytes(toSend));
        }

        private void callMethod(string method, object[] param)
        {
            foreach (Client client in c.Values)
            {
                if (!client.IsConnected)
                    Remove(client);
                else Application.calling(method, param, client);
            }
        }
    }
}
