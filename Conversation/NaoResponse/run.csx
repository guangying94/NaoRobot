#load "qnaObj.csx"
#load "sentimentObj.csx"
#load "updateTable.csx"
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
using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // parse query parameter
    string query = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "query", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    query = query ?? data?.query;

    // Process sentiment and get answer from QnAmaker
    string answer = "";
    double score = await SentimentAnalysis.MakeRequests(query);

    if (score > 0.4)
    {
        answer = await qnaMaker.GetFAQ(query);
        if (answer == "No good match found in the KB")
        {
            await UpdateTable.queryTable(query);
            answer = "sorry, I'm still learning, cannot answer you now.";
        }
        else
        {
            await UpdateTable.converTable(query, answer, score);
        }
    }
    else
    {
        answer = "you hurt my little heart, how could you say that to me?";
        await UpdateTable.converTable(query, answer, score);
    }

    return query == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Sorry, please try again!")
        : req.CreateResponse(HttpStatusCode.OK, answer);
}
