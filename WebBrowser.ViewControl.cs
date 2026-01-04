using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Web_Browser_CW1 {

    partial class WebBrowser {

        //
        // History related methods
        //

        public void UpdateHistoryButtons() {

            var historyLength = historyHandler.GetHistory().Count;
            var historyPosition = historyHandler.GetPosition();

            if (historyLength > 0 && historyPosition < historyLength - 1) {
                ToggleNextButton(true);
            } else {
                ToggleNextButton(false);
            }

            if (historyLength > 0 && historyPosition > 0) {
                TogglePreviousButton(true);
            } else {
                TogglePreviousButton(false);
            }

        }

        // Updates the history dropdown menu with the 10 most recent sites visited.
        public void UpdateHistroyDropDown() {

            // Clear existing items
            historyToolStripMenuItem.DropDownItems.Clear();

            // Build from newest to oldest
            List<string> list = historyHandler.GetHistory();
            for (int i = 0; i < list.Count; i++) {
                var item = list[list.Count - 1 - i];
                var urlItem = new ToolStripMenuItem(item);

                urlItem.Click += (sender, e) => {
                    historyHandler.visit(item);
                    requestHandler.LoadPage(item);
                    UpdateHistroyDropDown();
                };

                historyToolStripMenuItem.DropDownItems.Add(urlItem);
            }
        }

        public void TogglePreviousButton(bool enabled) {
            previousPage.Enabled = enabled;
            previousPage.Visible = enabled;
        }

        public void ToggleNextButton(bool enabled) {
            nextPage.Enabled = enabled;
            nextPage.Visible = enabled;
        }
    }
}
