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

public class qnaObj
{
    public Answer[] answers { get; set; }
}

public class Answer
{
    public string answer { get; set; }
    public string[] questions { get; set; }
    public float score { get; set; }
}

public class qnaMaker
{
    private const string qnamakerSubscriptionKey = "<QnAMaker API Key>";
    private const string kbID = "<QnAMaker KB ID>";

    public static async Task<string> GetFAQ(string queries)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey);
        var uri = "https://westus.api.cognitive.microsoft.com/qnamaker/v2.0/knowledgebases/" + kbID + "/generateAnswer";
        HttpResponseMessage response;
        byte[] byteData = Encoding.UTF8.GetBytes("{\"question\":\"" + queries + "\"}");
        using (var content = new ByteArrayContent(byteData))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();
            qnaObj qnaJSON = JsonConvert.DeserializeObject<qnaObj>(result);
            return qnaJSON.answers[0].answer;
        }
    }
}