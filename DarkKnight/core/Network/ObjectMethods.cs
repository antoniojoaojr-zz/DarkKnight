using DarkKnight.Network;
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

namespace DarkKnight.core.Network
{
    abstract class ObjectMethods
    {
        protected Object _reference;
        protected string _method;
        protected object _data = null;
        protected toPlayer _networkAccess;

        public Object reference
        {
            get { return _reference; }
        }

        public string name
        {
            get { return _method; }
        }

        public object data
        {
            get { return _data; }
        }

        public toPlayer networkAccess
        {
            get { return _networkAccess; }
        }

        public bool Update()
        {
            object data = Application.calling(_method, new object[] { }, _reference);

            if (data == null || data == _data)
                return false;

            _data = data;
            return true;
        }
    }
}
