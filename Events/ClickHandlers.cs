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
        private async void HomeClick(object sender, EventArgs e) { 
            requestHandler.LoadPage(stateHandler.homePageUrl);
            historyHandler.visit(stateHandler.homePageUrl);
        }

        private async void PrevClick(object sender, EventArgs e) { 
            requestHandler.LoadPage(historyHandler.previousPage()); 
        }

        private async void NextClick(object sender, EventArgs e) { 
            requestHandler.LoadPage(historyHandler.nextPage()); 
        }

        private async void SearchClick(object sender, EventArgs e) { 
            requestHandler.LoadPage(urlBar.Text); 
            historyHandler.visit(urlBar.Text);
        }
    }
}
