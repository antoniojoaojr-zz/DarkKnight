using System;
using System.Collections.Generic;

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
    class TaskManager
    {
        private static int _TaskId = 0;
        private static Dictionary<int, TaskProcess> DictionaryTask = new Dictionary<int, TaskProcess>();

        /// <summary>
        /// Create a new taks to server
        /// </summary>
        /// <param name="action">Method the task delegate</param>
        /// <param name="delay">Millesconds time to delegate task</param>
        /// <returns>TaskId</returns>
        public static int newTask(Action action, int delay)
        {
            int taskId = _TaskId++;

            DictionaryTask.Add(taskId, new TaskProcess(action, delay));
            if (ServerController.ServerRunning)
                DictionaryTask[taskId].resume();

            return taskId;
        }

        /// <summary>
        /// Checks a task is running
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <returns>True if is running otherwise false</returns>
        public static bool isRunning(int taskId)
        {
            if (!DictionaryTask.ContainsKey(taskId))
                return false;

            return DictionaryTask[taskId].isRunning;
        }

        /// <summary>
        /// Checks a taskId exist in the server
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <returns>True if taskId exists otherwise false</returns>
        public static bool taskExists(int taskId)
        {
            return DictionaryTask.ContainsKey(taskId);
        }

        /// <summary>
        /// Pause a specific task
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <exception cref="System.Exception">taskId not found</exception>
        public static void pauseTask(int taskId)
        {
            if (DictionaryTask.ContainsKey(taskId))
                DictionaryTask[taskId].pause();
            else throw new Exception("TaskId not found");
        }

        /// <summary>
        /// Resume a paused task
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <exception cref="System.Exception">taskId not found</exception>
        public static void resumeTask(int taskId)
        {
            if (DictionaryTask.ContainsKey(taskId))
                DictionaryTask[taskId].resume();
            else throw new Exception("TaskId not found");
        }

        /// <summary>
        /// Stop and remove the task
        /// </summary>
        /// <param name="taskId">TaskId</param>
        /// <exception cref="System.Exception">taskId not found</exception>
        public static void stopTask(int taskId)
        {
            if (DictionaryTask.ContainsKey(taskId))
            {
                DictionaryTask[taskId].pause();
                DictionaryTask.Remove(taskId);
            }
            else throw new Exception("TaskId not found");
        }

        /// <summary>
        /// Run all taks in server start wait
        /// </summary>
        public static void StartAllTask()
        {
            foreach (KeyValuePair<int, TaskProcess> task in DictionaryTask)
            {
                if (!task.Value.isRunning)
                    task.Value.resume();
            }
        }
    }
}
