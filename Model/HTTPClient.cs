using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web_Browser_CW1 {

    struct HttpResponse {

        public string title;
        public Icon? favicon;
        public string body;
        public HttpStatusCode statusCode;

        public HttpResponse(string title, Icon favicon, string body, HttpStatusCode statusCode) {
            this.title = title;
            this.favicon = favicon;
            this.body = body;
            this.statusCode = statusCode;
        }
        public HttpResponse(string title, string body, HttpStatusCode statusCode) {
            this.title = title;
            this.body = body;
            this.statusCode = statusCode;
            this.favicon = null;
        }

    }

    internal class HTTPClient {

        private static readonly HttpClient client = new HttpClient();

        public HTTPClient() {}

        public async Task<HttpResponse> Get(string url) {

            try {

                // submit HTTP request
                using HttpResponseMessage response = await client.GetAsync(url);
                String responseBody = await response.Content.ReadAsStringAsync();
                HttpStatusCode statusCode = response.StatusCode;

                // Get title of page
                Regex reg = new Regex("<title>(.*)</title>");
                MatchCollection match = reg.Matches(responseBody);
                string title = "website";

                if (match.Count == 1) {
                    title = match.First().Value;
                    title = title.Substring(7);
                    title = title.Substring(0, title.Length - 8);
                }

                // Get favicon
                Bitmap faviconBitmap = new Bitmap(await client.GetStreamAsync(url + "/favicon.ico"));
                System.IntPtr handle = faviconBitmap.GetHicon();
                Icon favicon = Icon.FromHandle(handle);

                return new HttpResponse(title, favicon, responseBody, statusCode);

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);
                return new HttpResponse("website", "Unknown error please try again later", 0);
            
            }
        }
    }
}
