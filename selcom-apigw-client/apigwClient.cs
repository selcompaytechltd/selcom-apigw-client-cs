using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.IO;

namespace selcom_apigw_client
{
    public class apigwClient
    {
        private String baseUrl;
        private String apiKey;
        private String apiSecret;

        public apigwClient(String baseUrl, String apiKey, String apiSecret)
        {
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public Dictionary<String, String> computeHeader(Dictionary<String, Object> dictData)
        {
            byte[] bapiKey = Encoding.Default.GetBytes(apiKey);
            String authToken = "SELCOM " + Convert.ToBase64String(bapiKey);

            String timestamp = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:sszzzz");

            String signedFields = "";
            String digestData = "timestamp=" + timestamp;


            foreach (KeyValuePair<string, Object> entry in dictData)
            {
                digestData = digestData + "&" + entry.Key + "=" + entry.Value.ToString();
                if (string.IsNullOrEmpty(signedFields))
                { signedFields = signedFields + entry.Key; }
                else
                { signedFields = signedFields + "," + entry.Key; }
            }
            
            byte[] bapiSecret = Encoding.Default.GetBytes(apiSecret);
            byte[] bdigestData = Encoding.Default.GetBytes(digestData);

            HMACSHA256 hmac = new HMACSHA256(bapiSecret);
            byte[] bhasdheData = hmac.ComputeHash(bdigestData);
            String digest = Convert.ToBase64String(bhasdheData);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Add("authToken", authToken);
            headers.Add("timestamp", timestamp);
            headers.Add("digest", digest);
            headers.Add("signedFields", signedFields);
            return headers;

        }

        public String postFunc(String path, Dictionary<String, Object> dictData)
        {
            Dictionary<String, String> headers = computeHeader(dictData);
            var client = new HttpClient();
            String  url = baseUrl + path;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,

            };


            request.Headers.Add("Authorization", headers["authToken"]);
            request.Headers.Add("Digest-Method", "HS256");
            request.Headers.Add("Digest", headers["digest"]);
            request.Headers.Add("Timestamp", headers["timestamp"]);
            request.Headers.Add("Signed-Fields", headers["signedFields"]);

            var jsonData = JsonConvert.SerializeObject(dictData);
            var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;
            var respnse = client.SendAsync(request).GetAwaiter().GetResult();
            var respnseData = respnse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return respnseData;



        }

        public String getFunc(String path, Dictionary<String, Object> dictData)
        {
            Dictionary<String, String> headers = computeHeader(dictData);
            var client = new HttpClient();
            String url = baseUrl + path;


            var parameters = "";
            foreach (KeyValuePair<string, Object> entry in dictData)
            {
                if (string.IsNullOrEmpty(parameters))
                { parameters = "?" + entry.Key + "=" + entry.Value.ToString(); }
                else
                { parameters = parameters + "&" + entry.Key + "=" + entry.Value.ToString(); }
            }
            url = url + parameters;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,

            };
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", headers["authToken"]);
            request.Headers.Add("Digest-Method", "HS256");
            request.Headers.Add("Digest", headers["digest"]);
            request.Headers.Add("Timestamp", headers["timestamp"]);
            request.Headers.Add("Signed-Fields", headers["signedFields"]);
            var respnse = client.SendAsync(request).GetAwaiter().GetResult();
            var respnseData = respnse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return respnseData;


        }

        public String deleteFunc(String path, Dictionary<String, Object> dictData)
        {
            Dictionary<String, String> headers = computeHeader(dictData);
            var client = new HttpClient();
            String url = baseUrl + path;


            var parameters = "";
            foreach (KeyValuePair<string, Object> entry in dictData)
            {
                if (string.IsNullOrEmpty(parameters))
                { parameters = "?" + entry.Key + "=" + entry.Value.ToString(); }
                else
                { parameters = parameters + "&" + entry.Key + "=" + entry.Value.ToString(); }
            }
            url = url + parameters;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Delete,

            };
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", headers["authToken"]);
            request.Headers.Add("Digest-Method", "HS256");
            request.Headers.Add("Digest", headers["digest"]);
            request.Headers.Add("Timestamp", headers["timestamp"]);
            request.Headers.Add("Signed-Fields", headers["signedFields"]);

            var respnse = client.SendAsync(request).GetAwaiter().GetResult();
            var respnseData = respnse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return respnseData;



        }
    }

}
