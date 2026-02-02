using System.Diagnostics;
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
            this.StateRequest.Invoke(this, new StateArgs { request = StateArgs.Requests.save });
        }

        #region IView Implementation

        public event EventHandler HistoryPreviousClick;
        public event EventHandler HistoryNextClick;
        
        public event EventHandler<UrlEvent> HistoryItemClicked;
        public event EventHandler<UrlEvent> UrlChanged;
        public event EventHandler<UrlEvent> HistoryUpdate;
        public event EventHandler<StateArgs> StateRequest;

        public event EventHandler<UrlEvent> BookmarkClick;
        public event EventHandler BookmarkItemClicked;

        public void UpdateHistoryDropDown(List<string> items) {

            // Clear existing items
            historyToolStripMenuItem.DropDownItems.Clear();

            // Build from newest to oldest
            for (int i = 0; i < items.Count; i++) {

                // Generate object.
                var item = items[items.Count - 1 - i];
                var urlItem = new ToolStripMenuItem(item);

                // Asign onclick event
                urlItem.Click += new EventHandler(Dropdown_Click); ;

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

        public void UpdateBookmarks(List<string> items) {

            // Clear existing items
            favouritesToolStripMenuItem.DropDownItems.Clear();

            // Build from newest to oldest
            for (int i = 0; i < items.Count; i++) {

                // Generate object.
                var urlItem = new ToolStripMenuItem(items[i]);

                // Asign onclick event
                urlItem.Click += new EventHandler(Dropdown_Click);

                favouritesToolStripMenuItem.DropDownItems.Add(urlItem);
            }
        }

        public void ToggleBookmarkButton(bool isBookmarked) {
            this.favourite.BackgroundImage = isBookmarked ? Properties.Resources.star_full : Properties.Resources.star_empty;
        }

        #endregion

        #region Events

        //
        // Click Events
        //
        private void ButtonHome_Click(object sender, EventArgs e) {

            // Call the state request event to fetch home and load into url bar.
            this.StateRequest?.Invoke(this, new StateArgs { request = StateArgs.Requests.homePageLoad });

            // Record visit to history.
            this.HistoryUpdate?.Invoke(this, new UrlEvent { url = this.urlBar.Text });

            // Notify subscribers URL changed to load page.
            this.UrlChanged?.Invoke(this, new UrlEvent { url = this.urlBar.Text });
        }

        private void ButtonPrev_Click(object sender, EventArgs e) {

            // Call the history previous event to navigate back.
            this.HistoryPreviousClick?.Invoke(this, e);

            // Notify URL changed to load page.
            this.UrlChanged?.Invoke(this, new UrlEvent { url = this.urlBar.Text });
        }

        private void ButtonNext_Click(object sender, EventArgs e) {

            // Call the history next event to navigate forward.
            this.HistoryNextClick?.Invoke(this, e);

            // Notify URL changed to load page.
            this.UrlChanged?.Invoke(this, new UrlEvent { url = this.urlBar.Text });
        }

        private void ButtonSearch_Click(object sender, EventArgs e) {

            // Update history with new url.
            this.HistoryUpdate?.Invoke(this, new UrlEvent { url = this.urlBar.Text });

            // Notify URL changed to load page.
            this.UrlChanged?.Invoke(this, new UrlEvent { url = this.urlBar.Text });
        }

        private void Dropdown_Click(object sender, EventArgs e) {

            Debug.WriteLine($"Sender: {sender.GetType()}");

            if (sender is ToolStripMenuItem) {

                ToolStripMenuItem item = (ToolStripMenuItem) sender;

                this.HistoryItemClicked?.Invoke(sender, new UrlEvent { url = item.Text });
            }
        }

        private void Bookmark_Click(object sender, EventArgs e) {
            this.BookmarkClick?.Invoke(sender, new UrlEvent { url =  this.urlBar.Text });
        }


        //
        // KeyDown Events
        //
        private void NewHomePageEnter(object sender, KeyEventArgs e) {

            // ignore non-enter keys.
            if (e.KeyCode != Keys.Enter) return;

            // Request homepage change on state handler and pass homepage as arg.
            this.StateRequest?.Invoke(this, 
                new StateArgs {
                    request = StateArgs.Requests.homePageSet, 
                    homepage = this.newHomepageText.Text
                }
            );


        }

        #endregion

    }
}
