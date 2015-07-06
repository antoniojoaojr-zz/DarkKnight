using System;
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

namespace DarkKnight.core
{
    class ServerController
    {
        private static Configure _config = null;
        private static Socket socket;
        private static bool _ServerRun = false;

        /// <summary>
        /// Gets the socket server is running
        /// </summary>
        public static bool ServerRunning
        {
            get { return _ServerRun; }
        }

        /// <summary>
        /// Sets or gets a configure of the server
        /// </summary>
        public static Configure config
        {
            get { return _config; }
            set
            {
                if (_config == null)
                    _config = value;
            }
        }


        /// <summary>
        /// Close the socket server
        /// </summary>
        public static void CloseSever()
        {
            _ServerRun = false;
        }

        /// <summary>
        /// Sets a socket running
        /// </summary>
        /// <param name="_socket"></param>
        public static void setWork(Socket _socket)
        {
            Utils.Log.Write("DarkKnight Server Started Successfully", Utils.LogLevel.TITLE);
            socket = _socket;
            _ServerRun = true;

            ServerProcess.work(ClientSignal.DiscardInactives, 1150);

            RunApplication();
        }

        private static void RunApplication()
        {
            while (_ServerRun)
            {
                Application.work();
                Thread.Sleep(1);
            }
            socket.Close();

            Utils.Log.Write("DarkKnight Server finished", Utils.LogLevel.TEXT);
            Console.ReadKey();
        }
    }
}
