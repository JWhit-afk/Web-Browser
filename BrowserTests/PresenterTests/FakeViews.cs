using System.Drawing;
using Web_Browser_CW1.Views;

namespace BrowserTests.PresenterTests {

    public sealed class FakeBookmarkView : IBookmarkView {
        public event EventHandler BookmarkClick = delegate { };
        public event EventHandler<SelectedUrlArgs> BookmarkDropDownClick = delegate { };

        public List<string> LoadedBookMarks { get; private set; } = new();
        public bool BookmarkButtonActive { get; private set; } = false;

        public int UpdateBookmarksCalls { get; private set; }
        public int UpdateBookmarkButtonCalls { get; private set; }

        public void UpdateBookmarks(List<string> items) {
            UpdateBookmarksCalls++;
            LoadedBookMarks = items;
        }

        public void UpdateBookmarkButton(bool isBookmarked) {
            UpdateBookmarkButtonCalls++;
            BookmarkButtonActive = isBookmarked;
        }
    }

    public sealed class FakeNavigationView : INavigationView {
        public event EventHandler HistoryPreviousClick = delegate { };
        public event EventHandler HistoryNextClick = delegate { };
        public event EventHandler UrlSubmit = delegate { };

        public string UrlInput { get; set; } = string.Empty;
        public string GetUrlInput() => UrlInput;
        public void SetURLInput(string url) {
            UrlInput = url;
        }

        public bool PreviousButtonEnabled { get; private set; } = false;
        public bool NextButtonEnabled { get; private set; } = false;

        public void TogglePreviousButton(bool enabled) { }
        public void ToggleNextButton(bool enabled) { }
        public void RefreshHistoryButtons(int historyCount, int position) { }
    }

}
