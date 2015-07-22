using DarkKnight.core;
using DarkKnight.core.Network;
using DarkKnight.Crypt;
using DarkKnight.Data;
using DarkKnight.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public abstract class Client
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

        private CryptProvider cryptProvider = new CryptProvider();

        private string _IPAddress;

        private Socket _client;

        protected Object _receiver = null;

        /// <summary>
        /// The socket object of this client
        /// </summary>
        protected Socket client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
                _IPAddress = ((IPEndPoint)_client.RemoteEndPoint).Address.ToString();
                transportLayer = new DataTransport(this, _client);
            }
        }

        /// <summary>
        /// Decode a byte data with cryptProvider
        /// </summary>
        /// <param name="data">the data to be decoded</param>
        /// <returns></returns>
        protected byte[] Decode(byte[] data)
        {
            return cryptProvider.decode(data);
        }

        /// <summary>
        /// The layer of the socket is work
        /// </summary>
        protected SocketLayer socketLayer = SocketLayer.undefined;

        /// <summary>
        /// Gets the client still connected
        /// </summary>
        public bool IsConnected
        {
            get { return Connected; }
        }

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
        /// Send a object to the client in json serialized format
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        /// <param name="toSend">the object to send</param>
        public void SendFormatedObject(PacketFormat format, object toSend)
        {
            if (!toSend.GetType().Equals(typeof(ObjectStream)))
            {
                if (!toSend.GetType().IsSubclassOf(typeof(ObjectSerialize)))
                    throw new Exception("Invalid object to send, needs ObjectSerialize extension or ObjectStream type");

                Dictionary<string, object> objectToSend = new Dictionary<string, object>();
                objectToSend["Object"] = toSend;

                ObjectStream stream = new ObjectStream();
                stream.Id = ((ObjectSerialize)toSend).objectId;
                stream.ObjectData = objectToSend;

                SendFormatedString(format, JsonConvert.SerializeObject(stream));
            }
            else
                SendFormatedString(format, JsonConvert.SerializeObject(toSend));
        }

        /// <summary>
        /// Register a class to receiver package from this client
        /// </summary>
        /// <typeparam name="T">The object receiver</typeparam>
        /// <param name="receiver">The object receiver</param>
        public void RegisterReceiver<T>(T receiver)
        {
            if (!receiver.GetType().IsAssignableFrom(typeof(IReceived)))
                throw new Exception("The object receiver needs interface DarkKnight.IReceived");

            _receiver = receiver;
        }

        /// <summary>
        /// Register a cryptograph class for this client to encrypt and decrypt packaged send and receive
        /// </summary>
        /// <typeparam name="T">The object crypt</typeparam>
        /// <param name="crypt">The object crypt</param>
        public void RegisterCrypt<T>(T crypt)
        {
            if (!crypt.GetType().IsSubclassOf(typeof(AbstractCrypt)))
                throw new Exception("The object of crypt is invalid, needs extends class DarkKnight.Crypt.AbstractCrypt");

            cryptProvider.registerCrypt(crypt);
        }

        /// <summary>
        /// Close the connection with the client
        /// Call 'connectionClosed' of your application that extends the DKService when the connection is closed successfully
        /// </summary>
        public void Close()
        {
            // one thread per time, prevent two or more thread call Close() in same time and generate duplicate notifications
            lock (client)
            {
                if (!Connected)
                    return;

                try
                {                 // sets connected false
                    Connected = false;
                    // remove the client from signal
                    ClientSignal.Remove(Id);
                    // close the socket
                    _client.Close();
                }
                catch (Exception ex)
                {
                    Log.Write("Error when disconnect client " + ex.Message + " - " + ex.StackTrace, LogLevel.ERROR);
                }
                finally
                {
                    // if the socket have a type and defined, notification the application is desconnected
                    if (socketLayer != SocketLayer.undefined)
                    {
                        Application.send(ApplicationSend.connectionClosed, new object[] { this });
                        socketLayer = SocketLayer.undefined;
                    }
                }
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
                data = PacketWeb.encode(cryptProvider.encode(packet.data));
            else // otherwise return packet encode
                data = cryptProvider.encode(packet.data);

            // send the data
            transportLayer.Send(data);
        }

    }
}
