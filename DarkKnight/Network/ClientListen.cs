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
    class ClientListen : DKClient
    {
        private byte[] bufferListen = new byte[65535];

        public ClientListen(Socket socket, int id)
        {
            client = socket;
            _ID = id;

            asyncReceive(client);
        }

        private void setCallbacks(Dictionary<byte, DKAbstractReceivable> iasync)
        {
            callbackRecorded = iasync;
        }


        /// <summary>
        /// Is called when this client socket receive a new package
        /// </summary>
        /// <param name="ar"></param>
        private void ReceivablePacket(IAsyncResult ar)
        {
            // getting the current client object
            ClientListen currentListen = (ClientListen)ar.AsyncState;

            // flush this thread to asyn receive more data from this client socket in a new object client
            ClientListen newListen = new ClientListen(currentListen.client, currentListen._ID);

            // update the callbacks functions of registors
            currentListen.UpdateRegistor();
            if (currentListen.callbackRecorded.Count > 0)
                newListen.setCallbacks(currentListen.callbackRecorded);

            int size = currentListen.client.EndReceive(ar);

            if (size > 0)
            {
                currentListen.setBuffer(currentListen.bufferListen, size);

                if (currentListen.callbackRecorded.ContainsKey(currentListen.bufferListen[0]))
                {
                    DKAbstractReceivable calling = callbackRecorded[currentListen.bufferListen[0]];
                    calling.ReceivablePacket(currentListen, currentListen);
                    calling.run();

                    return;
                }

                DarkKnightDelegate.callback.newPacket(this, this);
            }
        }

        /// <summary>
        /// Start listen this client socket to receive package async
        /// </summary>
        private void asyncReceive(Socket listen)
        {
            try
            {
                listen.BeginReceive(bufferListen, 0, bufferListen.Length, SocketFlags.None, new AsyncCallback(ReceivablePacket), this);
            }
            catch (ObjectDisposedException ex)
            {
                DarkKnightDelegate.callback.connectionClosed(this);
                // call to the delegate the socket is closed;
            }
        }
    }
}
