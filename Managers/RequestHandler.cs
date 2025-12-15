using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web_Browser_CW1.Handlers;

namespace Web_Browser_CW1.Managers {

    internal class RequestHandler {

        HTTPClient httpClient;

        WebBrowser browser;
        TextBox urlBar;
        TextBox htmlDisplay;
        ToolStripStatusLabel HtmlResponseCodeOutput;

        public RequestHandler(
            WebBrowser browser,
            TextBox urlBar,
            TextBox htmlDisplay,
            ToolStripStatusLabel HtmlResponseCodeOutput,
            ProgressBar progressBar
        ) 
        {
            this.browser = browser;
            this.urlBar = urlBar;
            this.htmlDisplay = htmlDisplay;
            this.HtmlResponseCodeOutput = HtmlResponseCodeOutput;
            httpClient = new HTTPClient(progressBar);
        }

        public async void LoadPage(String url) {
            urlBar.Text = url;
            RenderHtmlCode(await httpClient.Get(urlBar.Text));
        }

        private void RenderHtmlCode(HttpResponse response) {

            // display html + status code
            browser.Text = response.title;

            if (response.favicon != null) {
                browser.Icon = response.favicon;
            } else {
                browser.Icon = null;
            }

            htmlDisplay.Text = response.body;
            HtmlResponseCodeOutput.Text = "Response Code: " + ((int)response.statusCode) + " - " + response.statusCode.ToString();
        }
    }
}
