using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using api;
using System.Configuration;
using System.Net.Http;

namespace Company.Function
{
    public static class GetResumeCounter
    {
        [FunctionName("GetResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "AzureResume", containerName: "Counter", Connection = "AzureResumeConnectionString", Id = "1", PartitionKey = "1")] Counter counter, // Fixed parameter names
            [CosmosDB(databaseName: "AzureResume", containerName: "Counter", Connection = "AzureResumeConnectionString", Id = "1", PartitionKey = "1")] out Counter updatedCounter, // Fixed parameter names
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            updatedCounter = counter; // Added to avoid IDE0060 error
            updatedCounter.Count++; // Fixed increment

            var jsonToReturn = JsonConvert.SerializeObject(counter); // Fixed variable name

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK) // Fix typo
            {
                Content = new StringContent(jsonToReturn, System.Text.Encoding.UTF8, "application/json") // Fix property name
            };

        }
    }
}
