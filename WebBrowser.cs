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

            // Initialise handlers.
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

            // Load saved state and history from previous sessions.
            stateHandler.LoadState();
            historyHandler.LoadHistory();

            // Visit the home page on startup.
            HomeClick(this, EventArgs.Empty);

        }

        private void WebBrowser_FormClosing(object sender, FormClosingEventArgs e) {

            // Save state and history for next session.
            stateHandler.SaveState();
            historyHandler.SaveHistory();
        }
    }
}
