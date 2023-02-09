using System.Net.Http.Headers;
using System.Text.Json;
using InputOutput.Models;
using Microsoft.AspNetCore.Mvc;

namespace InputOutput.Controllers;

[ApiController]
[Route("")]
public class InputOutputController : ControllerBase
{
    [HttpGet]
    [Route("StringParser")]
    public IEnumerable<String> Get([FromQuery] String str)
    {

        var splited = str.Split(",").Select(s => s.Trim());
        return splited;
    }

    [HttpGet]
    [Route("StringParser2")]
    public IEnumerable<String> Get2([FromQuery] String str)
    {

        var splited = str.Split(",").Select(s => s.Trim());
        return splited;
    }

    /*
    Task 2: create a Controller and Call this API  https://files.nccih.nih.gov/test-sample.json 
 
Create a Table/Grid and bind the response from API. 
Return the following: 
id, title, grant_id, grant_type where  “grant_type” have R21 
*/

    [HttpGet]
    [Route("ConsumeApi")]
    public async Task<IActionResult> ConsumeApi()
    {
        // using var client = new HttpClient();
        // client.BaseAddress = new Uri("https://files.nccih.nih.gov");
        // client.DefaultRequestHeaders.Accept.Clear();
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("plain/text"));

        // var uri = new Uri(Uri baseUrl, string relative uri);

        // var request = new HttpRequestMessage(HttpMethod.Get, "https://files.nccih.nih.gov/test-sample.json");

        // request.Headers.Add("Accept", "application/json");

        // Console.WriteLine(request.RequestUri);
        // HttpResponseMessage response = await client.GetAsync("test-sample.json");
        
        // var content = await response.Content.ReadAsStringAsync();
        // Console.WriteLine(content);

        // response.EnsureSuccessStatusCode();

        // var json = JsonSerializer.Deserialize<TestSample>(content);

        // var client = new HttpClient();
        // client.BaseAddress = new Uri("https://dummyjson.com/products/1");
        // client.DefaultRequestHeaders.Accept.Clear();
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // HttpResponseMessage response = await client.GetAsync("products/1");
        // Console.WriteLine(response);
        // response.EnsureSuccessStatusCode();
        // var result = await response.Content.ReadAsStringAsync();
        // return Ok(result);

        var client = new HttpClient();

        string _baseAddress = "https://files.nccih.nih.gov";
string endpoint = "api/School";

client.BaseAddress = new Uri(_baseAddress);
string result = client.GetStringAsync(endpoint).Result.ToString();

return Ok(result);
    }
    
}
