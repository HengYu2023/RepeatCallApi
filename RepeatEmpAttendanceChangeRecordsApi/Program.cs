using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;


string baseUrl = "https://uat-pt-be.mayohr.com/api/EmpAttendanceChangeRecords/";

string authorizationToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjE2MTBjMTQ1ODdjMGUzY2U0YTk1N2IyMzlhODM3MzIwIn0.eyJpc3MiOiJodHRwczovL3VhdC1hc2lhYXV0aC5tYXlvaHIuY29tL3N0cyIsImlhdCI6MTcxNzczOTMxNSwiYXVkIjoiNDNmNWJjZGEtZjM0YS00YzA1LTk3ZGMtZTkxZWQxNTI3MzYzIiwibm9uY2UiOiJPRFUwTXpreE5XTXRNV1UxTXkwME9EUXpMV0UwWWpBdFpXWmpZV0U1TmpZd01URXhRRzFoZVc4M056Y3RZVEF3TURBeFFERTNNVGMzTXprek1UVT0iLCJleHAiOjE3MTgzNDQxMTMsIm5iZiI6MTcxNzczOTMxMywianRpIjoiY2E5NjU1ZmVmZDA4NDUyZGFjNGZkNGNmNzRiMDJlNGUiLCJzdWIiOiIyMDk3MGI0MzQ1MjM0OTQ4YjkwY2NlM2UxNjVmZTU0N2NjZWY0ZjAxY2UxODRiMTRhMmFmYmU1ODViNDllMjFhIiwiYW1yIjoicHdkIiwiaXBhZGRyIjoiMTkyLjE2OC4xNy40NyIsIm9pZCI6Ijg3ZTMxN2M2LWUyYzUtNDc3MC1iMmFjLWU5YzQ1NDVhMzMzOSIsInVpZCI6Ijg3ZTMxN2M2LWUyYzUtNDc3MC1iMmFjLWU5YzQ1NDVhMzMzOSIsImlkcCI6Im1heW8gSFJNIiwiZWlkIjoiMDNhYzlkYjItMTVkNS00NGMyLWJhNTctOTRkNTYxNDE0ODBkIiwiY2lkIjoiZGMzNGFmZjYtMmYwYy00OGUwLTk1MTMtZDYyNTE1Yjk2MjViIiwidWJtIjoiTm9uZSIsInJ0IjowLCJzZXAiOjEsInpvbmVpbmZvIjoiKzA4OjAwIn0.oJgh0sLqbw_jT7s70-y7EDlFZJMImD45m0Y-ctNhbRe6PJsusJvi-dZQ2TBUiQPafOwEj2voNKwXqYjIMXMSwrexo32mrEk86MxsAyykg85Xl_98Ic-xDM5K7tVnoTkS-Hh7f8wWlG67BwR0yPxnsQKvtzGyDLPW79Wntqccz5MUZOn6_IBWn_amNoRuf7COAcreXC8uZygLtkaheBqH3VaXWd2aTSJkMTsTOay5KmTjv0YsyqP3QIFCdK2Y2FWDeD9ADj9wd1RSEML3jroNRyo8dheL67dvGlmTyp3eK3iduCw0Y4SVjJ9BELCoI0gT5yeyq9-m2ySOV4XrB-K8YA";


List<Record> records = new List<Record>()
{
    new Record()
    {
        empolyeeId = "1D759338-B703-4107-8DF8-67A2BE69B177",
        AttendanceIds = new List<Attendance>()
        {
            new Attendance { RecordID = "379BCF8E-49ED-4248-AB6B-E45A18F12141" },
            new Attendance { RecordID = "8C413FD6-1511-449F-87ED-F3B666501B05" },
            new Attendance { RecordID = "4C5A0624-C30E-4D8F-91F5-62F4D7C158BB" },
            new Attendance { RecordID = "86DFDC6B-B7CE-4467-9DE5-767E4E275DFF" }
        }
        
    },
    new Record()
    {
        empolyeeId = "1D759338-B703-4107-8DF8-67A2BE69B177",
        AttendanceIds = new List<Attendance>()
        {
            new Attendance { RecordID = "F9433E9B-F518-4EE6-89DE-F8024D84C4D7" },
            new Attendance { RecordID = "FE14B5ED-2D99-4A09-B7D1-0B70223A4880" },
            new Attendance { RecordID = "2E57E6E2-D5A8-48BB-8F43-63152C0A796D" },
            new Attendance { RecordID = "4207953E-68BE-4812-AC18-45B3872FBEE1" }
        }
        
    },
};







    
        foreach (var record in records)
        {
            using HttpClient client = new HttpClient();
            var url = baseUrl + record.empolyeeId;
            var requestBody = record.AttendanceIds;
            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = content,
            };

            // 添加 Headers
            request.Headers.Add("accept", "*/*");
            request.Headers.Add("accept-language", "zh-tw");
            request.Headers.Add("actioncode", "Delete");
            //request.Headers.Add("content-type", "application/json");
            request.Headers.Add("functioncode", "StaffBasicInfo");
            request.Headers.Add("origin", "https://uat-tube.mayohr.com");
            request.Headers.Add("priority", "u=1, i");
            request.Headers.Add("referer", "https://uat-tube.mayohr.com/");
            request.Headers.Add("sec-ch-ua",
                "\"Microsoft Edge\";v=\"125\", \"Chromium\";v=\"125\", \"Not.A/Brand\";v=\"24\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-site");
            request.Headers.Add("user-agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36 Edg/125.0.0.0");
            request.Headers.Add("Authorization", authorizationToken);

            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response for GUID {record.empolyeeId}: {responseBody}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request failed for GUID {record.empolyeeId}: {ex.Message}");
            }
        }
    




Console.WriteLine("Hello, World!");

public class Attendance
{
    public string RecordID { get; set; }
}


public class Record
{
    public string empolyeeId { get; set; }
    public List<Attendance> AttendanceIds { get; set; }
}
