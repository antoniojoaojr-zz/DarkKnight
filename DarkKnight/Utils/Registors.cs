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

namespace DarkKnight.Utils
{
    class Registors
    {
        private Dictionary<byte, DKAbstractReceivable> _callbacksInclude = new Dictionary<byte, DKAbstractReceivable>();
        private Dictionary<byte, string> _callbacksExclude = new Dictionary<byte, string>();


        public Dictionary<byte, DKAbstractReceivable> callbacksInclude
        {
            get { return _callbacksInclude; }
        }

        public Dictionary<byte, string> callbacksExclude
        {
            get { return _callbacksExclude; }
        }


        private static Dictionary<int, Registors> _registors = new Dictionary<int, Registors>();


        public static Dictionary<int, Registors> registors
        {
            get { return _registors; }
        }

        public static Registors getRegistor(int ClientId)
        {
            if (!Registors.registors.ContainsKey(ClientId))
            {
                Registors.registors.Add(ClientId, new Registors());
            }

            return Registors.registors[ClientId];
        }
    }
}
