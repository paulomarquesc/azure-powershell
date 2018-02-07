// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.CosmosDb.Common
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class that holds all keys from Cosmos Db Account
    /// </summary>
    public class CosmosDbKeyInfo
    {

        public CosmosDbKeyInfo()
        {
        }

        public CosmosDbKeyInfo(string primaryMasterKey, string secondaryMasterKey, string primaryReadonlyMasterKey, string secondaryReadonlyMasterKey)
        {
            this.PrimaryMasterKey = primaryMasterKey;
            this.SecondaryMasterKey = secondaryMasterKey;
            this.PrimaryReadonlyMasterKey = primaryReadonlyMasterKey;
            this.SecondaryReadonlyMasterKey = secondaryReadonlyMasterKey;
        }

        public CosmosDbKeyInfo(string jsonString)
        {
            SetValuesFromJson(jsonString);
        }

        /// <summary>
        /// Primary master key
        /// </summary>
        public string PrimaryMasterKey { get; set; }

        /// <summary>
        /// Secondary master key
        /// </summary>
        public string SecondaryMasterKey { get; set; }

        /// <summary>
        /// Primary read only key
        /// </summary>
        public string PrimaryReadonlyMasterKey { get; set; }

        /// <summary>
        /// Secondary read only key
        /// </summary>
        public string SecondaryReadonlyMasterKey { get; set; }

        /// <summary>
        /// Converts the Cosmos Db keys json string to anonymous object then set the obejct properties
        /// </summary>
        /// <param name="jsonString"></param>
        private void SetValuesFromJson(string jsonString)
        {
            var keyObjDef = new
            {
                PrimaryMasterKey = "",
                SecondaryMasterKey = "",
                PrimaryReadonlyMasterKey = "",
                SecondaryReadonlyMasterKey = ""
            };

            var keysObj = JsonConvert.DeserializeAnonymousType(jsonString, keyObjDef);

            this.PrimaryMasterKey = keysObj.PrimaryMasterKey;
            this.SecondaryMasterKey = keysObj.PrimaryMasterKey;
            this.PrimaryReadonlyMasterKey = keysObj.PrimaryReadonlyMasterKey;
            this.SecondaryReadonlyMasterKey = keysObj.SecondaryReadonlyMasterKey;
        }

    }
}
