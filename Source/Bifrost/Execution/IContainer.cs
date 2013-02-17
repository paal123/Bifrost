﻿#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a container for resolving types
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Get an instance of a specific type
        /// </summary>
        /// <typeparam name="T">Type to get instance of</typeparam>
        /// <returns>Instance of the type</returns>
        T Get<T>();

        /// <summary>
        /// Get an instance of a specific type
        /// </summary>
        /// <typeparam name="T">Type to get instance of</typeparam>
        /// <param name="optional">If the binding is optional, return null and not throw an exception</param>
        /// <returns>Instance of the type</returns>
        T Get<T>(bool optional);

        /// <summary>
        /// Get an instance of a specific type
        /// </summary>
        /// <param name="type">Type to get instance of</param>
        /// <returns>Instance of the type</returns>
        object Get(Type type);

        /// <summary>
        /// Get an instance of a specific type
        /// </summary>
        /// <param name="type">Type to get instance of</param>
        /// <param name="optional">If the binding is optional, return null and not throw an exception</param>
        /// <returns>Instance of the type</returns>
        object Get(Type type, bool optional = false);

        /// <summary>
        /// Get all instances of a specific type
        /// </summary>
        /// <typeparam name="T">Type to get instances of</typeparam>
        /// <returns>Instances of the type</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Gets wether or not a specific service has a binding 
        /// </summary>
        /// <param name="type">Type of service to check</param>
        /// <returns>True if service has binding, false if not</returns>
        bool HasBindingFor(Type type);

        /// <summary>
        /// Gets wether or not a specific service has a binding 
        /// </summary>
        /// <typeparam name="T">Type of service to check</typeparam>
        /// <returns>True if service has binding, false if not</returns>
        bool HasBindingFor<T>();


        /// <summary>
        /// Get all instances of a specific type
        /// </summary>
        /// <param name="type">Type to get instances for</param>
        /// <returns>Instances of the type</returns>
        IEnumerable<object> GetAll(Type type);
        
        /// <summary>
        /// Get all services that have been bound
        /// </summary>
        /// <returns><see cref="IEnumerable{Type}"/> containing all bound services</returns>
        IEnumerable<Type> GetBoundServices();

        /// <summary>
        /// Bind a service type to a callback that can resolve it
        /// </summary>
        /// <param name="service">Service to bind</param>
        /// <param name="resolveCallback">Callback that gets called to resolve it</param>
        void Bind(Type service, Func<Type> resolveCallback);

        /// <summary>
        /// Bind a service type to a callback that can resolve it
        /// </summary>
        /// <typeparam name="T">Service to bind</typeparam>
        /// <param name="resolveCallback">Callback that gets called to resolve it</param>
        void Bind<T>(Func<Type> resolveCallback);

        /// <summary>
        /// Bind a service type to a callback that can resolve it with given lifecycle
        /// </summary>
        /// <param name="service">Service to bind</param>
        /// <param name="resolveCallback">Callback that gets called to resolve it</param>
        /// <param name="lifecycle">Lifecycle of the service</param>
        void Bind(Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle);

        /// <summary>
        /// Bind a service type to a callback that can resolve it with given lifecycle
        /// </summary>
        /// <typeparam name="T">Service to bind</typeparam>
        /// <param name="resolveCallback">Callback that gets called to resolve it</param>
        /// <param name="lifecycle">Lifecycle of the service</param>
        void Bind<T>(Func<Type> resolveCallback, BindingLifecycle lifecycle);

        /// <summary>
        /// Bind a service to a specific type 
        /// </summary>
        /// <typeparam name="T">Service to bind</typeparam>
        /// <param name="type">Target type to bind to</param>
        void Bind<T>(Type type);

        /// <summary>
        /// Bind a service to a specific type 
        /// </summary>
        /// <param name="service">Service to bind</param>
        /// <param name="type">Target type to bind to</param>
        void Bind(Type service, Type type);

        /// <summary>
        /// Bind a service to a specific type with given lifecycle
        /// </summary>
        /// <typeparam name="T">Service to bind</typeparam>
        /// <param name="type">Target type to bind to</param>
        /// <param name="lifecycle">Lifecycle of the service</param>
        void Bind<T>(Type type, BindingLifecycle lifecycle);

        /// <summary>
        /// Bind a service to a specific type with given lifecycle
        /// </summary>
        /// <param name="service">Service to bind</param>
        /// <param name="type">Target type to bind to</param>
        /// <param name="lifecycle">Lifecycle of the service</param>
        void Bind(Type service, Type type, BindingLifecycle lifecycle);

        /// <summary>
        /// Bind a service to a specific instance
        /// </summary>
        /// <typeparam name="T">Service to bind</typeparam>
        /// <param name="instance">Instance to bind to</param>
        void Bind<T>(T instance);

        /// <summary>
        /// Bind a service to a specific instance
        /// </summary>
        /// <param name="service">Service to bind</param>
        /// <param name="instance">Instance to bind to</param>
        void Bind(Type service, object instance);
    }
}
