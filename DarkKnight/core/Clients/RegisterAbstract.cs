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
    class RegisterAbstract
    {
        /// <summary>
        /// The register type of object
        /// </summary>
        RegisterType registerType;

        /// <summary>
        /// The object stored
        /// </summary>
        Object _abstractClass = null;

        /// <summary>
        /// Set the register type and object to store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type of object</param>
        /// <param name="abstractClass">The object to store</param>
        public void set<T>(RegisterType type, T abstractClass)
        {
            registerType = type;
            _abstractClass = abstractClass;
        }

        /// <summary>
        /// Gets a object stored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The object stored</returns>
        public T getAbstract<T>() where T : class
        {
            return _abstractClass as T;
        }

        /// <summary>
        /// Gets the type of object
        /// </summary>
        public RegisterType getType
        {
            get { return registerType; }
        }

    }
}
