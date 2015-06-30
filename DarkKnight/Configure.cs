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
    public class Configure
    {

        private int _ServerPort = 2111;
        private int _backlog = 50;
        private int _MaxThread = 4;

        /// <summary>
        /// The higher the number, the less likely a row of access to your application.
        /// Note that a very large number can also cause very slow performance of the processor.
        /// The default value is 4
        /// </summary>
        public int MaxThreadWorking
        {
            get { return _MaxThread; }
            set
            {
                if (1 > value)
                    throw new Exception("Minimum 1 thread for work");

                _MaxThread = value;
            }
        }

        /// <summary>
        /// Sets port with server work
        /// By default is 2111
        /// </summary>
        public int Port
        {
            get { return _ServerPort; }
            set
            {
                if (1 > value || 65535 < value)
                    throw new Exception("Invalid port in configuration file");

                _ServerPort = value;
            }
        }

        /// <summary>
        /// setting maximum number of sockets in line to enter the server
        /// By default is 50
        /// </summary>
        public int Backlog
        {
            get { return _backlog; }
            set
            {
                _backlog = value;
            }
        }

    }
}
