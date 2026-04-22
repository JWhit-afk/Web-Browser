using Web_Browser_CW1.Handlers;
using Web_Browser_CW1.Presenter.SubPresenters;

namespace BrowserTests.PresenterTests {

    // IMPLEMENTED:
    //
    // Loading Bookmark UI (ON STARTUP):
    //
    // 1. ✔️ Loading all bookmarks into the bookmark drop-down.
    // 2. ✔️ Loading all bookmarks into the bookmark drop-down and toggling the bookmark button to active if the homepage is bookmarked.
    // 3. ✔️ Loading all bookmarks into the bookmark drop-down and toggling the bookmark button to inactive if the homepage is not bookmarked.
    // 4. ✔️ Nothing is loaded into the bookmark drop-down if there are no bookmarks, and the bookmark button is toggled to inactive.
    //
    // Bookmark Click Handling (Button Clicking):
    //
    // 1. ✔️ If URL is not bookmarked, it is added to the bookmark list and the bookmark button is toggled to active.
    // 2. ✔️ If URL is bookmarked, it is removed from the bookmark list and the bookmark button is toggled to inactive.

    [TestClass]
    public sealed class BookmarkingPresenterTests {

        private FakeBookmarkView bookmarkView = null!;
        private FakeNavigationView navigationView = null!;
        private BookmarkHandler bookmarkHandler = null!;
        private StateHandler stateHandler = null!;

        private BookmarkingPresenter presenter = null!;

        # region Test Setup
        [TestInitialize]
        public void SetUp() {
            bookmarkView = new FakeBookmarkView();
            navigationView = new FakeNavigationView();
            bookmarkHandler = new BookmarkHandler();
            stateHandler = new StateHandler();

            presenter = new BookmarkingPresenter(bookmarkView, navigationView, bookmarkHandler, stateHandler);
        }
        #endregion

        #region Loading Bookmark UI Tests On Startup
        [TestMethod]
        public void LoadBookmarkUITest() {

            bookmarkHandler.AddBookmark("https://www.example.com");
            bookmarkHandler.AddBookmark("https://www.google.com");
            bookmarkHandler.AddBookmark("https://www.github.com");

            presenter.LoadBookmarkUI();

            Assert.AreEqual(1, bookmarkView.UpdateBookmarksCalls);
            Assert.AreEqual(1, bookmarkView.UpdateBookmarkButtonCalls);
            CollectionAssert.AreEqual(bookmarkHandler.GetBookmarks(), bookmarkView.LoadedBookMarks);
        }

        [TestMethod]
        public void LoadBookmarkUITest_HomepageBookmarked() {

            stateHandler.homePageUrl = "https://www.example.com";
            bookmarkHandler.AddBookmark(stateHandler.homePageUrl);

            presenter.LoadBookmarkUI();

            Assert.IsTrue(bookmarkView.BookmarkButtonActive);
            CollectionAssert.AreEqual(bookmarkHandler.GetBookmarks(), bookmarkView.LoadedBookMarks);
        }

        [TestMethod]
        public void LoadBookmarkUITest_HomepageNotBookmarked() {

            stateHandler.homePageUrl = "https://www.example.com";
            bookmarkHandler.AddBookmark("https://www.google.com");
            bookmarkHandler.AddBookmark("https://www.github.com");

            presenter.LoadBookmarkUI();

            Assert.IsFalse(bookmarkView.BookmarkButtonActive);
            CollectionAssert.AreEqual(bookmarkHandler.GetBookmarks(), bookmarkView.LoadedBookMarks);
        }

        [TestMethod]
        public void LoadBookmarkUITest_NoBookmarks() {

            presenter.LoadBookmarkUI();

            Assert.IsFalse(bookmarkView.BookmarkButtonActive);
            Assert.IsEmpty(bookmarkView.LoadedBookMarks);
        }
        #endregion

        #region Bookmark Click Handling Tests
        [TestMethod]
        public void BookmarkClickTest_AddBookmark() {
            navigationView.UrlInput = "https://www.example.com";

            presenter.BookmarkClick();

            Assert.IsTrue(bookmarkHandler.IsBookmarked(navigationView.UrlInput));
            Assert.IsTrue(bookmarkView.BookmarkButtonActive);
            CollectionAssert.Contains(bookmarkView.LoadedBookMarks, navigationView.UrlInput);
        }

        [TestMethod]
        public void BookmarkClickTest_RemoveBookmark() {
            navigationView.UrlInput = "https://www.example.com";
            bookmarkHandler.AddBookmark(navigationView.UrlInput);

            presenter.BookmarkClick();

            Assert.IsFalse(bookmarkHandler.IsBookmarked(navigationView.UrlInput));
            Assert.IsFalse(bookmarkView.BookmarkButtonActive);
            CollectionAssert.DoesNotContain(bookmarkView.LoadedBookMarks, navigationView.UrlInput);
        }
        #endregion
    }
}