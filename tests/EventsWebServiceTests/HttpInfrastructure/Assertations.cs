using EventsWebServiceTests.ApiInfrastructure.Dtos;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System.Net;
using System.Text;

namespace EventsWebServiceTests.ApiInfrastructure
{
    internal static class Assertations
    {
        public static void AssertContentTypeIsApplicationJson(RestResponse response)
        {
            Assert.AreEqual("application/json", response.ContentType);
        }

        public static void AssertSuccessfulStatusCode(RestResponse response)
        {
            Assert.True(response.StatusCode == HttpStatusCode.OK, $"Expected Statuc Code to be {HttpStatusCode.OK}, but was {response.StatusCode}");
        }

        public static void AssertBadRequestStatusCode(RestResponse response)
        {
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest, $"Expected Statuc Code to be {HttpStatusCode.BadRequest}, but was {response.StatusCode}");
        }

        public static void AssertJsonSchema(RestResponse response, string schemaContent)
        {
            JSchema jsonSchema;
            var jsonResponse = JToken.Parse(response.Content);

            try
            {
                jsonSchema = JSchema.Parse(schemaContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Schema is not valid schema", ex);
            }
            AssertJsonSchema(response.Content, jsonSchema);
        }

        internal static void AssertEventSuccessStatusMessage(RestResponse response)
        {
            var actualEventResponseDto = JsonConvert.DeserializeObject<EventResponseDto>(response.Content);
            Assert.AreEqual("Event processed successfully", actualEventResponseDto.Status);
        }

        internal static void AssertEventBadRequestStatusMessage(RestResponse response, params string[] expectedMessages)
        {
            var mesages = JsonConvert.DeserializeObject<List<string>>(response.Content);
            CollectionAssert.AreEqual(expectedMessages.ToList(), mesages);
        }



        internal static void AssertEventIsPostedSuccessfully(RestResponse response)
        {
            var schema = ResponseJsonSchemas.EventSuccsees();
            Assert.Multiple(() =>
            {
                AssertContentTypeIsApplicationJson(response);
                AssertSuccessfulStatusCode(response);
                AssertEventSuccessStatusMessage(response);
                AssertJsonSchema(response, schema);

            });
        }

        public static void AssertBadRequestResponse(RestResponse response, params string[] messages)
        {
            Assert.Multiple(() =>
            {
                AssertContentTypeIsApplicationJson(response);
                AssertBadRequestStatusCode(response);
                AssertEventBadRequestStatusMessage(response, messages);
            });
        }

        private static void AssertJsonSchema(string bodyJson, JSchema jsonSchema)
        {
            IList<string> messages;
            var trimmedContent = bodyJson.TrimStart();
            bool isSchemaValid =
                 trimmedContent.StartsWith("{", StringComparison.Ordinal)
                     ? JObject.Parse(trimmedContent).IsValid(jsonSchema, out messages)
                     : JArray.Parse(trimmedContent).IsValid(jsonSchema, out messages);
            if (!isSchemaValid)
            {
                var sb = new StringBuilder();
                sb.AppendLine("JSON Schema is not valid. Error Messages:");
                foreach (var errorMessage in messages)
                {
                    sb.AppendLine(errorMessage);
                }
                throw new AssertionException(sb.ToString());
            }
        }
    }
}
