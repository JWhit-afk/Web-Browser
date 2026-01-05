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

            // Assign events
            this.view.LoadPageURLBar += View_LoadPageURLBar;

            this.view.HistoryNext += View_HistoryNext;
            this.view.HistoryPrevious += View_HistoryPrevious;
            this.view.HomepageUpdate += View_HomepageUpdate;
            this.view.SaveState += View_SaveState;

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
            this.view.ToggleProgressIndicator(true);

            // Load the url from the url bar text input.
            HttpResponse response = await httpClient.Get(url);

            // Update the HTML Outputs
            this.view.UpdateHTMLOutput(response.body);
            this.view.UpdateStatusCodeOutput(response.statusCode.ToString());
            this.view.UpdateTitleOutput(response.title);

            if (response.favicon != null) this.view.UpdateFaviconOutput(response.favicon);

            // Ensure forward and back buttons are correctly enabled/disabled.
            this.view.RefreshHistoryButtons(historyHandler.GetHistory().Count, historyHandler.GetPosition());

            // Hide Progress indicator
            this.view.ToggleProgressIndicator(false);
        }

        #region URL and History Event Handlers

        private void View_HomepageUpdate(object? sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void View_HistoryPrevious(object? sender, EventArgs e) {

            // Load previous address into URL bar.
            this.view.SetURLInput(historyHandler.previousPage());

            // Perfrom page load as normal.
            LoadPage(this.view.GetURLInput());
        }

        private void View_HistoryNext(object? sender, EventArgs e) {

            // Load next address into URL bar.
            this.view.SetURLInput(historyHandler.nextPage());

            // Perfrom page load as normal.
            LoadPage(this.view.GetURLInput());
        }

        private async void View_LoadPageURLBar(object? sender, EventArgs e) {

            // Load page from URL in url bar
            LoadPage(this.view.GetURLInput());

            // Log the visit in history
            historyHandler.visit(this.view.GetURLInput());

            // Update the history dropdowns as history log updated.
            this.view.UpdateHistoryDropDown(historyHandler.GetHistory());
        }

        private async void View_LoadPageHomepage(object? sender, EventArgs e) {

            // Load homepage into url bar
            this.view.SetURLInput(stateHandler.homePageUrl);

            // Load page as normal
            LoadPage(stateHandler.homePageUrl);

            // Log the visit in history
            historyHandler.visit(stateHandler.homePageUrl);

            // Update the history dropdowns as history log updated.
            this.view.UpdateHistoryDropDown(historyHandler.GetHistory());
        }

        #endregion

        #region State saving and loading events

        private void View_SaveState(object? sender, EventArgs e) {
            stateHandler.SaveState();
            historyHandler.SaveHistory();
        }

        #endregion
    }
}
