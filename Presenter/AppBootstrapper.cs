using Web_Browser_CW1.Handlers;

namespace Web_Browser_CW1.Presenter {

    /// <summary>
    /// Connects dependencies
    /// </summary>
    internal class AppBootstrapper {
        public static void Initialise(WebBrowser app) {

            // Construct model layer
            var bookmarkHandler = new BookmarkHandler();
            var historyHandler = new HistoryHandler();
            var stateHandler = new StateHandler();

            // Construct presenter, passing in dependencies
            var presenter = new BrowserPresenter(app, bookmarkHandler, historyHandler, stateHandler);
        }
    }
}
