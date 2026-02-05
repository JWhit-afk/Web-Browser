
using System.Diagnostics;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control.Coordinators {

    internal class NavigationCoordinator {

        IView view;
        HistoryHandler HistoryHandler;
        BookmarkHandler BookmarkHandler;
        StateHandler StateHandler;

        public NavigationCoordinator(
            IView view,
            HistoryHandler historyHandler,
            BookmarkHandler bookmarkHandler,
            StateHandler stateHandler
            ) {

            // Get view refereance.
            this.view = view;

            // Assign references to handlers for use in the coordinator.
            this.HistoryHandler = historyHandler;
            this.BookmarkHandler = bookmarkHandler;
            this.StateHandler = stateHandler;
        }

        #region HTML Requests
        /// <summary>
        /// Loads the web page at the specified URL and updates the view with the page's content, 
        /// status, title and favicon, if avaliable.
        /// </summary>
        /// <remarks>This method updates the the bookmark icon, progress indicator, HTML output, status code, 
        /// page title, favicon, and navigation history buttons, based on the
        /// result of loading the specified URL. The method is asynchronous and returns immediately; UI updates occur as
        /// the page load completes.</remarks>
        /// <param name="url">The URL of the web page to load. Must be a valid, non-empty string representing an absolute or relative
        /// address.</param>
        private async void LoadPage(string url) {

            // Update bookmark icon if the new url is bookmarked or not
            view.ToggleBookmarkButton(BookmarkHandler.IsBookmarked(url));

            // Show progressbar
            view.ToggleProgressIndicator(true);

            // Load the url from the url bar text input.
            HttpResponse response = await HTTPClient.Get(url);

            // Update the HTML Outputs
            view.UpdateHTMLOutput(response.body);
            view.UpdateStatusCodeOutput($"Status Code: {(int)response.statusCode} - {response.statusCode.ToString()}");
            view.UpdateTitleOutput(response.title);

            if (response.favicon != null) view.UpdateFaviconOutput(response.favicon);

            // Ensure forward and back buttons are correctly enabled/disabled.
            view.RefreshHistoryButtons(HistoryHandler.GetHistory().Count, HistoryHandler.GetPosition());

            // Hide Progress indicator
            view.ToggleProgressIndicator(false);
        }

        /// <summary>
        /// Loads the homepage URL defined in the state handler, updates the URL input field.
        /// </summary>
        /// <remarks> This method sets the URL input field to the homepage URL, 
        /// loads the homepage, and logs the visit in the history handler, updating the history dropdown accordingly.
        /// </remarks>
        public void LoadHomepage() {

            // Load homepage into url bar
            view.SetURLInput(StateHandler.homePageUrl);

            // Load page as normal
            LoadPage(StateHandler.homePageUrl);

            // Log the visit in history
            HistoryHandler.Register(StateHandler.homePageUrl);

            // Ensure buttons are correctly enabled/disabled based on the updated history.
            view.RefreshHistoryButtons(HistoryHandler.GetHistory().Count, HistoryHandler.GetPosition());

            // Update the history dropdowns as history log updated.
            view.UpdateHistoryDropDown(HistoryHandler.GetHistory());
        }
        #endregion

        #region UI Events
        /// <summary>
        /// Handles the event triggered when the user clicks the history next button.
        /// </summary>
        /// <remarks>This method updates the URL input field to display the address of the next page in
        /// the history.</remarks>
        public void HistoryNext() {

            Debug.WriteLine($"History next requested, current position: {HistoryHandler.GetPosition()}");
            var url = this.HistoryHandler.nextPage();

            // Load next address into URL bar.
            view.SetURLInput(url);

            // Load page as normal.
            LoadPage(url);
        }

        /// <summary>
        /// Handles the event triggered when the user clicks the history previous button
        /// </summary>
        /// <remarks>This method updates the URL input field to display the address of the previous page
        /// in the history.</remarks>
        public void HistoryPrevious() {

            Debug.WriteLine($"History next requested, current position: {HistoryHandler.GetPosition()}");
            var url = this.HistoryHandler.previousPage();

            // Load previous address into URL bar.
            view.SetURLInput(url);

            // Load page as normal
            LoadPage(url);
        }

        /// <summary>
        /// Handles the event triggered when a URL is selected from the dropdown, updating the URL input, loading the
        /// corresponding page, and resetting the browsing history position to that URL.
        /// </summary>
        /// <param name="e">An event argument containing the URL requested from the user.</param>
        public void HistoryRequest(SelectedUrlArgs e) {

            // Load url into url bar
            this.view.SetURLInput(e.url);

            // Load the page as normal
            LoadPage(e.url);

            // Reset history pointer.
            Debug.WriteLine($"Setting position to {HistoryHandler.FindUrl(e.url)}");
            HistoryHandler.SetPosition(HistoryHandler.FindUrl(e.url));
        }

        /// <summary>
        /// Handles the event triggered when a URL is selected from the dropdown, updating the URL input, loading the
        /// corresponding page.
        /// </summary>
        /// <param name="e">An event argument containing the URL requested from the user.</param>
        public void BookmarkRequest(SelectedUrlArgs e) {

            Debug.WriteLine($"Bookmark request for {e.url}");

            // Load url into url bar
            this.view.SetURLInput(e.url);

            // Load the page as normal
            LoadPage(e.url);

        }

        /// <summary>
        /// Handles the event triggered when the view's URL changes, updating the URL input and loading the new page.
        /// </summary>
        public void NavigateFromURL() {

            string url = view.GetUrlInput();
            Debug.WriteLine($"URL changed to: {url} \t Loading...");

            // Load address into URL bar
            view.SetURLInput(url);

            // Register the new URL in the history handler.
            HistoryHandler.Register(url);

            // Update the history dropdowns as history log updated.
            view.UpdateHistoryDropDown(HistoryHandler.GetHistory());

            // Load the new URL.
            LoadPage(url);
        }


        #endregion
    }
}
