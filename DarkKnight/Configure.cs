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
        private int _inactiveTime = 15;

        /// <summary>
        /// Sets port with server work
        /// By default is 2111
        /// </summary>
        public int Port
        {
            get { return _ServerPort; }
            set
            {
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

        /// <summary>
        /// Sets a time to check if a client is inactive, if is inactive more of this time we is desconected
        /// Param in seconds
        /// By defauls is 15;
        /// </summary>
        public int MaxInactiveTime
        {
            get { return _inactiveTime; }
            set
            {
                _inactiveTime = value;
            }
        }

    }
}
