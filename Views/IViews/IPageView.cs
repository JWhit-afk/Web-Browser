
namespace Web_Browser_CW1.Views {

    internal interface IPageView {

        void UpdateHTMLOutput(string htmlContent);
        void UpdateStatusCodeOutput(string statusCode);
        void UpdateTitleOutput(string title);
        void UpdateFaviconOutput(Bitmap favicon);

        void ToggleProgressIndicator(bool visible);
    }
}
