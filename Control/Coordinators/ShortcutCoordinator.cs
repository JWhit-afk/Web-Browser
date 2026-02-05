
using System.Diagnostics;
using System.Windows.Forms;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control.Coordinators {

    internal class ShortcutCoordinator {

        private readonly Dictionary<Keys, Action> _shortcutHandlers;

        NavigationCoordinator Navigation;
        BookmarkingCoordinator Bookmarker;

        public ShortcutCoordinator(
            NavigationCoordinator Navigation,
            BookmarkingCoordinator Bookmarker
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

        public void Handle(ShortcutEventArgs e) {

            Debug.WriteLine($"Handle Shortcut: {e.Keys}");

            if (_shortcutHandlers.TryGetValue(e.Keys, out var action)) {
                e.Handled = true;
                action();
            }
        }


    }
}
