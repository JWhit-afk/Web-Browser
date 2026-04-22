
namespace Web_Browser_CW1.Views {

    public interface IBookmarkView {

        event EventHandler BookmarkClick;
        event EventHandler<SelectedUrlArgs> BookmarkDropDownClick;

        void UpdateBookmarks(List<string> items);
        void UpdateBookmarkButton(bool isBookmarked);

    }
}
