using System;

namespace Web_Browser_CW1.Views {

    public class UrlEvent : EventArgs {
        public required string url;
    }

    public class StateArgs : EventArgs {

        // Enum of possible requests to make to the state handler.
        public enum Requests {
            homePageLoad,
            homePageSet,
            save,
            load
        }

        public string homepage = string.Empty; 

        public required Requests request;
    }

    internal interface IView {

        event EventHandler<UrlEvent> UrlChanged;
        event EventHandler<UrlEvent> HistoryUpdate;
        event EventHandler<UrlEvent> HistoryItemClicked;

        event EventHandler<StateArgs> StateRequest;

        event EventHandler HistoryPreviousClick;
        event EventHandler HistoryNextClick;

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
        void SetURLInput(string url);
        #endregion

        #region Indicators
        void ToggleProgressIndicator(bool visible);
        #endregion
    }
}
