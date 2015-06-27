using DarkKnight.core;
using DarkKnight.core.Clients;
using DarkKnight.Crypt;
using DarkKnight.Data;
using System;
using System.Net;
using System.Net.Sockets;
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

namespace DarkKnight.Network
{
    public enum SocketLayer
    {
        undefined,
        socket,
        websocket
    }
    public abstract class Client : CryptProvider
    {
        /// <summary>
        /// Gets the client status
        /// </summary>
        private bool Connected = true;

        /// <summary>
        /// The session id of this client
        /// </summary>
        protected int _ID;

        /// <summary>
        /// The layer provider to server sends data for client
        /// </summary>
        private DataTransport transportLayer;

        private string _IPAddress;

        /// <summary>
        /// The socket object of this client
        /// </summary>
        protected Socket client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
                _IPAddress = ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                transportLayer = new DataTransport(this, value);
            }
        }

        /// <summary>
        /// The layer of the socket is work
        /// </summary>
        protected SocketLayer socketLayer = SocketLayer.undefined;

        /// <summary>
        /// Gets the IPAddress object of client connected
        /// </summary>
        public IPAddress IPAddress
        {
            get { return IPAddress.Parse(_IPAddress); }
        }

        /// <summary>
        /// Gets the ID of client
        /// </summary>
        public int Id
        {
            get { return _ID; }
        }

        /// <summary>
        /// Send a byte
        /// </summary>
        /// <param name="toSend">byte to send</param>
        public void Send(byte toSend)
        {
            Send(new byte[] { toSend });
        }

        /// <summary>
        /// Send a array of bytes 8-bits to the client
        /// </summary>
        /// <param name="toSend">The array of bytes to send</param>
        public void Send(byte[] toSend)
        {
            SendEncodingPacket(new PacketCreator(toSend));
        }

        /// <summary>
        /// Send a UTF8 String to the client
        /// </summary>
        /// <param name="toSend">The string to send</param>
        public void SendString(string toSend)
        {
            Send(Encoding.UTF8.GetBytes(toSend));
        }

        /// <summary>
        /// Send a mapped with format to the client
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        public void SendFormated(PacketFormat format)
        {
            SendEncodingPacket(new PacketCreator(format, new byte[] { }));
        }

        /// <summary>
        /// Send a array of bytes 8-bits mapped with format to the client
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        /// <param name="toSend">the int to send</param>
        public void SendFormated(PacketFormat format, byte[] toSend)
        {
            SendEncodingPacket(new PacketCreator(format, toSend));
        }

        /// <summary>
        ///  Send a UTF8 String mapped with format to the client
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        /// <param name="toSend">the string to send</param>
        public void SendFormatedString(PacketFormat format, string toSend)
        {
            SendFormated(format, Encoding.UTF8.GetBytes(toSend));
        }

        /// <summary>
        /// Close the connection with the client
        /// Call 'connectionClosed' of your application that extends the DKService when the connection is closed successfully
        /// </summary>
        public void Close()
        {
            if (Connected)
            {
                ClientWork.RemoveClientId(this.Id);
                client.Close();
                Application.connectionClosed(this);

                Connected = false;
            }
        }

        /// <summary>
        /// encode a packet to transport and send
        /// </summary>
        /// <param name="packet">The PacketCreator object</param>
        /// <returns>array of bytes encoded</returns>
        private void SendEncodingPacket(PacketCreator packet)
        {
            byte[] data;
            // if this client is a websocket, encode package with a packetweb
            if (socketLayer == SocketLayer.websocket)
                data = PacketWeb.encode(_encode(packet.data));
            else // otherwise return packet encode
                data = _encode(packet.data);

            // send the data
            transportLayer.Send(data);
        }

    }
}
