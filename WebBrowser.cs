using System.Net.Http;
using Web_Browser_CW1.Handlers;

namespace Web_Browser_CW1
{
    public partial class WebBrowser : Form {

        HTTPClient httpClient;
        HistoryHandler historyHandler;

        public WebBrowser() {

            InitializeComponent();

            httpClient = new HTTPClient(progressBar);

            historyHandler = new HistoryHandler(
                historyToolStripMenuItem,
                previousPage,
                nextPage
            );

            progressBar.Hide();

        }

        private void RenderHtmlCode(HttpResponse response) {

            // display html + status code
            this.Text = response.title;

            if (response.favicon != null) {
                this.Icon = response.favicon;
            } else {
                this.Icon = null;
            }

            htmlDisplay.Text = response.body;
            HtmlResponseCodeOutput.Text = "Response Code: " + ((int)response.statusCode) + " - " + response.statusCode.ToString();
        }

        private void WebBrowserWindow_Load(object sender, EventArgs e) {

        }

    }
}
