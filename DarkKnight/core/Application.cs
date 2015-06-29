using DarkKnight.Data;
using DarkKnight.Network;
using DarkKnight.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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
    enum ApplicationSend
    {
        connectionOpened,
        ReceivedPacket,
        connectionClosed
    }
    /// <summary>
    /// This class is responsable to communicate with application
    /// </summary>
    class Application
    {
        private static Queue<Application> callQueue = new Queue<Application>();
        private static int ThreadWorking = 0;

        public object[] _objects;
        public string _method;

        public static void work()
        {
            if (callQueue.Count > 0 && ThreadWorking < ServerController.config.MaxThreadWorking)
            {
                ThreadWorking++;
                new Thread(new ThreadStart(applicationCalling)).Start();
            }
        }

        public static void send(ApplicationSend method, object[] objects)
        {
            callQueue.Enqueue(new Application() { _method = Enum.GetName(typeof(ApplicationSend), method), _objects = objects });
        }

        private static void applicationCalling()
        {
            if (callQueue.Count == 0)
            {
                ThreadWorking--;
                return;
            }

            try
            {
                Application app = callQueue.Dequeue();
                DarkKnightAppliaction.callback.GetType().GetMethod(app._method).Invoke(DarkKnightAppliaction.callback, app._objects);
            }
            catch (TargetInvocationException ex)
            {
                Log.Write("Application generate a error - " + ex.InnerException.Message + "\n" + ex.InnerException.StackTrace, LogLevel.ERROR);
            }
            finally
            {
                ThreadWorking--;
            }
        }
    }
}
