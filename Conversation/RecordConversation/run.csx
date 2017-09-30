#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ICollector<Conversation> outputTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    string query = data?.query;
    string answer = data?.answer;
    double sentiment = data?.sentiment;

    if (query == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Query not found.");
    }

    outputTable.Add(new Conversation()
    {
        PartitionKey = "Functions",
        RowKey = Guid.NewGuid().ToString(),
        Query = query,
        Answer = answer,
        Sentiment = sentiment
        
    });
    return req.CreateResponse(HttpStatusCode.Created);
}

public class Conversation : TableEntity
{
    public string Query { get; set; }
    public string Answer {get;set;}
    public double Sentiment {get; set;}
}