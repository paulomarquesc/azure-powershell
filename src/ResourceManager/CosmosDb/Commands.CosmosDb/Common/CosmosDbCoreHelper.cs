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


namespace Microsoft.Azure.CosmosDb.Common
{
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Components;
    using Microsoft.Azure.Commands.CosmosDb.Common;
    using ProjectResources = Microsoft.Azure.Commands.CosmosDb.Properties.Resources;
    using System;

    /// <summary>
    /// Class that contains helper functions for Cosmos Db
    /// </summary>
    public static class CosmosDbCoreHelper
    {
        
        /// <summary>
        /// Returns the resource Id of a Cosmsos DB Account
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="resourceGroupName"></param>
        /// <param name="resourceType"></param>
        /// <param name="cosmosDbAccountName"></param>
        /// <returns>string</returns>
        public static string GetCosmosDbResourceId(Guid subscriptionId, string resourceGroupName, string resourceType, string cosmosDbAccountName)
        {
            return ResourceIdUtility.GetResourceId(
                                                    subscriptionId: subscriptionId,
                                                    resourceGroupName: resourceGroupName,
                                                    resourceType: resourceType,
                                                    resourceName: cosmosDbAccountName
            );
        }

        /// <summary>
        /// Returns a CosmosDbKeyInfo object with all Cosmos Db Account related keys
        /// </summary>
        /// <param name="listKeysResponse"></param>
        /// <returns>CosmosDbKeyInfo</returns>
        public static CosmosDbKeyInfo GetKeys(OperationResult listKeysResponse)
        {
            if (listKeysResponse == null)
            {
                throw new ArgumentException(ProjectResources.null_listkeys);
            }

            return (
                new CosmosDbKeyInfo(listKeysResponse.Value)
            );
        }

        /// <summary>
        /// Returns a connection string for Cosmos DB.
        /// </summary>
        /// <param name="cosmosDbAccountName"></param>
        /// <param name="key"></param>
        /// <returns>string</returns>
        public static string GetCosmosDbConnectionString(string cosmosDbAccountName, string key)
        {
            return (
                string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};TableEndpoint=https://{0}.table.cosmosdb.azure.com", cosmosDbAccountName,key)
            );

        }



    }

}