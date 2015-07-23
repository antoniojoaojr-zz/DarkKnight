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

namespace DarkKnight.core.Network
{
    class DataTransport
    {
        /// <summary>
        /// The client object request transport
        /// </summary>
        private Client client;

        /// <summary>
        /// The socket object responsible for performing transport
        /// </summary>
        private Socket socket;

        /// <summary>
        /// The queue data wait for transport
        /// </summary>
        private Queue<byte[]> queueData = new Queue<byte[]>();

        /// <summary>
        /// The actual situation of thread responsible for transport data
        /// </summary>
        private bool asynSending = false;

        public DataTransport(Client clientObj, Socket socketObj)
        {
            client = clientObj;
            socket = socketObj;
        }

        /// <summary>
        /// Sends data to client, if exists peding data in sending status
        /// the actual data param enter in the queue
        /// </summary>
        /// <param name="packet">array of bytes to send</param>
        public void Send(byte[] packet)
        {
            if (packet.Length == 0 || packet == null)
                return;

            // add the data in the queue
            queueData.Enqueue(packet);

            // if no thread working to send data
            lock (client)
            {
                if (!asynSending)
                    BeginSend(queueData.Dequeue());
            }
        }

        private void BeginSend(byte[] data)
        {
            // try send data to socket client
            try
            {
                // we set to true to say that we are working with sending
                asynSending = true;

                // start sending data to the socket
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendAsyncResult), socket);
            }
            catch
            {
                // otherwise, any exception not NullException, is a invalid socket connection
                // notify is to application with clossing connection
                client.Close();
            }
        }

        /// <summary>
        /// The sending result
        /// </summary>
        /// <param name="ar"></param>
        private void SendAsyncResult(IAsyncResult ar)
        {
            // retrieve the send socekt object
            Socket result = (Socket)ar.AsyncState;

            try
            {
                // setting end send
                result.EndSend(ar);

                // if have more data in queue
                if (queueData.Count > 0)
                {
                    // call sending
                    BeginSend(queueData.Dequeue());
                }
                else
                {
                    // if not have more data in queue
                    // flush asynseding
                    asynSending = false;
                }
            }
            catch
            {
                client.Close();
            }

        }
    }
}
