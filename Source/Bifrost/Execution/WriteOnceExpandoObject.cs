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
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an ExpandoObject that can only have values assigned to during creation.
    /// Similar to <see cref="ExpandoObject"/>, members are dynamic and can be added on the fly
    /// </summary>
    public class WriteOnceExpandoObject : DynamicObject, IDictionary<string, object>
    {
        Dictionary<string, object> _actualDictionary = new Dictionary<string, object>();
        bool _construction;

        /// <summary>
        /// Initializes a new instance of <see cref="WriteOnceExpandoObject"/>
        /// </summary>
        /// <param name="populate">Action that gets called during creation for populate the object</param>
        public WriteOnceExpandoObject(Action<dynamic> populate)
        {
            _construction = true;
            populate(this);
            _construction = false;
        }

#pragma warning disable 1591 // Xml Comments
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this[binder.Name] = value;
            return base.TrySetMember(binder, value);
        }

        public void Add(string key, object value)
        {
            ThrowIfNotUnderConstruction();
            _actualDictionary.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _actualDictionary.ContainsKey(key);
        }

        public ICollection<string> Keys { get { return _actualDictionary.Keys; } }

        public bool Remove(string key)
        {
            ThrowIfNotUnderConstruction();
            return _actualDictionary.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _actualDictionary.TryGetValue(key, out value);
        }

        public ICollection<object> Values { get { return _actualDictionary.Values; } }

        public object this[string key]
        {
            get { return _actualDictionary[key]; }
            set 
            {
                ThrowIfNotUnderConstruction();
                _actualDictionary[key] = value; 
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            ThrowIfNotUnderConstruction();
            _actualDictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            ThrowIfNotUnderConstruction();
            _actualDictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _actualDictionary.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count { get { return _actualDictionary.Count; } }
        public bool IsReadOnly { get { return false; } }

        public bool Remove(KeyValuePair<string, object> item)
        {
            ThrowIfNotUnderConstruction();
            return _actualDictionary.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _actualDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _actualDictionary.GetEnumerator();
        }


        void ThrowIfNotUnderConstruction()
        {
            if( !_construction )
                throw new ReadOnlyObjectException();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
