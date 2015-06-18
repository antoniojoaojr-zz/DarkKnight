using DarkKnight.Data;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

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
        private bool threadStatus = false;

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
        public void Send(PacketCreator packet)
        {
            // add the data in the queue
            queueData.Enqueue(packet.data);

            // if no thread working to send data
            if (!threadStatus)
            {
                // we set to true to say that we are working with sending
                threadStatus = true;

                // here we started working
                (new Thread(new ThreadStart(SendThread))).Start();
            }
        }

        /// <summary>
        /// Method sending in a thread
        /// </summary>
        private void SendThread()
        {
            // run this sending thread until there is nothing left in the queue
            while (queueData.Count > 0)
            {
                // get next data in the queue
                byte[] data = queueData.Dequeue();

                // try send data to socket client
                try
                {
                    // sends data to the client
                    socket.Send(data);
                }
                catch (ArgumentNullException ex)
                {
                    // try to send a null buffer to the client generate a exception
                    // notify is in a output log
                    Console.WriteLine("Null data to send a cliente is not accepted:\n" + ex.Message + " - " + ex.Source);
                }
                catch
                {
                    // otherwise, any exception not NullException, is a invalid socket connection
                    // notify is to application with clossing connection
                    client.Close();
                }
            }

            // if no more data, flush this thread making false to not work
            threadStatus = false;
        }
    }
}
