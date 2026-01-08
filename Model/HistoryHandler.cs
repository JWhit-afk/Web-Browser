using System.Diagnostics;
using System.Text.Json;

namespace Web_Browser_CW1.Handlers {

    internal class HistoryHandler {

        private const string HistoryFilePath = AppConstants.DataFilePath + "/history.json";

        private List<string> history = new List<string>();
        private int pointer = -1;

        public HistoryHandler() { }

        public void register(string url) {
            history.Add(url);
            pointer = history.Count - 1;
        }

        public string previousPage() {
            pointer--;
            return history[pointer];
        }

        public string nextPage() {
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
            Debug.WriteLine("State Saved:");
            Debug.WriteLine(json);

        }

        public List<string> GetHistory() {
            return history;
        }

        public int GetPosition() {
            return pointer;
        }
    }
}
