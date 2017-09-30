using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class UpdateTable
{
    private const string converURL = "<Table Storage URL>";
    private const string converKey = "<Table Storage Key>";
    private const string queryURL = "<Table Storage URL>";
    private const string queryKey = "<Table Storage Key>";
  
    public static async Task<string> converTable(string query, string answer, double sentiment)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(converURL + converKey);
            byte[] byteData = Encoding.UTF8.GetBytes("{\"query\": \"" + query + "\",\n    \"answer\": \"" + answer + "\",\n  \"sentiment\": " + Math.Round(sentiment,3).ToString() + "}");
            var itemContent = new ByteArrayContent(byteData);
            itemContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(converURL + converKey, itemContent);
            return "ok";
        }
    }   

    public static async Task<string> queryTable(string query)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(queryURL + queryKey);
            byte[] byteData = Encoding.UTF8.GetBytes("{\"query\": \"" + query + "\"}");
            var itemContent = new ByteArrayContent(byteData);
            itemContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(queryURL + queryKey, itemContent);
            return "ok";
        }
    }
}