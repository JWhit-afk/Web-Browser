
using System.Diagnostics;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control.Coordinators {

    internal class BookmarkingCoordinator
        (
            IBookmarkView bookmarkView,
            INavigationView navigationView,
            BookmarkHandler BookmarkHandler,
            StateHandler StateHandler
         ) {

        private readonly IBookmarkView bookmarkView = bookmarkView;
        private readonly INavigationView navigationView = navigationView;

        private readonly BookmarkHandler BookmarkHandler = BookmarkHandler;
        private readonly StateHandler StateHandler = StateHandler;

        /// <summary>
        /// Handles the loading of the bookmark UI by retrieving the current list of bookmarks from the BookmarkHandler.
        /// </summary>
        public void LoadBookmarkUI() {

            // Get bookmarks from handler and update the view.
            bookmarkView.UpdateBookmarks(BookmarkHandler.GetBookmarks());

            // Determine if the homepage is bookmarked and update the bookmark button on the view accordingly.
            bookmarkView.ToggleBookmarkButton(BookmarkHandler.IsBookmarked(StateHandler.homePageUrl));
        }

        /// <summary>
        /// Handles the bookmark click event by toggling the bookmark status of the specified URL 
        /// and updating the icon and bookmark list in the view accordingly.
        /// </summary>
        /// <remarks>This method updates both the bookmark list and the bookmark button state in the view
        /// to reflect the current bookmark status of the URL.</remarks>
        /// <param name="sender">The source of the event, the bookmark button.</param>
        /// <param name="e">An event argument containing the URL to be bookmarked or unbookmarked.</param>
        public void BookmarkClick() {

            Debug.WriteLine($"Bookmark button clicked for URL");
            string url = navigationView.GetUrlInput();

            // Toggle bookmark status for the given URL.
            if (BookmarkHandler.IsBookmarked(url)) {

                // If its bookmarked, remove it.
                BookmarkHandler.RemoveBookmark(url);
            } else {

                // If its not bookmarked, add it.
                BookmarkHandler.AddBookmark(url);
            }

            // Update bookmark list on view.
            bookmarkView.UpdateBookmarks(BookmarkHandler.GetBookmarks());

            // Update button on view to reflect new status.
            bookmarkView.ToggleBookmarkButton(BookmarkHandler.IsBookmarked(url));
        }
    }
}
