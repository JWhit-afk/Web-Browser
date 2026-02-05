using Web_Browser_CW1.Control.Coordinators;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control {

    // Deligates tasks to the relevent coordinators, such as navigation and session management.

    internal class MainCoordinator {

        BookmarkHandler BookmarkHandler;
        HistoryHandler HistoryHandler;
        StateHandler StateHandler;

        public NavigationCoordinator Navigation { get; }
        public SessionCoordinator Session { get; }

        public BookmarkingCoordinator Bookmarker { get; }

        public MainCoordinator(
                // Get view reference to pass to coordinators.
                IView view,

                // Handlers for the coordinators to use, passed in from the control layer.
                BookmarkHandler bookmarkHandler,
                HistoryHandler historyHandler,
                StateHandler stateHandler
            ) {

            // Assign references to handlers for use in the coordinators.
            this.BookmarkHandler = bookmarkHandler;
            this.HistoryHandler = historyHandler;
            this.StateHandler = stateHandler;

            // Initalise logic coordinators, passing in the handlers needed.
            this.Navigation = new NavigationCoordinator(
                view,
                HistoryHandler,
                BookmarkHandler,
                StateHandler
            );
            this.Session = new SessionCoordinator(
                BookmarkHandler,
                HistoryHandler,
                StateHandler
            );
            this.Bookmarker = new BookmarkingCoordinator(
                view,
                BookmarkHandler,
                StateHandler
            );
        }

        public void Initialise() {

            Session.LoadSession();
            Navigation.LoadHomepage();

            // Update the bookmark UI to reflect any bookmarks loaded from previous sessions.
            Bookmarker.LoadBookmarkUI();
        }

    }
}
