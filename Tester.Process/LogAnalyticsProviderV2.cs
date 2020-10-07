using Microsoft.Azure.OperationalInsights;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlertTester.Process.Providers
{
    [ExcludeFromCodeCoverageAttribute]
    public class CustomCredentialsV2 : ServiceClientCredentials
    {
        private string AuthenticationToken { get; set; }

        public string ClientId { get; set; }

        public string ClientKey { get; set; }

        public const string authEndpoint = "https://login.microsoftonline.com";
        public const string tokenAudience = "https://api.loganalytics.io/";
        public static readonly string WebAPIAadInstance = "https://login.windows.net/microsoft.com";

        public CustomCredentialsV2(string clientId, string clientKey)
        {
            ClientId = clientId;
            ClientKey = clientKey;
        }

        public override void InitializeServiceClient<T>(ServiceClient<T> client)
        {
            AuthenticationContext authContext = new AuthenticationContext(WebAPIAadInstance);
            // Acquire application token for Kusto:
            ClientCredential applicationCredentials = new ClientCredential(ClientId, ClientKey);
            var taskOutput = authContext.AcquireTokenAsync(tokenAudience, applicationCredentials);
            AuthenticationResult result = taskOutput.Result;
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            AuthenticationToken = result.AccessToken;
        }
    }

    [ExcludeFromCodeCoverageAttribute]
    public class LogAnalyticsProviderV2
    {
        public const string authEndpoint = "https://login.microsoftonline.com";
        public const string tokenAudience = "https://api.loganalytics.io/";
        public const string domain = "microsoft.onmicrosoft.com";
        public string LoganalyClientId;
        public string LoganalyClientKey;

        public LogAnalyticsProviderV2(string aadclientid, string aadclientkey)
        {
            this.LoganalyClientId = aadclientid;
            this.LoganalyClientKey = aadclientkey;
        }

        /// <summary>
        /// Method to process Log analytics Query
        /// </summary>
        /// <param name="workspaceId">Log Analytics Workspace Id</param>
        /// <param name="aadclientid">AAD clientid</param>
        /// <param name="addclientkey">AAD clientkey</param>
        /// <param name="logQuery">Kusto Query to be processed</param>
        /// <returns></returns>
        public Tuple<double, Int64, Int64, bool> ProcessQOSQuery(string workspaceId, string aadclientid, string addclientkey, string logQuery)
        {
            return ProcessQOSQueryAsTask(workspaceId, aadclientid, addclientkey, logQuery).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Method to process Log analytics Query as task
        /// </summary>
        /// <param name="workspaceId">Log Analytics Workspace Id</param>
        /// <param name="aadclientid">AAD clientid</param>
        /// <param name="addclientkey">AAD clientkey</param>
        /// <param name="logQuery">Kusto Query to be processed</param>
        /// <returns></returns>
        public static async Task<Tuple<double, Int64, Int64, bool>> ProcessQOSQueryAsTask(string workspaceId, string aadclientid, string addclientkey, string logQuery)
        {
            Tuple<double, Int64, Int64, bool> retValue = new Tuple<double, Int64, Int64, bool>(-1, -1, -1, false);
            try
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
                var adSettings = new ActiveDirectoryServiceSettings
                {
                    AuthenticationEndpoint = new Uri(authEndpoint),
                    TokenAudience = new Uri(tokenAudience),
                    ValidateAuthority = true
                };
                var creds = ApplicationTokenProvider.LoginSilentAsync(domain, aadclientid, addclientkey, adSettings).Result;
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
                var client = new OperationalInsightsDataClient(creds);
                client.WorkspaceId = workspaceId;
                var results = client.Query(logQuery);
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
                bool result = false;
                int count = 0;
                if (results != null && results.Tables.Count > 0)
                {
                    result = results.Tables[0].Rows.Count > 0 ? true : false;
                    count = results.Tables[0].Rows.Count;

                    retValue = new Tuple<double, Int64, Int64, bool>(1, 1, count, result);
                }
                else
                    retValue = new Tuple<double, Int64, Int64, bool>(-1, -1, -1, false);
            }
            catch (Exception exp)
            {
                retValue = new Tuple<double, Int64, Int64, bool>(-2, -2, -2, false);
                Console.WriteLine("GetTelemetry=>" + exp.Message + exp.InnerException);
                throw;
            }
            return retValue;
        }
    }
}
