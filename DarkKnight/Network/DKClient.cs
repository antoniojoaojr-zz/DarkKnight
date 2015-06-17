using DarkKnight.core;
using DarkKnight.Network;
using DarkKnight.Network;
using DarkKnight.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

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

namespace DarkKnight.Network
{
    public abstract class DKClient
    {
        /// <summary>
        /// The session id of this client
        /// </summary>
        protected int _ID;

        /// <summary>
        /// The socket object of this client
        /// </summary>
        protected Socket client;

        /// <summary>
        /// The layer of the socket is work
        /// </summary>
        protected SocketLayer socketLayer = SocketLayer.undefined;

        /// <summary>
        /// Gets the IPAddress object of client connected
        /// </summary>
        public IPAddress IPAddress
        {
            get { return IPAddress.Parse(((IPEndPoint)client.RemoteEndPoint).Address.ToString()); }
        }

        /// <summary>
        /// Gets the ID of client
        /// </summary>
        public int Id
        {
            get { return _ID; }
        }

        /// <summary>
        /// Send a integer to the client
        /// </summary>
        /// <param name="toSend">The integer to send</param>
        public void SendInt(int toSend)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a byte 8-bits to the client
        /// </summary>
        /// <param name="toSend">The bit to send</param>
        public void Send(byte toSend)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a array of bytes 8-bits to the client
        /// </summary>
        /// <param name="toSend">The array of bytes to send</param>
        public void Send(byte[] toSend)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a UTF8 String to the client
        /// </summary>
        /// <param name="toSend">The string to send</param>
        public void SendString(string toSend)
        {
            throw new NotImplementedException();
        }


        public void Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send the packet to the server appliaction handler and process
        /// </summary>
        /// <param name="packet">Packet to send</param>
        protected void packetToApplication(Packet packet)
        {
            if (packet.name == "???" && packet.length == 0)
            {
                Console.WriteLine("Client [" + this.IPAddress.ToString() + "] sended a invalid package");
                return;
            }

            DKAbstractReceivable callback = PacketDictionary.getmappin(packet.name);

            if (callback != null)
            {
                callback.ReceivedPacket(this, packet);
                callback.run();
            }
            else
            {
                DarkKnightAppliaction.send.ReceivedPacket(this, packet);
            }

        }
    }
}
