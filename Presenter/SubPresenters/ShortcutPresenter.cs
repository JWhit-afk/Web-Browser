using System.Diagnostics;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Presenter.SubPresenters {

    /// <summary>
    /// Handles keyboard shortcuts and delegates actions to feature presenters
    /// (e.g., <see cref="NavigationPresenter"/> and <see cref="BookmarkingPresenter"/>).
    /// </summary>
    internal class ShortcutPresenter {

        private readonly Dictionary<Keys, Action> _shortcutHandlers;

        private readonly NavigationPresenter Navigation;
        private readonly BookmarkingPresenter Bookmarker;

        public ShortcutPresenter (
            NavigationPresenter Navigation,
            BookmarkingPresenter Bookmarker
            ) {

            this.Navigation = Navigation;
            this.Bookmarker = Bookmarker;

            // Register Shortcuts (e.g., Ctrl + S to save) are handled internally by _shortcutHandlers which, in turn calls the relevant coordinator.
            _shortcutHandlers = new()
            {
                { Keys.Control | Keys.H,        () => Navigation.LoadHomepage() },      // Load homepage (Ctrl + H)
                { Keys.Control | Keys.Right,    () => Navigation.HistoryNext() },       // History next (Ctrl + Right Arrow)
                { Keys.Control | Keys.Left,     () => Navigation.HistoryPrevious() },   // History previous (Ctrl + Left Arrow)
                { Keys.Enter,                   () => Navigation.NavigateFromURL() },   // URL change (Enter)
                { Keys.F5,                      () => Navigation.NavigateFromURL() },   // URL change (F5)
                { Keys.Control | Keys.B,        () => Bookmarker.BookmarkClick() }      // Bookmark/Unbookmark page (Ctrl + B)
            };
        }

        /// <summary>
        /// Executes the mapped shortcut action from <see cref="_shortcutHandlers"/>.
        /// If a matching shortcut is found, marks the event as handled.
        /// </summary>
        /// <param name="e">The event arguments containing the key(s) pressed and a callback to designate 
        /// if the combination has been handled internally to prevent propagation of already handled shortcuts.</param>
        public void Handle(ShortcutEventArgs e) {

            Debug.WriteLine($"Handle Shortcut: {e.Keys}");

            if (_shortcutHandlers.TryGetValue(e.Keys, out var action)) {
                e.Handled = true;
                action();
            }
        }


    }
}
