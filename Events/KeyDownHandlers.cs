using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web_Browser_CW1.Handlers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Web_Browser_CW1 {

    public partial class WebBrowser {

        //
        // ToolMenu handlers
        //
        private void NewHomePageEnter(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.Enter) {
                stateHandler.homePageUrl = newHomepageText.Text;
            }
        }
    }
}
