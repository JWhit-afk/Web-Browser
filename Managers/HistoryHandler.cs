
namespace Web_Browser_CW1.Handlers {

    internal class HistoryHandler {

        ToolStripMenuItem historyStip;
        Button prevButton;
        Button nextButton;

        // Slinky approach
        private Stack<string> history = new Stack<string>();
        private Stack<string> navigate = new Stack<string>();

        public HistoryHandler(ToolStripMenuItem historyStip, Button prevButton, Button nextButton) { 
            this.historyStip = historyStip;
            this.prevButton = prevButton;
            this.nextButton = nextButton;
        }

        public void visit(string url) { 

            history.Push(url);

            if (!prevButton.Enabled && history.Count > 1) {
                prevButton.Enabled = true;
                prevButton.Visible = true;
            }

            // Always display the 10 most recent sites visitied.
            historyStip.DropDownItems.Clear();
            foreach (var item in updateHistory()) {
                historyStip.DropDownItems.Add(item);
            }
        }

        public string previousPage () {

            // Add the page to navigation stack to use the next page button.
            navigate.Push(history.Pop());

            if (history.Count == 0) {
                // Reached back of history -> hide back button
                prevButton.Enabled = false;
                prevButton.Visible = false;
            }

            if (!nextButton.Enabled) {
                // Ensure next button enabled when navigating back
                nextButton.Enabled = true;
                nextButton.Visible = true;
            }

            return navigate.Peek();
        }

        public string nextPage() {

            // Add the page back to history.
            history.Push(navigate.Pop());

            if (navigate.Count == 0) {
                // Reached front of history -> hide next button.
                nextButton.Enabled = false;
                nextButton.Visible = false;
            }

            if (!prevButton.Enabled) {
                // Ensure prev button enabled when nagivating forward
                prevButton.Enabled = true;
                prevButton.Visible = true;
            }

            return history.Peek();
        }

        public List<string> updateHistory() {

            int count = 0;
            List<string> items = new List<string>();

            foreach (var item in history) {
                if (count > 10) return items;
                count++;
                items.Add(item);
            }

            foreach (var item in navigate) {
                if (count > 10) return items;
                count++;
                items.Add(item);
            }

            return items;
        }
    }
}
