using System.Text.Json;
using Web_Browser_CW1;
using Web_Browser_CW1.Handlers;

namespace BrowserTests.ModelTests {

    [TestClass, DoNotParallelize]
    public sealed class BookmarkHandlerTest {

        // IMPLEMENTED:
        //
        // Adding bookmark tests
        //
        // 1. ✔️ Test that adding a bookmark to a collection adds the bookmark successfully.
        // 3. ✔️ Test that adding a bookmark to a full collection throws a BookmarkFullException.
        // 4. ✔️ Test that adding a bookmark that already exists in the collection throws a BookmarkAlreadyExistsException.
        //
        // Removing bookmark tests
        //
        // 5. ✔️ Test that removing a bookmark that exists in the collection removes the bookmark successfully.
        // 6. ✔️ Test that removing a bookmark that does not exist in the collection throws a BookmarkNotFoundException.
        //
        // Bookmark existence tests
        //
        // 7. ✔️ Test that the bookmark collection correctly identifies whether a URL is bookmarked or not.
        // 8. ✔️ Test that the bookmark collection correctly identifies whether a URL is bookmarked or not after adding and removing bookmarks.
        // 9. ✔️ Test that the bookmark collection correctly identifies whether a URL is bookmarked or not when the collection is empty.
        //
        // Saving and Loading tests
        //
        // 10. ✔️ Test that saving the bookmark collection to a file creates the file and saves the correct data.
        // 11. ✔️ Test that loading the bookmark collection from a file loads the correct data into the collection.

        private BookmarkHandler Bookmarker;

        private static readonly string SaveTestDirectory = AppConstants.TestDataFilePath + "/SaveBookmarks.json";
        private static readonly string LoadTestDirectory = AppConstants.TestDataFilePath + "/LoadBookmarks.json";

        public BookmarkHandlerTest() {
            Bookmarker = new();
        }

        #region Test Setup and Tear-down
        [TestInitialize]
        public void SetUp() {

            // Ensure test directory exist.
            Directory.CreateDirectory(AppConstants.TestDataFilePath);
        }

        [TestCleanup]
        public void TearDown() {

            // Delete test files if they exist.
            if (File.Exists(SaveTestDirectory))
                File.Delete(SaveTestDirectory);

            if (File.Exists(LoadTestDirectory))
                File.Delete(LoadTestDirectory);
        }
        #endregion

        #region Adding bookmark tests
        [TestMethod]
        public void AddingBookmarkTest() {

            Bookmarker.AddBookmark("https://www.example.com");

            Assert.AreEqual("https://www.example.com", Bookmarker.GetBookmarks().First());

            Bookmarker.AddBookmark("https://www.google.com");
            Bookmarker.AddBookmark("https://www.facebook.com");

            List<string> expected = new() {
                "https://www.example.com",
                "https://www.google.com",
                "https://www.facebook.com"
            };

            CollectionAssert.AreEqual(expected, Bookmarker.GetBookmarks());
        }

        [TestMethod]
        public void AddingBookmarkToFullCollectionTest() {

            for (int i = 0; i < 10; i++) {
                Bookmarker.AddBookmark($"https://www.example{i}.com");
            }

            Assert.Throws<BookmarkFullException>(() => Bookmarker.AddBookmark("https://www.google.com"));
        }

        [TestMethod]
        public void AddingDuplicateBookmarkTest() {

            Bookmarker.AddBookmark("https://www.example.com");

            Assert.Throws<BookmarkAlreadyExistsException>(() => Bookmarker.AddBookmark("https://www.example.com"));
        }
        #endregion

        #region Removing Bookmark Tests
        [TestMethod]
        public void RemovingBookmarkTest() {

            Bookmarker.AddBookmark("https://www.example.com");

            Bookmarker.RemoveBookmark("https://www.example.com");
            Assert.IsFalse(Bookmarker.IsBookmarked("https://www.example.com"));
        }

        [TestMethod]
        public void RemovingNonExistentBookmarkTest() {

            Bookmarker.AddBookmark("https://www.example.com");

            Assert.Throws<BookmarkNotFoundException>(() => Bookmarker.RemoveBookmark("https://www.google.com"));
        }
        #endregion

        #region Bookmark Existence Tests
        [TestMethod]
        public void BookmarkExistenceTest() {

            Bookmarker.AddBookmark("https://www.example.com");

            Assert.IsTrue(Bookmarker.IsBookmarked("https://www.example.com"));
            Assert.IsFalse(Bookmarker.IsBookmarked("https://www.google.com"));
        }

        [TestMethod]
        public void BookmarkExistenceAfterAddingAndRemovingTest() {

            Bookmarker.AddBookmark("https://www.example.com");
            Bookmarker.AddBookmark("https://www.google.com");
            Assert.IsTrue(Bookmarker.IsBookmarked("https://www.example.com"));
            Assert.IsTrue(Bookmarker.IsBookmarked("https://www.google.com"));

            Bookmarker.RemoveBookmark("https://www.example.com");
            Assert.IsFalse(Bookmarker.IsBookmarked("https://www.example.com"));
            Assert.IsTrue(Bookmarker.IsBookmarked("https://www.google.com"));
        }

        [TestMethod]
        public void BookmarkExistenceWhenCollectionEmptyTest() {
            Assert.IsFalse(Bookmarker.IsBookmarked("https://www.example.com"));
        }
        #endregion

        #region Saving and Loading Tests
        [TestMethod]
        public void SavingBookmarkCollectionTest() {

            Bookmarker.AddBookmark("https://www.example.com");
            Bookmarker.AddBookmark("https://www.google.com");
            Bookmarker.SaveBookmarks(SaveTestDirectory);

            Assert.IsTrue(File.Exists(SaveTestDirectory));

            string fileContent = File.ReadAllText(SaveTestDirectory);
            List<string> expected = new() {
                "https://www.example.com",
                "https://www.google.com"
            };

            string expectedContent = JsonSerializer.Serialize(expected);
            Assert.AreEqual(expectedContent, fileContent);

        }

        [TestMethod]
        public void LoadingBookmarkCollectionTest() {

            List<string> expected = new() {
                "https://www.example.com",
                "https://www.google.com"
            };

            string fileContent = JsonSerializer.Serialize(expected);
            File.WriteAllText(LoadTestDirectory, fileContent);

            Bookmarker.LoadBookmarks(LoadTestDirectory);
            CollectionAssert.AreEqual(expected, Bookmarker.GetBookmarks());
        }

        #endregion
    }
}
