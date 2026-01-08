using System;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control {

    internal class Controller {

        private readonly IView view;

        private readonly BookmarkHandler bookmarkHandler;
        private readonly HistoryHandler historyHandler;
        private readonly StateHandler stateHandler;

        private readonly HTTPClient httpClient;

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
            this.view.HistoryUpdate += View_HistoryUpdate;
            this.view.HistoryItemClicked += View_HistoryItemClicked;
            this.view.UrlChanged += View_UrlChanged;
            this.view.StateRequest += View_StateRequest;

            // Load from save
            stateHandler.LoadState();
            historyHandler.LoadHistory();

            // Load homepage
            View_LoadPageHomepage(this, EventArgs.Empty);
        }

        // Helper method to load a page from a given URL.
        // Only interacts with html outputs and progress indicator.
        private async void LoadPage(string url) {

            // Show progressbar
            view.ToggleProgressIndicator(true);

            // Load the url from the url bar text input.
            HttpResponse response = await httpClient.Get(url);

            // Update the HTML Outputs
            view.UpdateHTMLOutput(response.body);
            view.UpdateStatusCodeOutput(response.statusCode.ToString());
            view.UpdateTitleOutput(response.title);

            if (response.favicon != null) view.UpdateFaviconOutput(response.favicon);

            // Ensure forward and back buttons are correctly enabled/disabled.
            view.RefreshHistoryButtons(historyHandler.GetHistory().Count, historyHandler.GetPosition());

            // Hide Progress indicator
            view.ToggleProgressIndicator(false);
        }

        #region URL and History Event Handlers
        private void View_UrlChanged(object? sender, UrlEvent e) {
            Debug.WriteLine($"URL changed to: {e.url} \t Loading...");

            // Load address into URL bar.
            view.SetURLInput(e.url);

            // Load the new URL.
            LoadPage(e.url);
        }

        private void View_HistoryUpdate(object? sender, UrlEvent e) {
            Debug.WriteLine($"History registerd URL: {e.url}");

            historyHandler.register(e.url);

            view.UpdateHistoryDropDown(historyHandler.GetHistory());
        }

        private void View_HistoryPreviousClick(object? sender, EventArgs e) {

            // Load previous address into URL bar.
            view.SetURLInput(historyHandler.previousPage());
        }

        private void View_HistoryNextClick(object? sender, EventArgs e) {

            // Load next address into URL bar.
            view.SetURLInput(historyHandler.nextPage());
        }

        private async void View_LoadPageHomepage(object? sender, EventArgs e) {

            // Load homepage into url bar
            view.SetURLInput(stateHandler.homePageUrl);

            // Load page as normal
            LoadPage(stateHandler.homePageUrl);

            // Log the visit in history
            historyHandler.register(stateHandler.homePageUrl);

            // Update the history dropdowns as history log updated.
            view.UpdateHistoryDropDown(historyHandler.GetHistory());
        }

        private void View_HistoryItemClicked(object? sender, UrlEvent e) {
            throw new NotImplementedException();
        }

        #endregion

        #region State Requests.

        private void View_StateRequest(object? sender, StateArgs e) {

            Debug.WriteLine($"State request id:{e.request}");

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
                    Debug.WriteLine("Saving state");
                    stateHandler.SaveState();
                    historyHandler.SaveHistory();
                    break;

                case StateArgs.Requests.load:
                    Debug.WriteLine("Loading state");
                    stateHandler.LoadState();
                    historyHandler.LoadHistory();
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}
