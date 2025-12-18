using System.Diagnostics;
using System.Text.Json;
using Web_Browser_CW1.Managers;

namespace Web_Browser_CW1.Handlers {

    internal class HistoryHandler {

        private const string HistoryFilePath = AppConstants.DataFilePath + "/history.json";

        ToolStripMenuItem historyStip;
        RequestHandler requestHandler;
        Button prevButton;
        Button nextButton;

        private List<string> history = new List<string>();
        private int pointer = -1;

        public HistoryHandler
            (
            ToolStripMenuItem historyStip,
            Button prevButton,
            Button nextButton,
            RequestHandler requestHandler
            ) {
            this.historyStip = historyStip;
            this.prevButton = prevButton;
            this.nextButton = nextButton;
            this.requestHandler = requestHandler;
        }

        public void visit(string url) {

            history.Add(url);
            pointer++;

            if (!prevButton.Enabled && history.Count > 1) {
                prevButton.Enabled = true;
                prevButton.Visible = true;
            }

            if (nextButton.Enabled) {
                nextButton.Enabled = false;
                nextButton.Visible = false;
            }

            // Always display the 10 most recent sites visitied.
            historyStip.DropDownItems.Clear();
            for (int i = 0; i < history.Count; i++) {
                var item = history[history.Count - 1 - i];
                var urlItem = new ToolStripMenuItem(item);

                urlItem.Click += (sender, e) => {
                    visit(item);
                    requestHandler.LoadPage(item);
                };

                historyStip.DropDownItems.Add(urlItem);
            }
        }

        public string previousPage() {

            pointer--;

            if (pointer == 0) {
                // Reached back of history -> hide back button
                prevButton.Enabled = false;
                prevButton.Visible = false;
            }

            if (!nextButton.Enabled) {
                // Ensure next button enabled when navigating back
                nextButton.Enabled = true;
                nextButton.Visible = true;
            }

            return history[pointer];
        }

        public string nextPage() {

            pointer++;

            if (pointer == history.Count - 1) {
                // Reached front of history -> hide next button.
                nextButton.Enabled = false;
                nextButton.Visible = false;
            }

            if (!prevButton.Enabled) {
                // Ensure prev button enabled when nagivating forward
                prevButton.Enabled = true;
                prevButton.Visible = true;
            }

            return history[pointer];
        }

        public void LoadHistory() {

            try {
                string json = File.ReadAllText(HistoryFilePath);

                if (json == "") return;
                List<string>? previousHistory = JsonSerializer.Deserialize<List<string>>(json);

                if (previousHistory != null) history = previousHistory;

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
    }
}
