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

    using Commands.Common.Authentication.Abstractions;
    using Microsoft.Azure.Commands.Common.Authentication;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Components;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Entities.ErrorResponses;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Extensions;
    using Microsoft.Azure.Commands.CosmosDb.Extensions;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.RestClients;
    using ProjectResources = Microsoft.Azure.Commands.CosmosDb.Properties.Resources;
    using Microsoft.Azure.Commands.ResourceManager.Common;
    using System;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The base class for all Windows Azure Recovery Services commands
    /// </summary>
    public abstract class CosmosDbBaseCmdlet : AzureRMCmdlet
    {

        /// <summary>
        /// Gets a new instance of the <see cref="ResourceManagerRestRestClient"/>.
        /// </summary>
        public ResourceManagerRestRestClient GetResourcesClient()
        {
            var endpoint = DefaultContext.Environment.GetEndpoint(AzureEnvironment.Endpoint.ResourceManager);

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ApplicationException(ProjectResources.azureResourceManagerEndPointNotSet);
            }

            var endpointUri = new Uri(endpoint, UriKind.Absolute);

            return new ResourceManagerRestRestClient(
                endpointUri: endpointUri,
                httpClientHelper: HttpClientHelperFactory.Instance
                .CreateHttpClientHelper(
                        credentials: AzureSession.Instance.AuthenticationFactory
                                                 .GetSubscriptionCloudCredentials(
                                                    DefaultContext,
                                                    AzureEnvironment.Endpoint.ResourceManager),
                        headerValues: AzureSession.Instance.ClientFactory.UserAgents,
                        cmdletHeaderValues: this.GetCmdletHeaders()));
        }

        private Dictionary<string, string> GetCmdletHeaders()
        {
            return new Dictionary<string, string>
            {
                {"ParameterSetName", this.ParameterSetName },
                {"CommandName", this.CommandRuntime.ToString() }
            };
        }

        /// <summary>
        /// The cancellation source.
        /// </summary>
        private CancellationTokenSource cancellationSource;

        /// <summary>
        /// Gets the cancellation source.
        /// </summary>
        protected CancellationToken? CancellationToken
        {
            get
            {
                return this.cancellationSource == null ? null : (CancellationToken?)this.cancellationSource.Token;
            }
        }

        /// <summary>
        /// The <c>BeginProcessing</c> method.
        /// </summary>
        protected override void BeginProcessing()
        {
            try
            {
                if (this.cancellationSource == null)
                {
                    this.cancellationSource = new CancellationTokenSource();
                }

                base.BeginProcessing();
                this.OnBeginProcessing();
            }
            catch (Exception ex)
            {
                if (ex.IsFatal())
                {
                    throw;
                }

                var capturedException = ExceptionDispatchInfo.Capture(ex);
                this.HandleException(capturedException: capturedException);
            }
        }

        /// <summary>
        /// The <c>StopProcessing</c> method.
        /// </summary>
        protected override void StopProcessing()
        {
            try
            {
                if (this.cancellationSource != null && !this.cancellationSource.IsCancellationRequested)
                {
                    this.cancellationSource.Cancel();
                }

                this.OnStopProcessing();
                base.StopProcessing();
            }
            catch (Exception ex)
            {
                if (ex.IsFatal())
                {
                    throw;
                }

                var capturedException = ExceptionDispatchInfo.Capture(ex);
                this.HandleException(capturedException: capturedException);
            }
            finally
            {
                this.DisposeOfCancellationSource();
            }
        }

        /// <summary>
        /// The <c>EndProcessing</c> method.
        /// </summary>
        protected override void EndProcessing()
        {
            try
            {
                this.OnEndProcessing();
                base.EndProcessing();
            }
            catch (Exception ex)
            {
                if (ex.IsFatal())
                {
                    throw;
                }

                var capturedException = ExceptionDispatchInfo.Capture(ex);
                this.HandleException(capturedException: capturedException);
            }
            finally
            {
                this.DisposeOfCancellationSource();
            }
        }

        /// <summary>
        /// The <c>ProcessRecord</c> method.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            try
            {
                if (this.cancellationSource == null)
                {
                    this.cancellationSource = new CancellationTokenSource();
                }

                base.ExecuteCmdlet();
                this.OnProcessRecord();
            }
            catch (Exception ex)
            {
                if (ex.IsFatal())
                {
                    throw;
                }

                var capturedException = ExceptionDispatchInfo.Capture(ex);
                this.HandleException(capturedException: capturedException);
            }
        }

        /// <summary>
        /// When overridden, allows child classes to be called when the <c>ProcessRecord</c> method is invoked.
        /// </summary>
        protected virtual void OnProcessRecord()
        {
            // no-op
        }

        /// <summary>
        /// When overridden, allows child classes to be called when the <c>EndProcessing</c> method is invoked.
        /// </summary>
        protected virtual void OnEndProcessing()
        {
            // no-op
        }

        /// <summary>
        /// When overridden, allows child classes to be called when the <c>BeginProcessing</c> method is invoked.
        /// </summary>
        protected virtual void OnBeginProcessing()
        {
            // no-op
        }

        /// <summary>
        /// When overridden, allows child classes to be called when the <c>StopProcessing</c> method is invoked.
        /// </summary>
        protected virtual void OnStopProcessing()
        {
            // no-op
        }

        /// <summary>
        /// Provides specialized exception handling.
        /// </summary>
        /// <param name="capturedException">The captured exception</param>
        private void HandleException(ExceptionDispatchInfo capturedException)
        {
            try
            {
                var errorResponseException = capturedException.SourceException as ErrorResponseMessageException;
                if (errorResponseException != null)
                {
                    this.ThrowTerminatingError(errorResponseException.ToErrorRecord());
                }

                var aggregateException = capturedException.SourceException as AggregateException;
                if (aggregateException != null)
                {
                    if (aggregateException.InnerExceptions.CoalesceEnumerable().Any() &&
                        aggregateException.InnerExceptions.Count == 1)
                    {
                        errorResponseException = aggregateException.InnerExceptions.Single() as ErrorResponseMessageException;
                        if (errorResponseException != null)
                        {
                            this.ThrowTerminatingError(errorResponseException.ToErrorRecord());
                        }

                        this.ThrowTerminatingError(aggregateException.InnerExceptions.Single().ToErrorRecord());
                    }
                    else
                    {
                        this.ThrowTerminatingError(aggregateException.ToErrorRecord());
                    }
                }

                capturedException.Throw();
            }
            finally
            {
                this.DisposeOfCancellationSource();
            }
        }

        /// <summary>
        /// Disposes of the <see cref="CancellationTokenSource"/>.
        /// </summary>
        private void DisposeOfCancellationSource()
        {
            if (this.cancellationSource != null)
            {
                if (!this.cancellationSource.IsCancellationRequested)
                {
                    this.cancellationSource.Cancel();
                }

                this.cancellationSource.Dispose();
                this.cancellationSource = null;
            }
        }
    }



}