
namespace Web_Browser_CW1.Views {

    /// <summary>
    /// Interface defining any navigation-related UI elements, such as the URL input field and history navigation buttons.
    /// </summary>
    internal interface INavigationView {

        event EventHandler HistoryPreviousClick;
        event EventHandler HistoryNextClick;

        event EventHandler UrlSubmit;

        // URL input field
        void SetURLInput(string url);
        string GetUrlInput();

        // History navigation buttons
        void TogglePreviousButton(bool enabled);
        void ToggleNextButton(bool enabled);
        void RefreshHistoryButtons(int historyCount, int position);
    }
}
