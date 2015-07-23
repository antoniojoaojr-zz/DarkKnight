using DarkKnight.Utils;
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

namespace DarkKnight.core.Tasks
{
    class TaskProcess
    {
        private Action _work;
        private int _delay;
        private bool _running = false;

        public TaskProcess(Action work, int delay)
        {
            _work = work;
            _delay = delay == 0 ? 1 : delay;
        }

        /// <summary>
        /// Pause this task
        /// </summary>
        public void pause()
        {
            _running = false;
        }

        /// <summary>
        /// Running this task
        /// </summary>
        public void resume()
        {
            if (!_running)
                new Thread(new ThreadStart(Run)).Start();
        }

        /// <summary>
        /// Gets this task is running
        /// </summary>
        public bool isRunning
        {
            get { return _running; }
        }

        private void Run()
        {
            _running = true;
            while (ServerController.ServerRunning && _running)
            {
                try
                {
                    _work();
                }
                catch (Exception ex)
                {
                    Log.Write("Error in running task " + ex.Message + " - " + ex.StackTrace + " - " + ex.InnerException.Message + " - " + ex.InnerException.StackTrace, LogLevel.ERROR);
                }
                finally
                {
                    Thread.Sleep(_delay);
                }
            }
            _running = false;
        }
    }
}
