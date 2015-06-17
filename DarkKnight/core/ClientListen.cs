using DarkKnight.core.Clients;
using DarkKnight.Data;
using DarkKnight.Network;
using DarkKnight.Utils;
using System;
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

namespace DarkKnight.core
{
    class ClientListen : Client
    {
        /// <summary>
        /// The buffer to receive data from the client
        /// </summary>
        private byte[] buffer = new byte[65535];

        public ClientListen(Socket socket, int id)
        {
            client = socket;
            _ID = id;

            asyncReceive(this);
        }

        /// <summary>
        /// Is called when this client socket receive a new package
        /// </summary>
        /// <param name="ar"></param>
        private void ReceivablePacket(IAsyncResult ar)
        {
            // getting the current client object
            ClientListen listen = (ClientListen)ar.AsyncState;

            // getting the size of the packet received
            int size = listen.client.EndReceive(ar);

            // get the byte array with the size received
            byte[] received = getReceivedPacket(buffer, size);

            // after restore the client object and also the received packet in original size, 
            // we release the client asynchronously to receive more packages
            // so we can optimize the delivery time of the package when many data are sent in a short time
            asyncReceive(listen);

            // from here we are already processing the package without worrying that we are delaying the arrival of new

            // register the queue information registration of client
            Registers(listen);

            // if size is zero, we do not continue sense, just abandon this method and release the thread
            if (size == 0)
                return;

            // if the SocketLayer of this client is defined just we handle the packet
            if (listen.socketLayer != SocketLayer.undefined)
            {
                byte[] decoded;
                if (listen.socketLayer == SocketLayer.websocket)
                    decoded = listen.decode(PacketWeb.decode(received));
                else
                    decoded = listen.decode(received);

                listen.toApplication(new PacketHandler(decoded));

                return;
            }

            // define the socketLayer of this client
            byte[] webSocket = PacketWeb.auth(received);
            // if the webpacket response length is bigger than 1, the socket is 'websocket'
            if (webSocket.Length > 1)
            {
                listen.socketLayer = SocketLayer.websocket;
                // when working with websocket have to do the handshake on the connection, 
                // here we have the using Authentication processed and sent to the websocket finalizing the handshake
                listen.Send(webSocket);
            }
            else // otherwise the socket is normal 'socket' layer
                listen.socketLayer = SocketLayer.socket;

            // if we come here is because it was the first received packet,
            // we are sure that we've set the type of layer of SocketLayer that our client is,
            // so we will notify the application which new is connected
            Application.connectionOpened(this);
        }

        /// <summary>
        /// Restore registor informed by the application, to store object of client
        /// </summary>
        /// <param name="listen"></param>
        private void Registers(ClientListen listen)
        {
            // we try get a dequeue of registration
            RegisterAbstract register = Register.GetValue(listen._ID);
            // while dequeue of registration not null
            // we make this
            while (register != null)
            {
                // restore the object type
                RegisterType type = register.getType;
                // make selection by type of object
                switch (type)
                {
                    case RegisterType.crypt:
                        // if is crypt, register the crypt in the client
                        listen.registerCrypt(register.getAbstract<Object>());
                        break;
                }

                // try get a dequeue again of registration
                register = Register.GetValue(listen._ID);
            }
        }


        /// <summary>
        /// Send the packet to the server appliaction handler and process
        /// </summary>
        /// <param name="packet">Packet to send</param>
        private void toApplication(Packet packet)
        {
            // we make a finally validation of the packet in the server
            // if the packet is invalid, just print a log in the output
            if (packet.format.getStringFormat == "???" && packet.data.Length == 0)
            {
                Console.WriteLine("Client [" + this.IPAddress.ToString() + "] sended a invalid package");
                return;
            }

            // if allright, send to the application
            Application.ReceivedPacket(this, packet);
        }

        private byte[] getReceivedPacket(byte[] buffer, int size)
        {
            // security of return if the size received is zero
            if (size == 0)
                return new byte[1];

            byte[] pack = new byte[size];

            Array.Copy(buffer, pack, size);

            return pack;
        }

        /// <summary>
        /// Start listen this client socket to receive package async
        /// </summary>
        private void asyncReceive(ClientListen listen)
        {
            try
            {
                // we try to continue receiving client packages
                listen.client.BeginReceive(listen.buffer, 0, listen.buffer.Length, SocketFlags.None, new AsyncCallback(listen.ReceivablePacket), listen);
            }
            catch
            {
                // if we have an exception to receive a new package, it means that we have lost the connection with the client
                // notify this to the application
                if (listen.socketLayer != SocketLayer.undefined)
                    Application.connectionClosed(listen);
            }
        }
    }
}
