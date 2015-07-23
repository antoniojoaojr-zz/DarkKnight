using DarkKnight.core.Tasks;
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

namespace DarkKnight.Utils
{
    public class Task
    {
        private int taskId;

        /// <summary>
        /// Create a new task to server
        /// </summary>
        /// <param name="action">Action for this task</param>
        /// <param name="delay">Delay in milleseconds</param>
        public Task(Action action, int delay)
        {
            taskId = TaskManager.newTask(action, delay);
        }

        /// <summary>
        /// Check task is running
        /// </summary>
        public bool isRunning
        {
            get { return TaskManager.isRunning(taskId); }
        }

        /// <summary>
        /// Pause this task
        /// </summary>
        /// <exception cref="System.Exception">Task not found</exception>
        public void pause()
        {
            TaskManager.pauseTask(taskId);
        }

        /// <summary>
        /// Resume a paused task
        /// </summary>
        /// <exception cref="System.Exception">Task not found</exception>
        public void resume()
        {
            TaskManager.resumeTask(taskId);
        }

        /// <summary>
        /// Stop task and remove from server
        /// </summary>
        /// <exception cref="System.Exception">Task not found</exception>
        public void stop()
        {
            TaskManager.stopTask(taskId);
        }
    }
}
