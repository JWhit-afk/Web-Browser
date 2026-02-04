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

        public HistoryHandler() { }

        public void register(string url) {
            history.Add(url);
            pointer = history.Count - 1;
        }

        public string previousPage() {
            if (pointer <= 0) {
                throw new HistoryOutOfBoundsException(pointer - 1, history.Count);
            }
            pointer--;
            return history[pointer];
        }

        public string nextPage() {
            if (pointer >= history.Count - 1) {
                throw new HistoryOutOfBoundsException(pointer + 1, history.Count);
            }
            pointer++;
            return history[pointer];
        }

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

        public void SaveHistory() {

            Debug.WriteLine("Saving History...");
            string json = JsonSerializer.Serialize<List<string>>(history);
            File.WriteAllText(HistoryFilePath, json);
            Debug.WriteLine("History Saved:");
            Debug.WriteLine(json);

        }

        public List<string> GetHistory() {
            return history;
        }

        public int GetPosition() {
            return pointer;
        }

        public int GetPosition(string url) {
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
