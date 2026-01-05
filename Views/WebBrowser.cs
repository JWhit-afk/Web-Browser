using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1
{
    public partial class WebBrowser : Form, IView {

        public WebBrowser() {

            InitializeComponent();

        }

        private void WebBrowser_FormClosing(object sender, FormClosingEventArgs e) {

            // Save state and history for next session.

            this.SaveState.Invoke(this, EventArgs.Empty);
        }

        #region IView Implementation

        public event EventHandler HistoryPrevious;
        public event EventHandler HistoryNext;
        public event EventHandler HomepageUpdate;
        public event EventHandler LoadPageURLBar;
        public event EventHandler LoadHomepage;
        public event EventHandler SaveState;

        public void UpdateHistoryDropDown(List<string> items) {

            // Clear existing items
            historyToolStripMenuItem.DropDownItems.Clear();

            // Build from newest to oldest
            for (int i = 0; i < items.Count; i++) {
                var item = items[items.Count - 1 - i];
                var urlItem = new ToolStripMenuItem(item);

                urlItem.Click += async (sender, e) => {

                    // TODO: Load the selected URL
                    this.urlBar.Text = item;
                    this.LoadPageURLBar?.Invoke(this, e);
                };

                historyToolStripMenuItem.DropDownItems.Add(urlItem);
            }
        }

        public void UpdateHTMLOutput(string htmlContent) {
            this.htmlDisplay.Text = htmlContent;
        }

        public void UpdateStatusCodeOutput(string statusCode) {
            this.HtmlResponseCodeOutput.Text = statusCode;
        }

        public void UpdateTitleOutput(string title) {
            this.Text = title;
        }

        public void UpdateFaviconOutput(Icon favicon) {
            this.Icon = favicon;
        }

        public void TogglePreviousButton(bool enabled) {
            this.previousPage.Enabled = enabled;
            this.previousPage.Visible = enabled;
        }

        public void ToggleNextButton(bool enabled) {
            this.nextPage.Enabled = enabled;
            this.nextPage.Visible = enabled;
        }

        public void RefreshHistoryButtons(int historyCount, int position) {

            if (historyCount > 0 && position < historyCount - 1) {
                ToggleNextButton(true);
            } else {
                ToggleNextButton(false);
            }

            if (historyCount > 0 && position > 0) {
                TogglePreviousButton(true);
            } else {
                TogglePreviousButton(false);
            }

        }

        public string GetURLInput() {
            return this.urlBar.Text;
        }

        public void SetURLInput(string url) {
            this.urlBar.Text = url;
        }

        public void ToggleProgressIndicator(bool visible) {
            this.progressBar.Visible = visible;
        }

        #endregion

        #region Events

        //
        // Click Events
        //
        private void ButtonHome_Click(object sender, EventArgs e) {

            // Call the page load event to fetch  home url and refresh GUI
            this.LoadHomepage?.Invoke(this, e);
        }

        private void ButtonPrev_Click(object sender, EventArgs e) {

            // Call the history previous event to navigate back.
            this.HistoryPrevious?.Invoke(this, e);
        }

        private void ButtonNext_Click(object sender, EventArgs e) {

            // Call the history next event to navigate forward.
            this.HistoryNext?.Invoke(this, e);
        }

        private void ButtonSearch_Click(object sender, EventArgs e) {

            // Call the page load event to fetch url and refresh GUI.
            this.LoadPageURLBar?.Invoke(this, e);
        }

        //
        // KeyDown Events
        //
        private void NewHomePageEnter(object sender, KeyEventArgs e) {

            this.HomepageUpdate?.Invoke(this, e);
        }

        #endregion

    }
}
