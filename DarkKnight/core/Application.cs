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
        public static void send(ApplicationSend method, object[] param)
        {
            send(method, param, DarkKnightAppliaction.callback);
        }

        public static void send(ApplicationSend method, object[] param, object callback)
        {
            calling(Enum.GetName(typeof(ApplicationSend), method), param, callback);
        }

        public static object calling(string method, object[] param, object callback)
        {
            try
            {
                return callback.GetType().GetMethod(method).Invoke(callback, param);
            }
            catch (TargetInvocationException ex)
            {
                Log.Write("Application generate a error - " + ex.InnerException.Message + "\n" + ex.InnerException.StackTrace, LogLevel.ERROR);
                return null;
            }
        }
    }
}
