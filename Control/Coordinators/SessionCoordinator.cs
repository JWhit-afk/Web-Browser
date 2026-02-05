
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control.Coordinators {

    /// <summary>
    /// Coordinates the loading and saving of the session, which includes bookmarks, history, and state. 
    /// It acts as a central point for managing the session data and ensuring that all components are synchronized when loading or saving the session.
    /// </summary>
    internal class SessionCoordinator {

        BookmarkHandler BookmarkHandler;
        HistoryHandler HistoryHandler;
        StateHandler StateHandler;

        public SessionCoordinator(
            BookmarkHandler BookmarkHandler,
            HistoryHandler HistoryHandler, 
            StateHandler StateHandler) 
            {
            this.BookmarkHandler = BookmarkHandler;
            this.HistoryHandler = HistoryHandler;
            this.StateHandler = StateHandler;
        }

        /// <summary>
        /// Sets the homepage URL in the StateHandler, which is responsible for managing the state of the browser. 
        /// This allows the homepage to be loaded when the user clicks the homepage button or uses the corresponding shortcut.
        /// </summary>
        /// <param name="homepage">The non-empty string containing a URL of the homepage to be set to</param>
        public void SetHomepage(string homepage) {
            StateHandler.homePageUrl = homepage;
        }

        /// <summary>
        /// Loads the session data, including bookmarks, history, and state.
        /// </summary>
        public void LoadSession() {
            BookmarkHandler.LoadBookmarks();
            HistoryHandler.LoadHistory();
            StateHandler.LoadState();
        }

        /// <summary>
        /// Saves the session data, including bookmarks, history, and state. 
        /// This is typically called when the application is closing to ensure that the user's session is preserved for the next time they open the browser.
        /// </summary>
        public void SaveSession() {
            BookmarkHandler.SaveBookmarks();
            HistoryHandler.SaveHistory();
            StateHandler.SaveState();
        }
    }
}
