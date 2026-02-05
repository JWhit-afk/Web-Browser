
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control.Coordinators {

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

        public void SetHomepage(string homepage) {
            StateHandler.homePageUrl = homepage;
        }

        public void LoadSession() {
            BookmarkHandler.LoadBookmarks();
            HistoryHandler.LoadHistory();
            StateHandler.LoadState();
        }

        public void SaveSession() {
            BookmarkHandler.SaveBookmarks();
            HistoryHandler.SaveHistory();
            StateHandler.SaveState();
        }
    }
}
