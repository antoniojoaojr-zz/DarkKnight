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
        /// <summary>
        /// Create a new task to server
        /// </summary>
        /// <param name="action">The method called in task</param>
        /// <param name="delay">The delay in milleseconds</param>
        /// <returns>Task Id</returns>
        public static int newTask(Action action, int delay)
        {
            return TaskManager.newTask(action, delay);
        }

        /// <summary>
        /// Checks task is running
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <returns>True if task is runing otherwise false</returns>
        public static bool isRunning(int taskId)
        {
            return TaskManager.isRunning(taskId);
        }

        /// <summary>
        /// Checks a taskId exist in the server
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <returns>True if taskId exists otherwise false</returns>
        public static bool taskExists(int taskId)
        {
            return TaskManager.taskExists(taskId);
        }

        /// <summary>
        /// Pause a task
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <exception cref="System.Exception">taskId not found</exception>
        public static void pauseTask(int taskId)
        {
            TaskManager.pauseTask(taskId);
        }

        /// <summary>
        /// Resume paused task
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <exception cref="System.Exception">taskId not found</exception>
        public static void resumeTask(int taskId)
        {
            TaskManager.resumeTask(taskId);
        }

        /// <summary>
        /// Stop task and romeve from server
        /// </summary>
        /// <param name="taskId">taskId</param>
        /// <exception cref="System.Exception">taskId not found</exception>
        public static void stopTask(int taskId)
        {
            TaskManager.stopTask(taskId);
        }
    }
}
