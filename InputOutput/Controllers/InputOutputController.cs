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

    [HttpGet]
    [Route("ConsumeApi")]
    public async Task<IActionResult> ConsumeApi()
    {
        try
        {
        var productValue = new ProductInfoHeaderValue("InputOutput", "1.0");
var commentValue = new ProductInfoHeaderValue("(+http://www.intrepion.com/InputOutput.html)");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(productValue);
            client.DefaultRequestHeaders.UserAgent.Add(commentValue);

            var response = await client.GetAsync("https://files.nccih.nih.gov/test-sample.json");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                var testSample = await JsonSerializer.DeserializeAsync<TestSample>(json);

                var filteredGrants = testSample?.TestData
                    .Where(td => td.GrantType.Contains("\"R21\""))
                    .Select(td => new
                    {
                        td.Id,
                        td.Title,
                        td.GrantId,
                        td.GrantType
                    });

                return Ok(filteredGrants);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound("The requested resource could not be found.");
            }
            else
            {
                return StatusCode(500, "Error retrieving grants data.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
