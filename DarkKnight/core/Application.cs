using DarkKnight.Data;
using DarkKnight.Network;
using DarkKnight.Utils;
using System;
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
    /// <summary>
    /// This class is responsable to communicate with application
    /// </summary>
    class Application
    {
        public static void connectionOpened(Client client)
        {
            try
            {
                DarkKnightAppliaction.callback.GetType().GetMethod("connectionOpened").Invoke(DarkKnightAppliaction.callback, new object[] { client });
            }
            catch
            {
                // the application error
            }
        }

        public static void ReceivedPacket(Client client, Packet buffer)
        {
            // we try restore a map of packet
            DKAbstractReceiver callback = PacketDictionary.getmappin(Encoding.UTF8.GetBytes(buffer.format.getStringFormat));
            try
            {
                // if map is restored
                // send the packet to the application to the class mapped
                // First ReceivedPacket(Client, Packet), finalliy run() to process
                if (callback != null)
                {
                    callback.ReceivedPacket(client, buffer);
                    callback.run();
                }
                else
                {
                    // otherwise, send the packet to the default service packet handler
                    DarkKnightAppliaction.callback.GetType().GetMethod("ReceivedPacket").Invoke(DarkKnightAppliaction.callback, new object[] { client, buffer });
                }
            }
            catch
            {
                // the appliaction error
            }
        }

        public static void connectionClosed(Client client)
        {
            try
            {
                DarkKnightAppliaction.callback.GetType().GetMethod("connectionClosed").Invoke(DarkKnightAppliaction.callback, new object[] { client });
            }
            catch
            {
                // the application error
            }
        }
    }
}
