using System.Diagnostics;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control {

    /// <summary>
    /// Coordinates interactions between the user interface and application logic, managing history, bookmarks, and
    /// state within the browser view.
    /// </summary>
    /// <remarks>The Controller subscribes to events from the view and delegates tasks to specialized
    /// handlers for history, bookmarks, and state management. This class is intended for internal use and is not
    /// thread-safe.</remarks>
    internal class Controller {

        private readonly IView view;

        private readonly BookmarkHandler bookmarkHandler;
        private readonly HistoryHandler historyHandler;
        private readonly StateHandler stateHandler;

        private readonly HTTPClient httpClient;

        /// <summary>
        /// Initializes a new instance of the Controller class and registers event handlers.
        /// </summary>
        /// <remarks>This constructor registers view events and loads the saved
        /// data (bookmarks, history and homepage). The view must be fully initialized before passing it to the
        /// controller.</remarks>
        public Controller(IView view) {

            // Get view reference
            this.view = view;

            // Constuct handlers (the model)
            bookmarkHandler = new();
            historyHandler = new();
            stateHandler = new();
            httpClient = new();

            // Subscribe events
            this.view.HistoryNextClick += View_HistoryNextClick;
            this.view.HistoryPreviousClick += View_HistoryPreviousClick;
            this.view.DropdownUrlClicked += View_DropdownUrlClicked;

            this.view.BookmarkClick += View_BookmarkClick;

            this.view.UrlChanged += View_UrlChanged;
            this.view.HistoryUpdate += View_HistoryUpdate;

            this.view.StateRequest += View_StateRequest;

            // Load state
            View_StateRequest(this, new StateArgs { request = StateArgs.Requests.load });

            // Fetch homepage
            LoadPageHomepage();

            // Load bookmarks into view
            view.UpdateBookmarks(bookmarkHandler.GetBookmarks());

        }

        #region Controller Methods
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
            view.ToggleBookmarkButton(bookmarkHandler.IsBookmarked(url));

            // Show progressbar
            view.ToggleProgressIndicator(true);

            // Load the url from the url bar text input.
            HttpResponse response = await HTTPClient.Get(url);

            // Update the HTML Outputs
            view.UpdateHTMLOutput(response.body);
            view.UpdateStatusCodeOutput($"Status Code: {(int) response.statusCode} - {response.statusCode.ToString()}");
            view.UpdateTitleOutput(response.title);

            if (response.favicon != null) view.UpdateFaviconOutput(response.favicon);

            // Ensure forward and back buttons are correctly enabled/disabled.
            view.RefreshHistoryButtons(historyHandler.GetHistory().Count, historyHandler.GetPosition());

            // Hide Progress indicator
            view.ToggleProgressIndicator(false);
        }

        /// <summary>
        /// Loads the homepage URL defined in the state handler, updates the URL input field.
        /// </summary>
        /// <remarks> This method sets the URL input field to the homepage URL, 
        /// loads the homepage, and logs the visit in the history handler, updating the history dropdown accordingly.
        /// </remarks>
        private void LoadPageHomepage() {

            // Load homepage into url bar
            view.SetURLInput(stateHandler.homePageUrl);

            // Load page as normal
            LoadPage(stateHandler.homePageUrl);

            // Log the visit in history
            historyHandler.Register(stateHandler.homePageUrl);

            // Update the history dropdowns as history log updated.
            view.UpdateHistoryDropDown(historyHandler.GetHistory());
        }
        #endregion

        #region URL and History Event Handlers
        /// <summary>
        /// Handles the event triggered when the view's URL changes, updating the URL input and loading the new page.
        /// </summary>
        /// <param name="sender">The view control whose URL has changed.</param>
        /// <param name="e">An event argument containing the new URL.</param>
        private void View_UrlChanged(object? sender, UrlEvent e) {
            Debug.WriteLine($"URL changed to: {e.url} \t Loading...");

            // Load address into URL bar.
            view.SetURLInput(e.url);

            // Load the new URL.
            LoadPage(e.url);
        }

        /// <summary>
        /// Handles the event triggered when a new URL is registered.
        /// </summary>
        /// <remarks>This method registers the given url to the history handler and re-loads the dropdown</remarks>
        /// <param name="sender">The source of the event. Usually the view</param>
        /// <param name="e">An event argument containing the registered URL.</param>
        private void View_HistoryUpdate(object? sender, UrlEvent e) {
            Debug.WriteLine($"History registerd URL: {e.url}");

            historyHandler.Register(e.url);

            view.UpdateHistoryDropDown(historyHandler.GetHistory());
        }

        /// <summary>
        /// Handles the event triggered when the user clicks the history previous button
        /// </summary>
        /// <remarks>This method updates the URL input field to display the address of the previous page
        /// in the history.</remarks>
        /// <param name="sender">The source of the event, the control that was clicked.</param>
        /// <param name="e">An <see cref="EventArgs"/>Empty</param>
        private void View_HistoryPreviousClick(object? sender, EventArgs e) {

            // Load previous address into URL bar.
            view.SetURLInput(historyHandler.previousPage());
        }

        /// <summary>
        /// Handles the event triggered when the user clicks the history next button.
        /// </summary>
        /// <remarks>This method updates the URL input field to display the address of the next page in
        /// the history.</remarks>
        /// <param name="sender">The source of the event, the control that was clicked</param>
        /// <param name="e">An <see cref="EventArgs"/>Empty</param>
        private void View_HistoryNextClick(object? sender, EventArgs e) {

            // Load next address into URL bar.
            view.SetURLInput(historyHandler.nextPage());
        }

        /// <summary>
        /// Handles the event triggered when a URL is selected from the dropdown, updating the URL input, loading the
        /// corresponding page, and resetting the browsing history position.
        /// </summary>
        /// <param name="sender">The source of the event, The dropdown control that was clicked (bookmark or history)</param>
        /// <param name="e">An event argument containing the URL to be loaded.</param>
        private void View_DropdownUrlClicked(object? sender, UrlEvent e) {
            
            // Load url into url bar
            this.view.SetURLInput(e.url);

            // Load the page as normal
            LoadPage(e.url);

            // Reset history pointer.
            Debug.WriteLine($"Setting position to {historyHandler.FindUrl(e.url)}");
            historyHandler.SetPosition(historyHandler.FindUrl(e.url));
        }

        #endregion

        #region Bookmark Event Handlers
        /// <summary>
        /// Handles the bookmark click event by toggling the bookmark status of the specified URL 
        /// and updating the icon and bookmark list in the view accordingly.
        /// </summary>
        /// <remarks>This method updates both the bookmark list and the bookmark button state in the view
        /// to reflect the current bookmark status of the URL.</remarks>
        /// <param name="sender">The source of the event, the bookmark button.</param>
        /// <param name="e">An event argument containing the URL to be bookmarked or unbookmarked.</param>
        private void View_BookmarkClick(object? sender, UrlEvent e) {

            // Toggle bookmark status for the given URL.
            if (bookmarkHandler.IsBookmarked(e.url)) {

                // If its bookmarked, remove it.
                bookmarkHandler.RemoveBookmark(e.url);
            } else {

                // If its not bookmarked, add it.
                bookmarkHandler.AddBookmark(e.url);
            }

            // Update bookmark list on view.
            view.UpdateBookmarks(bookmarkHandler.GetBookmarks());

            // Update button on view to reflect new status.
            view.ToggleBookmarkButton(bookmarkHandler.IsBookmarked(e.url));
        }
        #endregion

        #region State Requests.

        /// <summary>
        /// Handles state-related requests triggered by the view, such as loading or saving application state, updating
        /// the homepage, and responding to homepage load events.
        /// </summary>
        /// <remarks>This method processes requests defined in <see cref="StateArgs.Requests"/></remarks>
        /// <param name="sender">The source of the event, typically the view component initiating the state request.</param>
        /// <param name="e">A <see cref="StateArgs"/> instance containing details about the specific state request and any associated
        /// data.</param>
        /// <exception cref="NotImplementedException">Thrown when the request is not specified in <paramref name="e"/></exception>
        private void View_StateRequest(object? sender, StateArgs e) {

            Debug.WriteLine($"State request id: {e.request}");

            switch (e.request) {

                case StateArgs.Requests.homePageLoad:
                    Debug.WriteLine("Homepage Loaded");
                    view.SetURLInput(stateHandler.homePageUrl);
                    break;

                case StateArgs.Requests.homePageSet:
                    Debug.WriteLine($"Homepage set to: {e.homepage}");
                    stateHandler.homePageUrl = e.homepage;
                    break;

                case StateArgs.Requests.save:
                    Debug.WriteLine("Saving All");
                    stateHandler.SaveState();
                    historyHandler.SaveHistory();
                    bookmarkHandler.SaveBookmarks();
                    Debug.WriteLine("State saved");
                    break;

                case StateArgs.Requests.load:
                    Debug.WriteLine("Loading state");
                    stateHandler.LoadState();
                    Debug.WriteLine("Loaded state");
                    historyHandler.LoadHistory();
                    Debug.WriteLine("Loaded history");
                    bookmarkHandler.LoadBookmarks();
                    Debug.WriteLine("Loaded Bookmarks");
                    break;

                default:
                    throw new NotImplementedException($"id:{e.request} is not a recognised request");
            }
        }
        #endregion
    }
}
