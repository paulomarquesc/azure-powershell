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

    using System.Management.Automation;
    using System.Security.Permissions;
    using Microsoft.Azure.Commands.CosmosDb.Common;
    using Microsoft.Azure.CosmosDB.Table;
    using Microsoft.Azure.Commands.ResourceManager.Common;

    /// <summary>
    /// list azure tables
    /// </summary>
    [Cmdlet(VerbsCommon.Get, CosmosDbNouns.Table), OutputType(typeof(CloudTable))]
    public class GetAzureRmCosmosDbTableCommand : AzureRMCmdlet
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
            WriteObject(this.ResourceGroup);
        }

    }

}
