using System;
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
    class ServerController
    {
        private static Socket socket;
        private static bool Running = false;

        /// <summary>
        /// Close the socket server
        /// </summary>
        public static void CloseSever()
        {
            Running = false;
            socket.Close();
        }

        /// <summary>
        /// Sets a socket running
        /// </summary>
        /// <param name="_socket"></param>
        public static void setWork(Socket _socket)
        {
            socket = _socket;
            Running = true;
            
            // hack to keep the process running
            RunningTimer();
        }

        private static void RunningTimer()
        {
            // when Running is true,
            // this loop keep the process running
            while (Running)
            {
                // remove clients inactive
                DarkKnight.core.Clients.ClientWork.RemoveInactiveClients();

                // Pass through the loop every second
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
}
