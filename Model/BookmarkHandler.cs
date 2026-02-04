using System;
using System.Collections.Generic;
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
    internal class BookmarkHandler {

        private const string bookmarkFilePath = AppConstants.DataFilePath + "/bookmarks.json";

        private int capacity { get; }
        private List<string> bookmarks = new();

        public BookmarkHandler() { this.capacity = 10; }
        public BookmarkHandler(int capacity) { this.capacity = capacity; }

        public bool AddBookmark(string url) {
            if (bookmarks.Count >= 10 || bookmarks.Contains(url)) {
                return false;
        public void AddBookmark(string url) {
            if (bookmarks.Count >= this.capacity) {
                throw new BookmarkFullException(this.capacity, this.bookmarks.Count);
            } else if (bookmarks.Contains(url)) {
                throw new BookmarkAlreadyExistsException();
            }
            bookmarks.Add(url);
        }

        public bool RemoveBookmark(string url) {
        public void RemoveBookmark(string url) {
            if (!bookmarks.Contains(url)) {
                throw new BookmarkNotFoundException("Cannot find bookmark to remove");
            }

            bookmarks.Remove(url);
        }

        public bool IsBookmarked(string url) {
            Debug.WriteLine($"{url} in bookmarks? - {bookmarks.Contains(url)}");
            return bookmarks.Contains(url);
        }

        public List<string> GetBookmarks() {
            return bookmarks;
        }

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

        public void SaveBookmarks() {
            Debug.WriteLine("Saving Bookmarks...");
            string json = JsonSerializer.Serialize<List<string>>(bookmarks);
            File.WriteAllText(bookmarkFilePath, json);
            Debug.WriteLine("Bookmarks Saved:");
            Debug.WriteLine(json);
        }
    }
}
