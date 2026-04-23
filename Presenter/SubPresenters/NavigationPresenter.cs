using System.Diagnostics;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Presenter.SubPresenters {

    /// <summary>
    /// Coordinates navigation-related interactions between the view and the underlying handlers, 
    /// managing page loading, history navigation, and bookmark status updates.
    /// </summary>
    internal class NavigationPresenter
        (
            INavigationView navigationView,
            IBookmarkView bookmarkView,
            IPageView pageView,
            IHistoryView historyView,

            HistoryHandler historyHandler,
            BookmarkHandler bookmarkHandler,
            StateHandler stateHandler
        ) {

        private readonly INavigationView navigationView = navigationView;
        private readonly IBookmarkView bookmarkView = bookmarkView;
        private readonly IPageView pageView = pageView;
        private readonly IHistoryView historyView = historyView;

        private readonly HistoryHandler HistoryHandler = historyHandler;
        private readonly BookmarkHandler BookmarkHandler = bookmarkHandler;
        private readonly StateHandler StateHandler = stateHandler;

        #region HTML Requests
        /// <summary>
        /// Loads the web page at the specified URL and updates the view with the page's content, 
        /// status, title and favicon, if available.
        /// </summary>
        /// <remarks>This method updates the bookmark icon, progress indicator, HTML output, status code, 
        /// page title, favicon, and navigation history buttons, based on the
        /// result of loading the specified URL. The method is asynchronous and returns immediately; UI updates occur as
        /// the page load completes.</remarks>
        /// <param name="url">The URL of the web page to load. Must be a valid, non-empty string representing an absolute or relative
        /// address.</param>
        private async Task LoadPage(string url) {

            // Update bookmark icon if the new url is bookmarked or not
            bookmarkView.ToggleBookmarkButton(BookmarkHandler.IsBookmarked(url));

            // Show progress bar
            pageView.ToggleProgressIndicator(true);

            // Load the url from the url bar text input.
            HttpResponse response = await HTTPClient.Get(url);

            // Update the HTML Outputs
            pageView.UpdateHTMLOutput(response.body);
            pageView.UpdateStatusCodeOutput($"Status Code: {(int)response.statusCode} - {response.statusCode}");
            pageView.UpdateTitleOutput(response.title);

            if (response.favicon != null) pageView.UpdateFaviconOutput(response.favicon);

            // Ensure forward and back buttons are correctly enabled/disabled.
            navigationView.RefreshHistoryButtons(HistoryHandler.GetHistory().Count, HistoryHandler.GetPosition());

            // Hide Progress indicator
            pageView.ToggleProgressIndicator(false);
        }

        /// <summary>
        /// Loads the homepage URL defined in the state handler, updates the URL input field.
        /// </summary>
        /// <remarks> This method sets the URL input field to the homepage URL, 
        /// loads the homepage, and logs the visit in the history handler, updating the history drop-down accordingly.
        /// </remarks>
        public async Task LoadHomepage() {

            // Load homepage into url bar
            navigationView.SetURLInput(StateHandler.homePageUrl);

            // Load page as normal
            await LoadPage(StateHandler.homePageUrl);

            // Log the visit in history
            HistoryHandler.Register(StateHandler.homePageUrl);

            // Update the history drop-downs as history log updated.
            historyView.UpdateHistoryDropDown(HistoryHandler.GetHistory());
        }
        #endregion

        #region UI Events
        /// <summary>
        /// Handles the event triggered when the user clicks the history next button.
        /// </summary>
        /// <remarks>This method updates the URL input field to display the address of the next page in
        /// the history.</remarks>
        public async Task HistoryNext() {

            // Check possible to move in history
            var count = HistoryHandler.GetHistory().Count;
            if (count == 0 || HistoryHandler.GetPosition() >= count - 1) {
                Debug.WriteLine("Cannot navigate forward in history, already at the end.");
                return;
            }

            Debug.WriteLine($"History next requested, current position: {HistoryHandler.GetPosition()}");
            var url = this.HistoryHandler.nextPage();

            // Load next address into URL bar.
            navigationView.SetURLInput(url);

            // Load page as normal.
            await LoadPage(url);
        }

        /// <summary>
        /// Handles the event triggered when the user clicks the history previous button
        /// </summary>
        /// <remarks>This method updates the URL input field to display the address of the previous page
        /// in the history.</remarks>
        public async Task HistoryPrevious() {

            // Check possible to move in history
            if (HistoryHandler.GetHistory().Count == 0 || HistoryHandler.GetPosition() == 0) {
                Debug.WriteLine("Cannot navigate backward in history, already at the beginning.");
                return;
            }

            Debug.WriteLine($"History next requested, current position: {HistoryHandler.GetPosition()}");
            var url = this.HistoryHandler.previousPage();

            // Load previous address into URL bar.
            navigationView.SetURLInput(url);

            // Load page as normal
            await LoadPage(url);
        }

        /// <summary>
        /// Handles the event triggered when a URL is selected from the drop-down, updating the URL input, loading the
        /// corresponding page, and resetting the browsing history position to that URL.
        /// </summary>
        /// <param name="e">An event argument containing the URL requested from the user.</param>
        public async Task HistoryRequest(SelectedUrlArgs e) {

            // Load url into url bar
            this.navigationView.SetURLInput(e.url);

            // Load the page as normal
            await LoadPage(e.url);

            // Reset history pointer.
            Debug.WriteLine($"Setting position to {HistoryHandler.FindUrl(e.url)}");
            HistoryHandler.SetPosition(HistoryHandler.FindUrl(e.url));
        }

        /// <summary>
        /// Handles the event triggered when a URL is selected from the drop-down, updating the URL input, loading the
        /// corresponding page.
        /// </summary>
        /// <param name="e">An event argument containing the URL requested from the user.</param>
        public async Task BookmarkRequest(SelectedUrlArgs e) {

            Debug.WriteLine($"Bookmark request for {e.url}");

            // Load url into url bar
            this.navigationView.SetURLInput(e.url);

            // Load the page as normal
            await LoadPage(e.url);

        }

        /// <summary>
        /// Handles the event triggered when the view's URL changes, updating the URL input and loading the new page.
        /// </summary>
        public async Task NavigateFromURL() {

            string url = navigationView.GetUrlInput();
            Debug.WriteLine($"URL changed to: {url} \t Loading...");

            // Load address into URL bar
            navigationView.SetURLInput(url);

            // Register the new URL in the history handler.
            HistoryHandler.Register(url);

            // Update the history drop-downs as history log updated.
            historyView.UpdateHistoryDropDown(HistoryHandler.GetHistory());

            // Load the new URL.
            await LoadPage(url);
        }


        #endregion
    }
}
