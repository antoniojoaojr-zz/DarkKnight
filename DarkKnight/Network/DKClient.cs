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
    public abstract class DKClient : DKBuffer
    {
        protected int _ID;
        protected Socket client;
        protected Dictionary<byte, DKAbstractReceivable> callbackRecorded = new Dictionary<byte, DKAbstractReceivable>();

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
        }

        /// <summary>
        /// Send a byte 8-bits to the client
        /// </summary>
        /// <param name="toSend">The bit to send</param>
        public void Send(byte toSend)
        {
        }

        /// <summary>
        /// Send a array of bytes 8-bits to the client
        /// </summary>
        /// <param name="toSend">The array of bytes to send</param>
        public void Send(byte[] toSend)
        {
        }

        /// <summary>
        /// Send a UTF8 String to the client
        /// </summary>
        /// <param name="toSend">The string to send</param>
        public void SendString(string toSend)
        {
        }


        public void Close()
        {
        }

        public void RegisterCallback(byte identifier, DKAbstractReceivable receiver)
        {
            Registors registors = Registors.getRegistor(_ID);

            registors.callbacksInclude[identifier] = receiver;
        }

        public void DeleteCallback(byte identifier)
        {
            Registors registors = Registors.getRegistor(_ID);
            registors.callbacksExclude[identifier] = null;
        }



        protected void setBuffer(byte[] buffer, int size)
        {
            base.setBuffer(buffer, size);
        }

        protected void UpdateRegistor()
        {
            Registors registors = Registors.getRegistor(_ID);

            foreach (KeyValuePair<byte, DKAbstractReceivable> pair in registors.callbacksInclude)
            {
                callbackRecorded[pair.Key] = pair.Value;
            }

            registors.callbacksInclude.Clear();

            foreach (KeyValuePair<byte, string> pair in registors.callbacksExclude)
            {
                callbackRecorded.Remove(pair.Key);
            }

            registors.callbacksExclude.Clear();
        }
    }
}
