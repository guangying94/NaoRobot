#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ICollector<Unanswered> outputTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    string query = data?.query;
    string status = "open";

    if (query == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Query not found.");
    }

    outputTable.Add(new Unanswered()
    {
        PartitionKey = "Functions",
        RowKey = Guid.NewGuid().ToString(),
        Query = query,
        Status = status
        
    });
    return req.CreateResponse(HttpStatusCode.Created);
}

public class Unanswered : TableEntity
{
    public string Query { get; set; }
    public string Status { get; set; }
}