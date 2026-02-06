
namespace Web_Browser_CW1.Views {

    internal interface IBookmarkView {

        event EventHandler BookmarkClick;
        event EventHandler<SelectedUrlArgs> BookmarkDropDownClick;

        void UpdateBookmarks(List<string> items);
        void ToggleBookmarkButton(bool isBookmarked);

    }
}
