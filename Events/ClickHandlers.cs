using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Browser_CW1.Handlers;

namespace Web_Browser_CW1 {
    
    public partial class WebBrowser {

        //
        // Button Handlers
        //
        private async void SearchClick(object sender, EventArgs e) {
            RenderHtmlCode(await httpClient.Get(urlBar.Text));
            historyHandler.visit(urlBar.Text);
        }

        private void PrevClick(object sender, EventArgs e) { urlBar.Text = historyHandler.previousPage(); }
        
        private void NextClick(object sender, EventArgs e) { urlBar.Text = historyHandler.nextPage(); }
    }
}
