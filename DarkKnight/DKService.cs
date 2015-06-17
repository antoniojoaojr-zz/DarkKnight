using DarkKnight.Data;
using DarkKnight.Network;
using System;

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

namespace DarkKnight
{
    public abstract class DKService : IReceived
    {
        public DKService()
        {
            DarkKnightAppliaction.setApplication(this);
        }

        /// <summary>
        /// Is called when new client is connected to the server
        /// </summary>
        /// <param name="client"></param>
        abstract public void connectionOpened(Client client);

        /// <summary>
        /// Is called when received a new packet not registered from a client
        /// </summary>
        /// <param name="client"></param>
        abstract public void ReceivedPacket(Client client, Data.Packet buffer);

        /// <summary>
        /// Is called when a client closing the connection with the server
        /// </summary>
        /// <param name="client"></param>
        abstract public void connectionClosed(Client client);


    }
}
