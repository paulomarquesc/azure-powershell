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

namespace Microsoft.Azure.Commands.CosmosDb.Table.Cmdlet
{
    using System;
    using System.Management.Automation;
    using System.Security.Permissions;
    using Microsoft.Azure.CosmosDb.Common;
    using Microsoft.Azure.Commands.CosmosDb.Common;
    using Microsoft.Azure.CosmosDB.Table;
    using Newtonsoft.Json.Linq;
    using ProjectResources = Microsoft.Azure.Commands.CosmosDb.Properties.Resources;
    using Microsoft.Azure.Storage;

    /// <summary>
    /// list azure tables
    /// </summary>
    [Cmdlet(VerbsCommon.Get, CosmosDbNouns.Table), OutputType(typeof(CloudTable))]
    public class GetAzureRmCosmosDbTableCommand : CosmosDbBaseCmdlet
    {
        [Parameter(Position = 0,
                    HelpMessage = "Resource Group name",
                    Mandatory = true,
                    ValueFromPipeline = false,
                    ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroup { get; set; }

        [Parameter(Position = 1,
                    HelpMessage = "Cosmos Db Account name",
                    Mandatory = true,
                    ValueFromPipeline = false,
                    ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string CosmosDbAccount { get; set; }

        [Parameter(Position = 2,
            HelpMessage = "Table name",
            Mandatory = true,
            ValueFromPipeline = false,
            ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string TableName { get; set; }

        /// <summary>
        /// Initializes a new instance of the GetAzureStorageTableCommand class.
        /// </summary>
        public GetAzureRmCosmosDbTableCommand()
        {
        }

        /// <summary>
        /// Execute command
        /// </summary>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override void ExecuteCmdlet()
        {
            string resourceType = "Microsoft.DocumentDb/databaseAccounts";
            string apiVersion = "2015-04-08";
            string action = "listKeys";

            // Obtaining the resource Id of CosmosDB Account
            Guid subscriptionId = new Guid (DefaultContext.Subscription.Id);
            string cosmosDbResourceId = CosmosDbCoreHelper.GetCosmosDbResourceId(
                    subscriptionId: subscriptionId,
                    resourceGroupName: this.ResourceGroup,
                    resourceType: resourceType,
                    cosmosDbAccountName: this.CosmosDbAccount
                );

            // Obtaining Cosmos DB Keys
            var operationResult = this.GetResourcesClient()
                .InvokeActionOnResource<JObject>(
                    resourceId: cosmosDbResourceId,
                    action: action,
                    apiVersion: apiVersion,
                    cancellationToken: this.CancellationToken.Value)
                .Result;

            CosmosDbKeyInfo keys = CosmosDbCoreHelper.GetKeys(operationResult);

            if (keys == null)
            {
                throw new Exception(ProjectResources.null_cosmosDbKeyInfoObject);
            }

            // Creating the table client object and returning
            string connString = CosmosDbCoreHelper.GetCosmosDbConnectionString(CosmosDbAccount, keys.PrimaryMasterKey);
            CloudStorageAccount cosmosDbAccount = CloudStorageAccount.Parse(connString);
            CloudTableClient tableClient = cosmosDbAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName: this.TableName);

            // Creating table if it does not exist
            table.CreateIfNotExists();

            WriteObject(table);
        }
    }
}
