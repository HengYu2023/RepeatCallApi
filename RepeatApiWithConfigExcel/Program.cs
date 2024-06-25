using System.Net.Http.Headers;
using System.Text;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;


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

var filePath = "Records.xlsx"; 
var records = ReadExcelFile(filePath);
var failInfos = new StringBuilder();
var currentCount = 0;

using var client = new HttpClient();
foreach (var record in records)
{
    currentCount++;
    var urlPara = record.UrlPara;
    var url = baseUrl + urlPara;
    var requestBody = record.RequestBody;
    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
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
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseBody);
        }
        //response.EnsureSuccessStatusCode();
        Console.WriteLine(
            $"recordNo {currentCount}, para {urlPara}, Body {requestBody}, {responseBody}");
    }
    catch(Exception ex)
    {
        var failInfo = $"Failed for recordNo {currentCount}, para {urlPara}, Body {requestBody}, {ex.Message} {Environment.NewLine}";
        Console.Write(failInfo);
        failInfos.AppendLine(failInfo);
    }
}

if (failInfos.Length != 0)
{
    Console.Write(failInfos.ToString());
}

Console.WriteLine($"API executed {currentCount} times");
Console.WriteLine("Press any key to exit.");
Console.ReadKey();


static List<Record> ReadExcelFile(string filePath)
{
    var records = new List<Record>();

    using var workbook = new XLWorkbook(filePath);
    var worksheet = workbook.Worksheet(1); // 第一個工作表
    var rows = worksheet.RangeUsed().RowsUsed().Skip(1); //去除第一筆資料

    foreach (var row in rows)
    {
        var urlPara = row.Cell(1).GetString();
        if (urlPara == "urlPara")
        {
            throw new Exception("please check excel format");
        }
        var requestBody = row.Cell(2).GetString();
        records.Add(new Record { UrlPara = urlPara, RequestBody = requestBody });
    }

    return records;
}


public class Record
{
    public string UrlPara { get; set; }
    public string RequestBody { get; set; }
}