using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs;
using Amazon.Lambda.Core;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.AwsCloudWatch;
using SharedEntities;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.IO;
using Amazon;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSMarvin_Lambda
{
    public class Function
    {
        private Marvin.Marvin _marvin;

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public object FunctionHandler(object input, ILambdaContext context)
        {
            LambdaLogger.Log("Starting" + Environment.NewLine);

            AppSettings appSettings = new AppSettings()
            {
                PrivateKey = GetPrivateKey(),
                //Test Daedric_Address = "cx58ca994194cf0c6a2a68b789d81c70484a5675b3",
                //Test Network_Url = "https://bicon.net.solidwallet.io/api/v3",
                Daedric_Address= "cxcc711062b732ed14954008da8a5b5193b4d48618",
                Network_Url= "https://ctz.solidwallet.io/api/v3",
                testTransactions = false,
                Price_Increment = 3600000
            };
            
            var logger = new LoggerConfiguration()
                              .MinimumLevel.Information()
                              .WriteTo.LambdaLoggerSink()
                              .CreateLogger();

            _marvin = new Marvin.Marvin(appSettings, logger);
            _marvin.Run();
            return "OK";
        }

        public static string GetPrivateKey()
        {
            string secretName = "PrivateKey";
            string region = "ap-southeast-2";

            MemoryStream memoryStream = new MemoryStream();

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretName;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

            GetSecretValueResponse response = null;

            // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
            // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
            // We rethrow the exception by default.

            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                // Deal with the exception here, and/or rethrow at your discretion
                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (System.AggregateException ae)
            {
                // More than one of the above exceptions were triggered.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }

            string secretJsonString = "";

            // Decrypts secret using the associated KMS CMK.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response.SecretString != null)
            {
                secretJsonString = response.SecretString;
            }
            else
            {
                memoryStream = response.SecretBinary;
                StreamReader reader = new StreamReader(memoryStream);
                secretJsonString = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            }

            var secretData = (JObject)JsonConvert.DeserializeObject(secretJsonString);
            string secret = secretData[secretName].Value<string>();
            
            return secret;
        }
    }
}
