using System.Net.Http;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Managers;

namespace Web_Browser_CW1
{
    public partial class WebBrowser : Form {

        BookmarkHandler bookmarkHandler;
        HistoryHandler historyHandler;
        StateHandler stateHandler;
        RequestHandler requestHandler;

        public WebBrowser() {

            InitializeComponent();

            bookmarkHandler = new BookmarkHandler();
            requestHandler = new RequestHandler(
               this,
               urlBar,
               htmlDisplay,
               HtmlResponseCodeOutput,
               progressBar
           );
            historyHandler = new HistoryHandler(
                historyToolStripMenuItem,
                previousPage,
                nextPage,
                requestHandler
            );
            stateHandler = new StateHandler();

            // Visit the home page on startup.
            HomeClick(this, EventArgs.Empty);

        }

        private void WebBrowserWindow_Load(object sender, EventArgs e) {

        }
    }
}
