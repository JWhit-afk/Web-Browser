using Web_Browser_CW1.Handlers;

namespace Web_Browser_CW1
{
    public partial class WebBrowser : Form {

        BookmarkHandler bookmarkHandler;
        HistoryHandler historyHandler;
        StateHandler stateHandler;

        HTTPClient httpClient;

        public WebBrowser() {

            InitializeComponent();

            // Initialise handlers.
            bookmarkHandler = new BookmarkHandler();
            httpClient = new HTTPClient(progressBar);
            historyHandler = new HistoryHandler();
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
