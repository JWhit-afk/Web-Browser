using System.Diagnostics;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Presenter {

    /// <summary>
    /// Root presenter that subscribes to view events and routes user actions to the facade.
    /// </summary>
    /// <remarks>Connects the passive view to feature presenters through <see cref="BrowserFacade"/>.
    /// This class is intended for internal use and is not thread-safe.</remarks>
    internal class BrowserPresenter {

        private Dictionary<StateArgs.Requests, Action<StateArgs>> _stateHandlers;

        private readonly IApplicationStateView applicationStateView;
        private readonly IBookmarkView bookmarkView;
        private readonly IHistoryView historyView;
        private readonly INavigationView navigationView;
        private readonly IPageView pageView;

        private readonly BrowserFacade facade;

        /// <summary>
        /// Initializes the root presenter, subscribes to view events, and triggers initial facade startup.
        /// </summary>
        /// <remarks>The view must be fully initialized before passing it to this presenter.</remarks>
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. line 62 initializes this field, but the compiler cannot infer that.
        public BrowserPresenter(
            WebBrowser app,
            BookmarkHandler bookmarkHandler,
            HistoryHandler historyHandler,
            StateHandler stateHandler) {

            this.applicationStateView = app;
            this.bookmarkView = app;
            this.historyView = app;
            this.navigationView = app;
            this.pageView = app;

            this.facade = new(
                bookmarkView, historyView, navigationView, pageView,
                bookmarkHandler, historyHandler, stateHandler);

            this.SubscribeEvents();
            this.facade.Initialise();
        }
#       pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor

        /// <summary>
        /// Subscribes view events and maps them to feature presenters via the facade.
        /// </summary>
        /// <remarks>Acts as the event-routing boundary between the view and presenter layer.</remarks>
        private void SubscribeEvents() {

            // Navigation Requests (History navigation, URL changes, drop-down requests etc) are delegated to the navigation coordinator.
            navigationView.HistoryNextClick += (_, _) => facade.Navigation.HistoryNext();
            navigationView.HistoryPreviousClick += (_, _) => facade.Navigation.HistoryPrevious();

            historyView.HistoryDropDownClick += (_, e) => facade.Navigation.HistoryRequest(e);
            bookmarkView.BookmarkDropDownClick += (_, e) => facade.Navigation.BookmarkRequest(e);

            navigationView.UrlSubmit += (_, _) => facade.Navigation.NavigateFromURL();

            // Bookmarking Requests (Bookmark UI updates, e.g., anything not related to navigation) are delegated to the bookmarking coordinator.
            bookmarkView.BookmarkClick += (_, e) => facade.Bookmarker.BookmarkClick();

            // Session Requests (Anything that requires saving / loading) are handled internally by _stateHandlers which, in turn calls the session coordinator.
            _stateHandlers = new()
            {
                { StateArgs.Requests.homePageLoad, _ => facade.Navigation.LoadHomepage() },
                { StateArgs.Requests.homePageSet, e => facade.Session.SetHomepage(e.homepage) },
                { StateArgs.Requests.save, _ => facade.Session.SaveSession() },
                { StateArgs.Requests.load, _ => facade.Session.LoadSession() },
            };
            applicationStateView.StateRequest += (_, e) => View_StateRequest(e);

            // Register shortcut handle
            applicationStateView.ShortcutPressed += (_, e) => facade.Shortcuts.Handle(e);
        }

        /// <summary>
        /// Handles state requests from the view and dispatches them to the mapped presenter action.
        /// </summary>
        /// <remarks>This method processes requests defined in <see cref="StateArgs.Requests"/>.</remarks>
        /// <param name="sender">The source of the event, typically the view component initiating the state request.</param>
        /// <param name="e">A <see cref="StateArgs"/> instance containing details about the specific state request and any associated
        /// data.</param>
        /// <exception cref="NotImplementedException">Thrown when the request is not specified in <paramref name="e"/></exception>
        private void View_StateRequest(StateArgs e) {

            Debug.WriteLine($"State request id: {e.request}");

            if (_stateHandlers.TryGetValue(e.request, out var handler)) {
                handler(e);
            } else {
                throw new NotImplementedException($"id:{e.request} is not a recognised request");
            }
        }
    }
}
