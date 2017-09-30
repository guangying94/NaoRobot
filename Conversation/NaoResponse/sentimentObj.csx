#r "Newtonsoft.Json"

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class Document

{
    public double score { get; set; }
    public string id { get; set; }
}

public class sentimentObj
{
    public IList<Document> documents { get; set; }
    public IList<object> errors { get; set; }
}

public class SentimentAnalysis
{
    private const string baseURL = "https://westus.api.cognitive.microsoft.com/";
    private const string AccountKey = "<Text Analytics API Key>";

    public static async Task<double> MakeRequests(string input)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            byte[] byteData = Encoding.UTF8.GetBytes("{\"documents\":[" +
            "{\"id\":\"1\",\"text\":\"" + input + "\"},]}");
            var uri = "text/analytics/v2.0/sentiment";
            var response = await CallEndpoint(client, uri, byteData);
            return response.documents[0].score;
        }
    }

    public static async Task<sentimentObj> CallEndpoint(HttpClient client, string uri, byte[] byteData)
    {
        using (var content = new ByteArrayContent(byteData))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();
            sentimentObj sentimentJSON = JsonConvert.DeserializeObject<sentimentObj>(result);
            return sentimentJSON;
        }
    }
}