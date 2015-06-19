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

namespace DarkKnight.core.Clients
{
    enum RegisterType
    {
        crypt,
        packet
    }
    class Register
    {
        /// <summary>
        /// we store all registers request by application to server
        /// </summary>
        private static Dictionary<int, Queue<RegisterAbstract>> register = new Dictionary<int, Queue<RegisterAbstract>>();

        /// <summary>
        /// Add a registor to store of registers of clients
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ClientId">ClientId request new register</param>
        /// <param name="type">Type of registor</param>
        /// <param name="abstractClass">the object to store</param>
        public static void Add<T>(int ClientId, RegisterType type, T abstractClass)
        {
            if (!register.ContainsKey(ClientId))
                register.Add(ClientId, new Queue<RegisterAbstract>());

            Queue<RegisterAbstract> queue = register[ClientId];

            RegisterAbstract registerAbstract = new RegisterAbstract();
            registerAbstract.set(type, abstractClass);

            queue.Enqueue(registerAbstract);
        }

        /// <summary>
        /// Restore a registor from a queue register requests client
        /// </summary>
        /// <param name="ClientId">ClientId request dequeue</param>
        /// <returns>The RegisterAbstract class with object stored and type of object</returns>
        public static RegisterAbstract GetValue(int ClientId)
        {
            if (!register.ContainsKey(ClientId))
                return null;

            if (register[ClientId].Count == 0)
                return null;

            return register[ClientId].Dequeue();
        }

    }
}
