using System;
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
    class ServerProcess
    {
        private Action _work;
        private int _delay;

        public ServerProcess(Action work, int delay)
        {
            _work = work;
            _delay = delay;
        }

        public void _working()
        {
            while (ServerController.ServerRunning)
            {
                _work();

                if (_delay > 0)
                    Thread.Sleep(_delay);
            }
        }

        /// <summary>
        /// Run a function when the server is online with e delay
        /// </summary>
        /// <param name="work">Action to call</param>
        /// <param name="delay">int miliseconds</param>
        internal static void work(Action action, int delay)
        {
            new Thread(new ThreadStart(new ServerProcess(action, delay)._working));
        }
    }
}
