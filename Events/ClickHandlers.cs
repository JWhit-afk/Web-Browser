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
            urlBar.Text = stateHandler.homePageUrl;
            RenderHtmlCode(await httpClient.Get(urlBar.Text));
            historyHandler.visit(urlBar.Text);
            progressBar.Hide();
        }

        private async void PrevClick(object sender, EventArgs e) {
            progressBar.Show();
            urlBar.Text = historyHandler.previousPage();
            RenderHtmlCode(await httpClient.Get(urlBar.Text));
            progressBar.Hide();
        }

        private async void NextClick(object sender, EventArgs e) {
            progressBar.Show();
            urlBar.Text = historyHandler.nextPage();
            RenderHtmlCode(await httpClient.Get(urlBar.Text));
            progressBar.Hide();
        }

        private async void SearchClick(object sender, EventArgs e) {
            progressBar.Show();
            RenderHtmlCode(await httpClient.Get(urlBar.Text));
            historyHandler.visit(urlBar.Text);
            progressBar.Hide();
        }
    }
}
