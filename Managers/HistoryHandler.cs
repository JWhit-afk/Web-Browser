
using System.Reflection;
using Web_Browser_CW1.Managers;

namespace Web_Browser_CW1.Handlers {

    internal class HistoryHandler {

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
            foreach (var item in updateHistory()) {
                var urlItem = new ToolStripMenuItem(item);

                urlItem.Click += (sender, e) => {
                    visit(item);
                    requestHandler.LoadPage(item);
                };

                historyStip.DropDownItems.Add(urlItem);
            }
        }

        public string previousPage () {

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

        public List<string> updateHistory() {

            int count = 0;
            List<string> items = new List<string>();

            foreach (var item in history) {
                if (count > 10) return items;
                count++;
                items.Insert(0, item);
            }

            return items;
        }
    }
}
