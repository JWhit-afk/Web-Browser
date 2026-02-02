using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_Browser_CW1.Handlers {

    internal class BookmarkHandler {

        private const string bookmarkFilePath = AppConstants.DataFilePath + "/bookmarks.json";
        private List<string> bookmarks = new();

        public BookmarkHandler() { }

        public bool AddBookmark(string url) {
            if (bookmarks.Count >= 10 || bookmarks.Contains(url)) {
                return false;
            }

            bookmarks.Add(url);
            return true;
        }

        public bool RemoveBookmark(string url) {
            if (!bookmarks.Contains(url)) {
                return false;
            }

            bookmarks.Remove(url);
            return true;
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
