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
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");

            var response = await client.GetAsync("https://files.nccih.nih.gov/test-sample.json");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                var testSample = await JsonSerializer.DeserializeAsync<TestSample>(json);

                var filteredGrants = testSample?.TestData
                    .Where(td => td.GrantType == "R21")
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
