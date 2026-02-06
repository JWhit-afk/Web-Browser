using Web_Browser_CW1.Control.Coordinators;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control {

    /// <summary>
    /// Delegates tasks to the relevant coordinators, such as navigation and session management.
    /// </summary>
    /// <remarks>Acts as a coordinator factory. Sets up the others that will handle the delegation.</remarks>
    internal class MainCoordinator {

        public NavigationCoordinator Navigation { get; }
        public SessionCoordinator Session { get; }

        public BookmarkingCoordinator Bookmarker { get; }

        public ShortcutCoordinator Shortcuts { get;  }

        public MainCoordinator(
                // Get view references to pass to coordinators.
                IBookmarkView bookmarkView,
                IHistoryView historyView,
                INavigationView navigationView,
                IPageView pageView,

                // Handlers for the coordinators to use, passed in from the control layer.
                BookmarkHandler bookmarkHandler,
                HistoryHandler historyHandler,
                StateHandler stateHandler
            ) {

            // Initialise logic coordinators, passing in the handlers needed.
            this.Navigation = new NavigationCoordinator(
                navigationView,
                bookmarkView,
                pageView,
                historyView,

                historyHandler,
                bookmarkHandler,
                stateHandler
            );
            this.Session = new SessionCoordinator(
                bookmarkHandler,
                historyHandler,
                stateHandler
            );
            this.Bookmarker = new BookmarkingCoordinator(
                bookmarkView,
                navigationView,

                bookmarkHandler,
                stateHandler
            );

            // Special - shortcut coordinator uses other coordinators
            this.Shortcuts = new ShortcutCoordinator(
                Navigation,
                Bookmarker
            );
        }

        /// <summary>
        /// Initialises the application by loading data from the previous session (if any).
        /// </summary>
        public void Initialise() {

            Session.LoadSession();
            Navigation.LoadHomepage();

            // Update the bookmark UI to reflect any bookmarks loaded from previous sessions.
            Bookmarker.LoadBookmarkUI();
        }

    }
}
