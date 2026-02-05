using System.Diagnostics;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1
{
    public partial class WebBrowser : Form, IView {

        public WebBrowser() {

            InitializeComponent();

        }

        private void WebBrowser_FormClosing(object sender, FormClosingEventArgs e) {

            // Save state and history for next session.
            this.StateRequest?.Invoke(this, new StateArgs { request = StateArgs.Requests.save });
        }

        #region IView Implementation (GUI Updates)

        public event EventHandler? HistoryPreviousClick;
        public event EventHandler? HistoryNextClick;
        public event EventHandler? HistoryRegister;

        public event EventHandler<SelectedUrlArgs>? HistoryDropDownClick;
        public event EventHandler<SelectedUrlArgs>? BookmarkDropDownClick;

        public event EventHandler? UrlChanged;
        public event EventHandler<StateArgs>? StateRequest;

        public event EventHandler? BookmarkClick;


        /// <summary>
        /// Gets the string located in <see cref="urlBar"/>
        /// </summary>
        /// <returns>A possibly empty string</returns>

        public string GetUrlInput() {
            return this.urlBar.Text;
        }

        /// <summary>
        /// Updates the history dropdown with <paramref name="items"/>
        /// </summary>
        /// <remarks>The new dropdown items, onclick will call <see cref="HistoryDropDown_Click"/></remarks>
        /// <param name="items">The URLs to display</param>
        public void UpdateHistoryDropDown(List<string> items) {

            // Clear existing items
            historyToolStripMenuItem.DropDownItems.Clear();

            // Build from newest to oldest
            for (int i = 0; i < items.Count; i++) {

                // Generate object.
                var item = items[items.Count - 1 - i];
                var urlItem = new ToolStripMenuItem(item);

                // Asign onclick event
                urlItem.Click += new EventHandler(HistoryDropDown_Click);

                historyToolStripMenuItem.DropDownItems.Add(urlItem);
            }
        }

        /// <summary>
        /// Updates the displayed HTML content in the HTML display control.
        /// </summary>
        /// <param name="htmlContent">The HTML to display.</param>
        public void UpdateHTMLOutput(string htmlContent) {
            this.htmlDisplay.Text = htmlContent;
        }

        /// <summary>
        /// Updates the displayed HTTP status code in the output strip.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to display.</param>
        public void UpdateStatusCodeOutput(string statusCode) {
            this.HtmlResponseCodeOutput.Text = statusCode;
        }

        /// <summary>
        /// Sets the title to the specified text.
        /// </summary>
        /// <param name="title">The title to display.</param>
        public void UpdateTitleOutput(string title) {
            this.Text = title;
        }

        /// <summary>
        /// Updates the displayed favicon image with the specified bitmap.
        /// </summary>
        /// <param name="favicon">The bitmap image to use as the new favicon.</param>
        public void UpdateFaviconOutput(Bitmap favicon) {
            this.favicon.Image = favicon;
        }

        /// <summary>
        /// Shows or hides the previous page button.
        /// </summary>
        /// <param name="enabled">Boolean indicating whether the previous page button should be visible and enabled. Specify <see
        /// langword="true"/> to show and enable the button; otherwise, <see langword="false"/> to hide and disable it.</param>
        public void TogglePreviousButton(bool enabled) {
            this.previousPage.Enabled = enabled;
            this.previousPage.Visible = enabled;
        }

        /// <summary>
        /// Shows or hides the next page button.
        /// </summary>
        /// <param name="enabled">Boolean indicating whether the next page button should be visible and enabled. Specify <see
        /// langword="true"/> to show and enable the button; otherwise, <see langword="false"/> to hide and disable it.</param>
        public void ToggleNextButton(bool enabled) {
            this.nextPage.Enabled = enabled;
            this.nextPage.Visible = enabled;
        }

        /// <summary>
        /// Toggles the next page previous page buttons if they can function
        /// </summary>
        /// <remarks>If the history pointer is pointing to the most recent URL the next page button
        /// will be disabled; otherwise, enabled. <br/>
        /// If the history pointer is pointing to the oldest URL the previous page button will be
        /// disabled; otherwise, enabled.</remarks>
        /// <param name="historyCount">The number of URLs in the collection.</param>
        /// <param name="position">The current URL being pointed to by the handler.</param>
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

        /// <summary>
        /// Sets the URL bar to a specified URL.
        /// </summary>
        /// <remarks>URL changes will trigger a <see cref="UrlChanged"/> event</remarks>
        /// <param name="url">The URL to update the control with.</param>
        public void SetURLInput(string url) {
            this.urlBar.Text = url;
        }

        /// <summary>
        /// Toggles the visibility of the progress bar indicator control.
        /// </summary>
        /// <param name="visible">Pass <see langword="true"/> to enable; 
        /// otherwise, <see langword="false"/> to disable </param>
        public void ToggleProgressIndicator(bool visible) {
            this.progressBar.Visible = visible;
        }

        /// <summary>
        /// Updates the bookmark dropdown with <paramref name="items"/>
        /// </summary>
        /// <remarks>The new dropdown items, onclick will call <see cref="BookmarkDropDown_Click"/></remarks>
        /// <param name="items">The URLs to display</param>
        public void UpdateBookmarks(List<string> items) {

            // Clear existing items
            favouritesToolStripMenuItem.DropDownItems.Clear();

            // Build from newest to oldest
            for (int i = 0; i < items.Count; i++) {

                // Generate object.
                var urlItem = new ToolStripMenuItem(items[i]);

                // Asign onclick event
                urlItem.Click += new EventHandler(BookmarkDropDown_Click);

                favouritesToolStripMenuItem.DropDownItems.Add(urlItem);
            }
        }

        /// <summary>
        /// Toggles bettween the control images that indicate if a URL is bookmarked or not
        /// </summary>
        /// <param name="isBookmarked">Pass <see langword="true"/> for the bookmarked image; 
        /// otherwise, <see langword="false"/> for the non-bookmarked image.</param>
        public void ToggleBookmarkButton(bool isBookmarked) {
            this.favourite.BackgroundImage = isBookmarked ? Properties.Resources.star_full : Properties.Resources.star_empty;
        }

        #endregion

        #region GUI Events

        #region Click Events
        /// <summary>
        /// Event handler for the home button click event.
        /// </summary>
        /// <remarks>Raises relevent events for the <see cref="Web_Browser_CW1.Control.Controller"/> to handle</remarks>
        /// <param name="sender">The button control.</param>
        /// <param name="e">Default event args</param>
        private void ButtonHome_Click(object sender, EventArgs e) {

            // Call the state request event to fetch home and load into url bar.
            this.StateRequest?.Invoke(sender, new StateArgs { request = StateArgs.Requests.homePageLoad });

            // Record visit to history.
            this.HistoryRegister?.Invoke(sender, e);

            // Notify subscribers URL changed to load page.
            this.UrlChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Event handler for the previous page button click event.
        /// </summary>
        /// <remarks>Raises relevent events for the <see cref="Web_Browser_CW1.Control.Controller"/> to handle</remarks>
        /// <param name="sender">The button control.</param>
        /// <param name="e">Default event args</param>
        private void ButtonPrev_Click(object sender, EventArgs e) {

            // Call the history previous event to navigate back.
            this.HistoryPreviousClick?.Invoke(sender, e);

            // Notify URL changed to load page.
            this.UrlChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Event handler for the next page button click event.
        /// </summary>
        /// <remarks>Raises relevent events for the <see cref="Web_Browser_CW1.Control.Controller"/> to handle</remarks>
        /// <param name="sender">The button control.</param>
        /// <param name="e">Default event args.</param>
        private void ButtonNext_Click(object sender, EventArgs e) {

            // Call the history next event to navigate forward.
            this.HistoryNextClick?.Invoke(sender, e);

            // Notify URL changed to load page.
            this.UrlChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Event handler for the search button click event.
        /// </summary>
        /// <remarks>Raises relevent events for the <see cref="Web_Browser_CW1.Control.Controller"/> to handle</remarks>
        /// <param name="sender">The button control.</param>
        /// <param name="e">Default event args.</param>
        private void ButtonSearch_Click(object sender, EventArgs e) {

            // Update history with new url.
            this.HistoryRegister?.Invoke(sender, e);

            // Notify URL changed to load page.
            this.UrlChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Event handler for URL dropdown item click event.
        /// </summary>
        /// <remarks>Raises relevent events for the <see cref="Web_Browser_CW1.Control.Controller"/> to handle</remarks>
        /// <param name="sender">The dropdown control.</param>
        /// <param name="e">Default event args.</param>
        private void HistoryDropDown_Click(object? sender, EventArgs e) {

            if (sender != null )
                Debug.WriteLine($"Sender: {sender.GetType()}");

            if (sender is ToolStripMenuItem { Text: not null } item) {

                this.HistoryDropDownClick?.Invoke(sender, new SelectedUrlArgs { url = item.Text });
            }
        }

        private void BookmarkDropDown_Click(object? sender, EventArgs e) {

            if (sender != null)
                Debug.WriteLine($"Sender: {sender.GetType()}");

            if (sender is ToolStripMenuItem { Text: not null } item) {
                this.BookmarkDropDownClick?.Invoke(sender, new SelectedUrlArgs { url = item.Text });
            }
        }

        /// <summary>
        /// Event handler for the bookmark button click event.
        /// </summary>
        /// <remarks>Raises relevent events for the <see cref="Web_Browser_CW1.Control.Controller"/> to handle</remarks>
        /// <param name="sender">The button control.</param>
        /// <param name="e">Default event args.</param>
        private void Bookmark_Click(object sender, EventArgs e) {
            this.BookmarkClick?.Invoke(sender, e);
        }
        #endregion

        #region KeyDown Events
        /// <summary>
        /// Event handler for new homepage keydowns.
        /// </summary>
        /// <remarks>Only actioned when <see cref="Keys.Enter"/> is pressed. <br/>
        /// Invokes a <see cref="StateRequest"/> to handle the change.</remarks>
        /// <param name="sender">The new homepage dropdown control</param>
        /// <param name="e">Default event args</param>
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
        #endregion

    }
}
