using Web_Browser_CW1.Presenter.SubPresenters;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Presenter {

    /// <summary>
    /// Facade over feature presenters (navigation, session, bookmarks, and shortcuts).
    /// </summary>
    /// <remarks>Builds and exposes sub-presenters used by the root presenter.</remarks>
    internal class BrowserFacade {

        public NavigationPresenter Navigation { get; }
        public SessionPresenter Session { get; }
        public BookmarkingPresenter Bookmarker { get; }
        public ShortcutPresenter Shortcuts { get;  }

        public BrowserFacade(
                // Get view references to pass to presenters.
                IBookmarkView bookmarkView,
                IHistoryView historyView,
                INavigationView navigationView,
                IPageView pageView,

                // Handlers for presenters to use, passed in from the presenter layer.
                BookmarkHandler bookmarkHandler,
                HistoryHandler historyHandler,
                StateHandler stateHandler
            ) {

            // Initialise feature presenters, passing in the handlers needed.
            this.Navigation = new NavigationPresenter(
                navigationView,
                bookmarkView,
                pageView,
                historyView,

                historyHandler,
                bookmarkHandler,
                stateHandler
            );
            this.Session = new SessionPresenter(
                bookmarkHandler,
                historyHandler,
                stateHandler
            );
            this.Bookmarker = new BookmarkingPresenter(
                bookmarkView,
                navigationView,

                bookmarkHandler,
                stateHandler
            );

            // Special - shortcut presenter uses other presenters
            this.Shortcuts = new ShortcutPresenter(
                Navigation,
                Bookmarker
            );
        }

        /// <summary>
        /// Initialises the application by loading data from the previous session (if any).
        /// </summary>
        public void Initialise() {

            Session.LoadSession();
            Bookmarker.LoadBookmarkUI();

            Navigation.LoadHomepage();
        }

    }
}
