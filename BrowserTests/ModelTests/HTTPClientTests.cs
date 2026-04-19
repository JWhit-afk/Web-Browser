using System.Diagnostics;
using Web_Browser_CW1;

namespace BrowserTests.ModelTests {

    [TestClass]
    public sealed class HTTPClientTests {

        //
        // Test Title
        //
        // 1. ✔️ Valid URL with title: https://www.google.com
        // 2. ✔️ Valid URL without title: https://httpbin.org/get
        //
        // Test Favicon
        //
        // 3. ✔️ Valid URL with favicon: https://www.google.com
        // 4. ✔️ Valid URL without favicon: https://httpbin.org/get
        //
        // Test Body
        //
        // 5. ✔️ Valid URL with body: https://www.google.com
        // 6. ✔️ Valid URL without body: https://httpbin.org/status/204
        //
        // Test Status Codes
        //
        // 7. ✔️ 200 OK: https://httpbin.org/status/200
        // 8. ✔️ 400 Bad Request: https://httpbin.org/status/400
        // 9. ✔️ 401 Unauthorized: https://httpbin.org/status/401
        // 10. ✔️ 403 Forbidden: https://httpbin.org/status/403
        // 11. ✔️ 404 Not Found: https://httpbin.org/status/404
        // 12. ✔️ 500 Internal Server Error: https://httpbin.org/status/500
        // 13. ✔️ 301 Moved Permanently: https://httpbin.org/status/301

        #region Title Grabbing Tests
        [TestMethod]
        public async Task HttpTitleTest() {
            HttpResponse response = await HTTPClient.Get("https://www.google.com");
            Assert.AreEqual("Google", response.title);
        }

        [TestMethod]
        public async Task HttpNoTitleTest() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/get");
            Assert.AreEqual("website", response.title); // Default title when no title is found in the HTML response is "website".
        }
        #endregion

        #region Favicon Grabbing Tests
        [TestMethod]
        public async Task HttpFaviconTest() {
            HttpResponse response = await HTTPClient.Get("https://www.google.com");
            Assert.IsNotNull(response.favicon);
        }

        [TestMethod]
        public async Task HttpNoFaviconTest() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/get");
            Assert.IsNull(response.favicon);
        }
        #endregion

        #region Body Grabbing Tests
        [TestMethod]
        public async Task HttpBodyTest() {
            HttpResponse response = await HTTPClient.Get("https://www.google.com");
            Assert.IsTrue(response.body.Length > 0);
        }

        [TestMethod]
        public async Task HttpNoBodyTest() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/204");
            Assert.AreEqual(string.Empty, response.body);
        }
        #endregion

        #region Status Code Tests
        [TestMethod]
        public async Task HttpStatusCode200Test() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/200");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.statusCode);
        }

        [TestMethod]
        public async Task HttpStatusCode400Test() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/400");
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.statusCode);
        }

        [TestMethod]
        public async Task HttpStatusCode401Test() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/401");
            Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, response.statusCode);
        }

        [TestMethod]
        public async Task HttpStatusCode403Test() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/403");
            Assert.AreEqual(System.Net.HttpStatusCode.Forbidden, response.statusCode);
        }

        [TestMethod]
        public async Task HttpStatusCode404Test() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/404");
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.statusCode);
        }

        [TestMethod]
        public async Task HttpStatusCode500Test() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/500");
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.statusCode);
        }

        [TestMethod]
        public async Task HttpStatusCode301Test() {
            HttpResponse response = await HTTPClient.Get("https://httpbin.org/status/301");
            Assert.AreEqual(System.Net.HttpStatusCode.MovedPermanently, response.statusCode);

            Debug.WriteLine("data path");
            Debug.WriteLine(AppConstants.DataFilePath);
            Debug.WriteLine("test data path");
            Debug.WriteLine(AppConstants.TestDataFilePath);
        }
        #endregion
    }
}
