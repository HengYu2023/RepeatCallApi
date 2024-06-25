// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
var apiSettings = config.GetSection("ApiSettings");
var baseUrl = apiSettings["BaseUrl"];
var token = apiSettings["Token"];
var headersSection = apiSettings.GetSection("Headers");
var headers = new Dictionary<string, string>();



foreach (var section in headersSection.GetChildren())
{
    headers[section.Key] = section.Value;
}


List<Record> records = new List<Record>()
{
    new Record()
    {
        empolyeeId = "1D759338-B703-4107-8DF8-67A2BE69B177",
        AttendanceIds = new List<Attendance>()
        {
            new Attendance { RecordID = "52BC3013-8C3B-4B32-A52E-7F36F9CB6565" },
            new Attendance { RecordID = "D858FF65-D2B3-4C1C-978B-0FD220141077" },
            new Attendance { RecordID = "9CE2E04D-3E77-4391-9C3D-DF3B1A4E3A49" },
            new Attendance { RecordID = "8721B690-7694-4C47-9411-BE9C9A2F3458" }
        }
        
    },
    // new Record()
    // {
    //     empolyeeId = "1D759338-B703-4107-8DF8-67A2BE69B177",
    //     AttendanceIds = new List<Attendance>()
    //     {
    //         new Attendance { RecordID = "F9433E9B-F518-4EE6-89DE-F8024D84C4D7" },
    //         new Attendance { RecordID = "FE14B5ED-2D99-4A09-B7D1-0B70223A4880" },
    //         new Attendance { RecordID = "2E57E6E2-D5A8-48BB-8F43-63152C0A796D" },
    //         new Attendance { RecordID = "4207953E-68BE-4812-AC18-45B3872FBEE1" }
    //     }
    //     
    // },
};

using var client = new HttpClient();
foreach (var record in records)
{
    var urlPara = record.empolyeeId;
    var url = baseUrl + urlPara;
    var requestBody = record.AttendanceIds;
    var jsonContent = JsonConvert.SerializeObject(requestBody);
    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    var request = new HttpRequestMessage(HttpMethod.Delete, url)
    {
        Content = content,
    };
    
    request.Headers.Add("Authorization", token);
    foreach (var header in headers)
    {
        request.Headers.Add(header.Key, header.Value);
    }

    try
    {
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(
            $"para {urlPara}, Body {jsonContent}, {responseBody}");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Failed for para {urlPara}, Body {jsonContent}, {ex.Message}");
    }
}


public class Attendance
{
    public string RecordID { get; set; }
}


public class Record
{
    public string empolyeeId { get; set; }
    public List<Attendance> AttendanceIds { get; set; }
}

