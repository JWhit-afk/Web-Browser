
using System.Diagnostics;
using System.Security.Policy;
using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Views;

namespace Web_Browser_CW1.Control.Coordinators {

    internal class BookmarkingCoordinator {

        IView view;
        BookmarkHandler BookmarkHandler;
        StateHandler StateHandler;

        public BookmarkingCoordinator(
            IView view,
            BookmarkHandler BookmarkHandler,
            StateHandler StateHandler
            ) {

            this.view = view;
            this.BookmarkHandler = BookmarkHandler;
            this.StateHandler = StateHandler;
        }

        /// <summary>
        /// Handles the loading of the bookmark UI by retrieving the current list of bookmarks from the BookmarkHandler.
        /// </summary>
        public void LoadBookmarkUI() {

            // Get bookmarks from handler and update the view.
            view.UpdateBookmarks(BookmarkHandler.GetBookmarks());

            // Determine if the homepage is bookmarked and update the bookmark button on the view accordingly.
            view.ToggleBookmarkButton(BookmarkHandler.IsBookmarked(StateHandler.homePageUrl));
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
            string url = view.GetUrlInput();

            // Toggle bookmark status for the given URL.
            if (BookmarkHandler.IsBookmarked(url)) {

                // If its bookmarked, remove it.
                BookmarkHandler.RemoveBookmark(url);
            } else {

                // If its not bookmarked, add it.
                BookmarkHandler.AddBookmark(url);
            }

            // Update bookmark list on view.
            view.UpdateBookmarks(BookmarkHandler.GetBookmarks());

            // Update button on view to reflect new status.
            view.ToggleBookmarkButton(BookmarkHandler.IsBookmarked(url));
        }
    }
}
