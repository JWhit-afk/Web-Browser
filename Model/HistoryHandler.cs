using System.Diagnostics;
using System.Text.Json;

namespace Web_Browser_CW1.Handlers {

internal class HistoryHandler {

        #region Exceptions
        /// <summary>
        /// The exception that is thrown when an attempt is made to access a position outside the valid range of the
        /// history collection.
        /// </summary>
        /// <remarks>This exception typically indicates that a requested index is less than zero or
        /// greater than or equal to the total number of items in the history.</remarks>
        public class HistoryOutOfBoundsException : Exception {
            public HistoryOutOfBoundsException(int position, int historySize)
                : base($"The requested history position {position} is out of bounds for history size {historySize}") { }
            public HistoryOutOfBoundsException(string message, Exception inner) : base(message, inner) { }
        }
        #endregion

        private const string HistoryFilePath = AppConstants.DataFilePath + "/history.json";

        private List<string> history = new List<string>();
        private int pointer = -1;

        /// <summary>
        /// Singleton collection handling all history items.
        /// </summary>
        /// <remarks>Provides services for mainting the state of the application by saving and loading 
        /// the history along with navigation.</remarks>
        public HistoryHandler() { }

        /// <summary>
        /// Adds the specified URL to the history and updates the current pointer to the front of the collection.
        /// </summary>
        /// <param name="url">The URL to add to the history</param>
        public void Register(string url) {
            history.Add(url);
            pointer = history.Count - 1;
        }

        /// <summary>
        /// Retrieves the URL of the previous page in the navigation history.
        /// </summary>
        /// <returns>A string containing the URL of the previous page.</returns>
        /// <exception cref="HistoryOutOfBoundsException">Thrown when there is no previous page in the history to navigate to.</exception>
        public string previousPage() {
            if (pointer <= 0) {
                throw new HistoryOutOfBoundsException(pointer - 1, history.Count);
            }
            pointer--;
            return history[pointer];
        }

        /// <summary>
        /// Advances to the next entry in the history and returns its URL.
        /// </summary>
        /// <returns>The value of the next history URL</returns>
        /// <exception cref="HistoryOutOfBoundsException">Thrown when there is no next entry in the history</exception>
        public string nextPage() {
            if (pointer >= history.Count - 1) {
                throw new HistoryOutOfBoundsException(pointer + 1, history.Count);
            }
            pointer++;
            return history[pointer];
        }

        /// <summary>
        /// Loads the history from a previous application instance.
        /// </summary>
        public void LoadHistory() {

            try {
                string json = File.ReadAllText(HistoryFilePath);

                if (json == "") return;
                List<string>? previousHistory = JsonSerializer.Deserialize<List<string>>(json);

                if (previousHistory != null) {
                    history = previousHistory;
                    pointer = history.Count - 1;
                }

            } catch (FileNotFoundException) {
                
                File.Create(HistoryFilePath).Close();
            }

        }

        /// <summary>
        /// Saves the current collection of bookmarks to persistent storage.
        /// </summary>
        public void SaveHistory() {

            Debug.WriteLine("Saving History...");
            string json = JsonSerializer.Serialize<List<string>>(history);
            File.WriteAllText(HistoryFilePath, json);
            Debug.WriteLine("History Saved:");
            Debug.WriteLine(json);

        }

        /// <summary>
        /// Retrieves the list of previously visited URLs.
        /// </summary>
        /// <returns>A list of strings containing URLs visited. The list will be empty if no URLs have been recorded.</returns>
        public List<string> GetHistory() {
            return history;
        }

        /// <summary>
        /// Returns the zero-based index of the history pointer, which indicates the index of the currently active URL in the history collection.
        /// </summary>
        /// <returns>The zero-based index of the current position</returns>
        public int GetPosition() {
            return pointer;
        }

        /// <summary>
        /// Returns the zero-based index of the specified URL within the browsing history.
        /// </summary>
        /// <remarks>If multiple entries match the specified URL, the index of the first occurrence is
        /// returned. The comparison is case-sensitive.</remarks>
        /// <param name="url">The URL to locate in the browsing history. Cannot be null.</param>
        /// <returns>The zero-based index of the URL if found; otherwise, –1.</returns>
        public int FindUrl(string url) {
            return history.FindIndex((string u) => {
                return u == url;
            });
        }

        /// <summary>
        /// Sets the position the history handler is pointing to
        /// </summary>
        /// <param name="position"></param>
        /// <exception cref="Exception">Thrown when <paramref name="position"/> is out of bounds of history handler </exception>
        public void SetPosition(int position) {
            if (position > history.Count - 1 || position < 0) {
                throw new HistoryOutOfBoundsException(position, history.Count);
            }
            pointer = position;
        }
    }
}
