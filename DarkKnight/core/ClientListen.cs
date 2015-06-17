using DarkKnight.Network;
using System;
using System.Collections.Generic;
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

namespace DarkKnight.core
{
    class ClientListen : DKClient
    {
        /// <summary>
        /// The buffer to receive data from the client
        /// </summary>
        private byte[] buffer = new byte[65535];

        public ClientListen(Socket socket, int id)
        {
            client = socket;
            _ID = id;

            asyncReceive(client);
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

            // flush this thread to asyn receive more data from this client socket
            asyncReceive(listen.client);

            // if the size is zero, just close the method
            if (size == 0)
                return;

            // if the SocketLayer of this client is defined just handle the packet
            if (listen.socketLayer != SocketLayer.undefined)
            {
                if (listen.socketLayer == SocketLayer.websocket)
                    listen.packetToApplication(new PacketHandler(PacketWeb.decode(received)));
                else
                    listen.packetToApplication(new PacketHandler(received));

                return;
            }

            // define the socketLayer of this client
            byte[] webSocket = PacketWeb.auth(received);
            // if the webpacket response length is bigger than 1, the socket is 'websocket'
            if (webSocket.Length > 1)
            {
                listen.socketLayer = SocketLayer.websocket;
                listen.Send(webSocket);
                return;
            }

            // it not a websocket, this socket is a normal 'socket' layer
            listen.socketLayer = SocketLayer.socket;
            //currentListen.SendString("connected");
        }

        private byte[] getReceivedPacket(byte[] buffer, int size)
        {
            // security of return if the size received is zero
            if (size == 0)
                return new byte[1];

            byte[] pack = new byte[size];
            for (int i = 0; i < size; i++)
            {
                pack[i] = buffer[i];
            }

            return pack;
        }

        /// <summary>
        /// Start listen this client socket to receive package async
        /// </summary>
        private void asyncReceive(Socket listen)
        {
            try
            {
                listen.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceivablePacket), this);
            }
            catch (ObjectDisposedException ex)
            {
                DarkKnightAppliaction.send.connectionClosed(this);
                // call to the delegate the socket is closed;
            }
        }
    }
}
