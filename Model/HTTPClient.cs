using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Web_Browser_CW1 {

    /// <summary>
    /// Represents the result of an HTTP request, including status code, response body,
    /// the page title and favicon.
    /// </summary>
    /// <remarks>Used to encapsulate the essential information returned from an HTTP response.<br/>
    /// The <see cref="favicon"/> is provided for display purposes. and may be <see langword="null"/> if no favicon is available <br/>
    /// The <see cref="statusCode"/> field indicates the HTTP status of the response,<br/>
    /// and <see cref="body"/> contains the raw response content. <br/>
    /// </remarks>
    struct HttpResponse {

        public string title;
        public Bitmap? favicon;
        public string body;
        public HttpStatusCode statusCode;

        public HttpResponse(string title, Bitmap favicon, string body, HttpStatusCode statusCode) {
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

    /// <summary>
    /// Provides functionality to perform HTTP GET requests and retrieve web page information, including the page title,
    /// response body, status code, and favicon.
    /// </summary>
    internal class HTTPClient {

        private static readonly HttpClient client = new HttpClient();

        public HTTPClient() {}

        /// <summary>
        /// Sends an asynchronous HTTP GET request to the specified URL and retrieves the response, including the page
        /// title, favicon, response body, and status code.
        /// </summary>
        /// <remarks>If the favicon cannot be retrieved, the returned <see cref="HttpResponse"/> will not
        /// include a favicon. The method attempts to extract the page title from the HTML response. Network errors or
        /// invalid URLs will result in a response with default values and an error message.</remarks>
        /// <param name="url">The URL of the web page to request. Must be a valid, absolute URI; otherwise, the request may fail.</param>
        /// <returns>A <see cref="HttpResponse"/> containing the page title, favicon (if available), response body, and HTTP
        /// status code. If an error occurs, the response contains default values and an error message.</returns>
        public static async Task<HttpResponse> Get(string url) {

            string title = "website";
            Bitmap? favicon = null;
            string responseBody = "";
            HttpStatusCode statusCode = 0;

            try {

                // submit HTTP request
                using HttpResponseMessage response = await client.GetAsync(url);

                responseBody = await response.Content.ReadAsStringAsync();
                statusCode = response.StatusCode;

                // Get title of page
                Regex reg = new Regex("<title>(.*)</title>");
                MatchCollection match = reg.Matches(responseBody);
                title = "website";

                if (match.Count == 1) {
                    title = match.First().Value;
                    title = title.Substring(7);
                    title = title.Substring(0, title.Length - 8);
                }

            } catch (Exception ex) {

                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return new HttpResponse("website", "Unknown error please try again later", 0);
            
            }

            // Get favicon
            try {
                favicon = new Bitmap(await client.GetStreamAsync(url + "/favicon.ico"));

            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            if (favicon != null) {
                return new HttpResponse(title, favicon, responseBody, statusCode);
            } else {
                return new HttpResponse(title, responseBody, statusCode);
            }
        }
    }
}
