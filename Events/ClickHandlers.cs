
namespace Web_Browser_CW1 {
    
    public partial class WebBrowser {

        //
        // Button Handlers
        //
        private async void HomeClick(object sender, EventArgs e) {

            // Request home page url.
            HttpResponse response = await httpClient.Get(stateHandler.homePageUrl);
            UpdateHTMLOutputs(response.body, response.statusCode.ToString(), response.title, response.favicon);

            // Log visit in history.
            historyHandler.visit(stateHandler.homePageUrl);

            // Update GUI elements.
            UpdateHistroyDropDown();
            UpdateHistoryButtons();
        }

        private async void PrevClick(object sender, EventArgs e) {

            // Request previous page url.
            HttpResponse response = await httpClient.Get(historyHandler.previousPage());
            UpdateHTMLOutputs(response.body, response.statusCode.ToString(), response.title, response.favicon);

            // Update GUI elements.
            UpdateHistoryButtons();
        }

        private async void NextClick(object sender, EventArgs e) {

            // Request next page url.
            HttpResponse response = await httpClient.Get(historyHandler.nextPage());
            UpdateHTMLOutputs(response.body, response.statusCode.ToString(), response.title, response.favicon);

            // Update GUI elements.
            UpdateHistoryButtons();
        }

        private async void SearchClick(object sender, EventArgs e) {

            // Request page url.
            HttpResponse response = await httpClient.Get(urlBar.Text);
            UpdateHTMLOutputs(response.body, response.statusCode.ToString(), response.title, response.favicon);

            // Log visit in history.
            historyHandler.visit(urlBar.Text);

            // Update GUI elements.
            UpdateHistroyDropDown();
            UpdateHistoryButtons();
        }
    }
}
