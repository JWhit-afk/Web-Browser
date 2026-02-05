using System.Diagnostics;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control {

    /// <summary>
    /// Coordinates interactions between the user interface and application logic, managing history, bookmarks, and
    /// state within the browser view.
    /// </summary>
    /// <remarks>The Controller subscribes to events from the view and delegates tasks to specialized
    /// handlers for history, bookmarks, and state management. This class is intended for internal use and is not
    /// thread-safe.</remarks>
    internal class Controller {

        private Dictionary<StateArgs.Requests, Action<StateArgs>> _stateHandlers;

        private readonly IView view;
        private readonly MainCoordinator coordinator;

        private readonly BookmarkHandler BookmarkHandler;
        private readonly HistoryHandler HistoryHandler;
        private readonly StateHandler StateHandler;

        /// <summary>
        /// Initializes a new instance of the Controller class and registers event handlers.
        /// </summary>
        /// <remarks>This constructor registers view events and loads the saved
        /// data (bookmarks, history and homepage). The view must be fully initialized before passing it to the
        /// controller.</remarks>
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. line 50 initializes this field, but the compiler cannot infer that.
        public Controller(IView view) {

            // Construct handlers (the model)
            BookmarkHandler = new();
            HistoryHandler = new();
            StateHandler = new();

            // Get app reference (Delegation service) and view (GUI Updates)
            this.view = view;
            this.coordinator = new(view, BookmarkHandler, HistoryHandler, StateHandler);

            // Subscribe events and register handlers for state requests.
            this.SubscribeEvents();

            // Pass control to main coordinator.
            this.coordinator.Initialise();
        }
#       pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor

        /// <summary>
        /// Subscribes coordinator handlers to the views events.
        /// </summary>
        /// <remarks>Dab</remarks>
        private void SubscribeEvents() {

            // Navigation Requests (History navigation, URL changes, drop-down requests etc) are delegated to the navigation coordinator.
            view.HistoryNextClick += (_, _) => coordinator.Navigation.HistoryNext();
            view.HistoryPreviousClick += (_, _) => coordinator.Navigation.HistoryPrevious();

            view.HistoryDropDownClick += (_, e) => coordinator.Navigation.HistoryRequest(e);
            view.BookmarkDropDownClick += (_, e) => coordinator.Navigation.BookmarkRequest(e);

            view.UrlSubmit += (_, _) => coordinator.Navigation.NavigateFromURL();

            // Bookmarking Requests (Bookmark UI updates, e.g., anything not related to navigation) are delegated to the bookmarking coordinator.
            view.BookmarkClick += (_, e) => coordinator.Bookmarker.BookmarkClick();

            // Session Requests (Anything that requires saving / loading) are handled internally by _stateHandlers which, in turn calls the session coordinator.
            _stateHandlers = new()
            {
                { StateArgs.Requests.homePageLoad, _ => coordinator.Navigation.LoadHomepage() },
                { StateArgs.Requests.homePageSet, e => coordinator.Session.SetHomepage(e.homepage) },
                { StateArgs.Requests.save, _ => coordinator.Session.SaveSession() },
                { StateArgs.Requests.load, _ => coordinator.Session.LoadSession() },
            };
            view.StateRequest += (_, e) => View_StateRequest(e);

            // Register shortcut handle
            view.ShortcutPressed += (_, e) => coordinator.Shortcuts.Handle(e);
        }

        /// <summary>
        /// Handles state-related requests triggered by the view, such as loading or saving application state, updating
        /// the homepage, and responding to homepage load events.
        /// </summary>
        /// <remarks>This method processes requests defined in <see cref="StateArgs.Requests"/></remarks>
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
