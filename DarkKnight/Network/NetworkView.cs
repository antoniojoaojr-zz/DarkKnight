using DarkKnight.core.Network;
using DarkKnight.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

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

namespace DarkKnight.Network
{
    public enum StreamAccess
    {
        /// <summary>
        /// All client in the radius view the method
        /// </summary>
        Public,

        /// <summary>
        /// Only clients who are looking for the object capture this information
        /// </summary>
        Private,
    }

    /// <summary>
    /// Add this attribute in your method to identify this method is shared with network
    /// </summary>
    public class NetworkViewAttribute : Attribute
    {
        public StreamAccess streamAccess = StreamAccess.Private;
    }

    /// <summary>
    /// Extend this class in your object class to controler you class like object in the server
    /// </summary>
    public abstract class NetworkView
    {
        private ObjectController _networkController;

        /// <summary>
        /// Pass your object to server watcher and share your methods with the network
        /// </summary>
        /// <typeparam name="T">The object to watcher</typeparam>
        /// <param name="objectWatcher">Object needs extension of DarkKnight.Network.NetworkView object</param>
        protected void CreateInstance<T>(T objectWatcher)
        {
            if (objectWatcher.GetType().IsSubclassOf(typeof(NetworkView)))
                throw new Exception("Invalid object, the object needs extends DarkKnight.Network.NetworkView class");

            _networkController = new ObjectController(GetMethodView(objectWatcher));
        }

        /// <summary>
        /// Call this method to update your object with the network, only method changed will be shared
        /// </summary>
        protected void Update()
        {
            _networkController.Update();
        }

        private List<ObjectMethods> GetMethodView(Object objectWatcher)
        {
            List<ObjectMethods> methods = new List<ObjectMethods>();

            foreach (MethodInfo mInfo in objectWatcher.GetType().GetMethods())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() != typeof(NetworkViewAttribute))
                        continue;

                    if (!mInfo.ReturnType.IsAssignableFrom(typeof(int)) &&
                        !mInfo.ReturnType.IsAssignableFrom(typeof(byte)) &&
                        !mInfo.ReturnType.IsAssignableFrom(typeof(string)) &&
                        !mInfo.ReturnType.IsAssignableFrom(typeof(float)) &&
                        !mInfo.ReturnType.IsAssignableFrom(typeof(double)) &&
                        !mInfo.ReturnType.IsAssignableFrom(typeof(long)))
                    {
                        Log.Write("Invalid NetworkView attribute in " + objectWatcher.GetType().Name + "." + mInfo.Name + "(), the return data not valid", LogLevel.WARNING);
                        continue;
                    }

                    if (mInfo.GetParameters().Length > 0)
                    {
                        Log.Write("Invalidt NetworkView attribute in " + objectWatcher.GetType().Name + "." + mInfo.Name + "(), the method can not have an parameter", LogLevel.WARNING);
                        continue;
                    }

                    ObjectMethodsController methodsController = new ObjectMethodsController();
                    methodsController.setReference(objectWatcher);
                    methodsController.setMethod(mInfo.Name);
                    methodsController.setNetworkAccess(((NetworkViewAttribute)attr).streamAccess);

                    methods.Add(methodsController);
                }
            }

            return methods;
        }
    }
}
