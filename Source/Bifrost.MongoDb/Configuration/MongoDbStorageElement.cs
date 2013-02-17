﻿#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
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

using Bifrost.Configuration;
using Bifrost.Configuration.Xml;

namespace Bifrost.MongoDB
{
    [ElementName("MongoDB")]
    public class MongoDbStorageElement : StorageElement
    {
        public MongoDbStorageElement()
        {
            EntityContextType = typeof(EntityContext<>);
        }

        public string ConnectionString { get; set; }
        public string Database { get; set; }

        public override IEntityContextConfiguration GetConfiguration()
        {
            var entityContextConfiguration = new EntityContextConfiguration();
            var connection = new EntityContextConnection(ConnectionString, Database);
            entityContextConfiguration.Connection = connection;
            return entityContextConfiguration;
        }
    }
}
