using System;

namespace Web_Browser_CW1.Views {

    #region Event Types

    /// <summary>
    /// Provides event data for state-related operations, including the requested action.
    /// </summary>
    /// <remarks>The class is used to pass details about state requests, such as loading or saving state, to
    /// event handlers. The <see cref="Requests"/> enumeration specifies the type of operation being requested.</remarks>
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
    #endregion

    /// <summary>
    /// Interface defining the contract for the View component in the MVC architecture.
    /// </summary>
    internal interface IView {

        #region Event Handlers
        event EventHandler UrlChanged;

        event EventHandler<StateArgs> StateRequest;

        event EventHandler HistoryPreviousClick;
        event EventHandler HistoryNextClick;
        event EventHandler HistoryRegister;

        event EventHandler HistoryDropDownClick;
        event EventHandler BookmarkDropDownClick;

        event EventHandler BookmarkClick;

        event EventHandler<UrlEvent> BookmarkClick;
        #endregion

        #region View Control Methods
        #region HTML Outputs
        void UpdateHTMLOutput(string htmlContent);
        void UpdateStatusCodeOutput(string statusCode);
        void UpdateTitleOutput(string title);
        void UpdateFaviconOutput(Bitmap favicon);
        #endregion

        #region History Elements
        void UpdateHistoryDropDown(List<string> items);
        void TogglePreviousButton(bool enabled);
        void ToggleNextButton(bool enabled);
        void RefreshHistoryButtons(int historyCount, int position);
        #endregion

        #region Bookmark Elements
        void UpdateBookmarks(List<string> items);
        void ToggleBookmarkButton(bool isBookmarked);
        #endregion

        #region Inputs
        void SetURLInput(string url);
        string GetUrlInput();
        #endregion

        #region Indicators
        void ToggleProgressIndicator(bool visible);
        #endregion
        #endregion
    }
}
