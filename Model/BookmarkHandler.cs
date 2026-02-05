using System.Diagnostics;
using System.Text.Json;

namespace Web_Browser_CW1.Handlers {

    #region Exceptions
    /// <summary>
    /// This exception is thrown when adding a bookmark to a full collection.
    /// </summary>
    public class BookmarkFullException : Exception {
        private int capacity { get; }
        private int current { get; }
        public BookmarkFullException(int capacity, int current) 
            : base($"Cannot add bookmark to collection of maximum capacity of: {capacity}, with current size of {current}")
            { this.capacity = capacity; this.current = current; }
        public BookmarkFullException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// This exception is thrown when a bookmark to be added already exists in the collection.
    /// </summary>
    public class BookmarkAlreadyExistsException : Exception {
        public BookmarkAlreadyExistsException()
            : base($"Cannot add bookmark to collection as it already exists") { }
        public BookmarkAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// This exception is thrown when a bookmark to be removed cannot be found.
    /// </summary>
    public class BookmarkNotFoundException : Exception {
        public BookmarkNotFoundException(string url) 
            : base($"Cannot remove bookmark {url} from collection as it does not exist") { }
        public BookmarkNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
    #endregion

    /// <summary>
    /// Singleton collection handling all bookmarks.
    /// </summary>
    /// <remarks>Provides services for maintaining the state of the application by saving and loading 
    /// the bookmarks.</remarks>
    internal class BookmarkHandler {

        private const string bookmarkFilePath = AppConstants.DataFilePath + "/bookmarks.json";

        private int capacity { get; }
        private List<string> bookmarks = new();

        public BookmarkHandler() { this.capacity = 10; }
        public BookmarkHandler(int capacity) { this.capacity = capacity; }

        /// <summary>
        /// Adds a URL to the bookmark collection.
        /// </summary>
        /// <remarks>Rejects the URL if the maximum number of URLs are contained or the URL is already included.</remarks>
        /// <param name="url">The URL to add.</param>
        /// <exception cref="BookmarkFullException">Thrown when bookmark collection is full or URL already exists in collection</exception>
        public void AddBookmark(string url) {
            if (bookmarks.Count >= this.capacity) {
                throw new BookmarkFullException(this.capacity, this.bookmarks.Count);
            } else if (bookmarks.Contains(url)) {
                throw new BookmarkAlreadyExistsException();
            }
            bookmarks.Add(url);
        }

        /// <summary>
        /// Removes the bookmark associated with the specified URL from the collection.
        /// </summary>
        /// <param name="url">The URL of the bookmark to remove.</param>
        /// <exception cref="BookmarkNotFoundException">Thrown if a bookmark with the specified URL does not exist in the collection.</exception>
        public void RemoveBookmark(string url) {
            if (!bookmarks.Contains(url)) {
                throw new BookmarkNotFoundException("Cannot find bookmark to remove");
            }

            bookmarks.Remove(url);
        }

        /// <summary>
        /// Determines whether the specified URL is present in the bookmarks collection.
        /// </summary>
        /// <param name="url">The URL to check for in the collection</param>
        /// <returns>true if the specified URL is bookmarked; otherwise, false.</returns>
        public bool IsBookmarked(string url) {
            Debug.WriteLine($"{url} in bookmarks? - {bookmarks.Contains(url)}");
            return bookmarks.Contains(url);
        }

        /// <summary>
        /// Retrieves a list of all saved bookmarks.
        /// </summary>
        /// <remarks>The returned list will contain no more than 10 elements.</remarks>
        /// <returns>A list of strings containing the names or identifiers of all bookmarks. The list will be empty if no
        /// bookmarks are saved.</returns>
        public List<string> GetBookmarks() {
            return bookmarks;
        }

        /// <summary>
        /// Loads bookmarks from a previous instance of the application.
        /// </summary>
        public void LoadBookmarks() {

            try {
                string json = File.ReadAllText(bookmarkFilePath);

                if (json == "") return;
                List<string>? previousBookmarks = JsonSerializer.Deserialize<List<string>>(json);

                if (previousBookmarks != null) {
                    bookmarks = previousBookmarks;
                }

            } catch (FileNotFoundException) {

                File.Create(bookmarkFilePath).Close();
            }

        }

        /// <summary>
        /// Saves the current collection of bookmarks to persistent storage.
        /// </summary>
        public void SaveBookmarks() {
            Debug.WriteLine("Saving Bookmarks...");
            string json = JsonSerializer.Serialize<List<string>>(bookmarks);
            File.WriteAllText(bookmarkFilePath, json);
            Debug.WriteLine("Bookmarks Saved:");
            Debug.WriteLine(json);
        }
    }
}
