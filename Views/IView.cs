using System;

namespace Web_Browser_CW1.Views {

    internal interface IView {

        event EventHandler LoadPageURLBar;
        event EventHandler LoadHomepage;

        event EventHandler HistoryPrevious;
        event EventHandler HistoryNext;

        event EventHandler HomepageUpdate;

        event EventHandler SaveState;

        #region HTML Outputs
        void UpdateHTMLOutput(string htmlContent);
        void UpdateStatusCodeOutput(string statusCode);
        void UpdateTitleOutput(string title);
        void UpdateFaviconOutput(Icon favicon);
        #endregion

        #region History Elements
        void UpdateHistoryDropDown(List<string> items);
        void TogglePreviousButton(bool enabled);
        void ToggleNextButton(bool enabled);
        void RefreshHistoryButtons(int historyCount, int position);
        #endregion

        #region Inputs
        string GetURLInput();
        void SetURLInput(string url);
        #endregion

        #region Indicators
        void ToggleProgressIndicator(bool visible);
        #endregion
    }
}
