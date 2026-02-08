using System.Text.Json;
using Web_Browser_CW1;
using Web_Browser_CW1.Handlers;
using static Web_Browser_CW1.Handlers.HistoryHandler;

namespace BrowserTests.ModelTests {

    [TestClass]
    public sealed class HistoryHandlerTests {

        //
        // Adding history tests
        //
        // 1. ✔️ Test that adding a history entry to a collection adds the entry successfully.
        //
        // Navigation tests
        //
        // 2. ✔️ Test that navigating to the previous page in the history returns the correct URL.
        // 3. ✔️ Test that navigating to the previous page in the history when there are no previous pages throws a HistoryOutOfBoundsException.
        // 4. ✔️ Test that navigating to the next page in the history returns the correct URL.
        // 5. ✔️ Test that navigating to the next page in the history when there are no next pages throws a HistoryOutOfBoundsException.
        // 6. ✔️ Test that navigating to the previous and next pages in the history updates the pointer correctly.
        // 7. ✔️ Test that finding a URl in the history returns the correct index.
        // 8. ✔️ Test that finding a URL that does not exist in the history returns -1.
        // 9. ✔️ Test that Setting the pointer to a valid index updates the pointer correctly.
        // 10. ✔️ Test that Setting the pointer to an invalid index throws a HistoryOutOfBoundsException.
        //
        // Saving and Loading tests
        //
        // 10. ✔️ Test that saving the history collection to a file creates the file and saves the correct data.
        // 11. ✔️ Test that loading the history collection from a file loads the correct data into the collection.

        private HistoryHandler HistoryHandler;

        private const string SaveTestDirectory = AppConstants.TestDataFilePath + "/SaveHistory.json";
        private const string LoadTestDirectory = AppConstants.TestDataFilePath + "/LoadHistory.json";

        #region Test Setup and Tear-down
        [TestInitialize]
        public void SetUp() {

            HistoryHandler = new();

            // Ensure test directory exist.
            Directory.CreateDirectory(AppConstants.TestDataFilePath);
        }

        [TestCleanup]
        public void TearDown() {

            // Delete files if they exist.
            if (File.Exists(SaveTestDirectory))
                File.Delete(SaveTestDirectory);

            if (File.Exists(LoadTestDirectory))
                File.Delete(LoadTestDirectory);
        }
        #endregion

        #region History Register Tests
        [TestMethod]
        public void HistoryRegisterTest() {

            HistoryHandler.Register("https://www.example.com");

            Assert.AreEqual("https://www.example.com", HistoryHandler.GetHistory().First());

            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.Register("https://www.amazon.com");

            List<string> expected = new() {
                "https://www.example.com",
                "https://www.google.com",
                "https://www.facebook.com",
                "https://www.amazon.com"
            };

            CollectionAssert.AreEqual(expected, HistoryHandler.GetHistory());
        }
        #endregion

        #region Navigation Tests
        [TestMethod]
        public void HistoryPreviousTest() {

            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.Register("https://www.amazon.com");

            Assert.AreEqual("https://www.facebook.com", HistoryHandler.previousPage());
        }

        [TestMethod]
        public void HistoryPreviousOutOfBoundsTest() {

            HistoryHandler.Register("https://www.google.com");
            Assert.Throws<HistoryOutOfBoundsException>(() => HistoryHandler.previousPage());

            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.previousPage();
            Assert.Throws<HistoryOutOfBoundsException>(() => HistoryHandler.previousPage());
        }

        [TestMethod]
        public void HistoryNextTest() {

            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.Register("https://www.amazon.com");
            HistoryHandler.previousPage();

            Assert.AreEqual("https://www.amazon.com", HistoryHandler.nextPage());
        }

        [TestMethod]
        public void HistoryNextOutOfBoundsTest() {

            HistoryHandler.Register("https://www.google.com");
            Assert.Throws<HistoryOutOfBoundsException>(() => HistoryHandler.nextPage());

            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.previousPage();
            HistoryHandler.nextPage();

            Assert.Throws<HistoryOutOfBoundsException>(() => HistoryHandler.nextPage());
        }

        public void HistoryPointerUpdateTest() {

            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.Register("https://www.amazon.com");

            Assert.AreEqual(2, HistoryHandler.GetPosition());

            HistoryHandler.previousPage();
            Assert.AreEqual(1, HistoryHandler.GetPosition());

            HistoryHandler.previousPage();
            Assert.AreEqual(0, HistoryHandler.GetPosition());

            HistoryHandler.nextPage();
            Assert.AreEqual(1, HistoryHandler.GetPosition());
        }

        public void HistorySearchTest() {

            // Test regular search functionality and search miss.
            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.Register("https://www.amazon.com");

            Assert.AreEqual(1, HistoryHandler.FindUrl("https://www.facebook.com"));
            Assert.AreEqual(-1, HistoryHandler.FindUrl("https://www.x.com"));

            // Test that searching for a URL returns the most recent index of the URL in the history.
            HistoryHandler.Register("https://www.google.com");
            Assert.AreEqual(3, HistoryHandler.FindUrl("https://www.google.com"));
        }

        public void HistoryPointerChangeTest() {

            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.Register("https://www.amazon.com");
            HistoryHandler.SetPosition(0);

            Assert.AreEqual("https://www.google.com", HistoryHandler.GetHistory()[HistoryHandler.GetPosition()]);

            HistoryHandler.SetPosition(1);
            Assert.AreEqual("https://wwww.facebook.com", HistoryHandler.GetHistory()[HistoryHandler.GetPosition()]);

            HistoryHandler.SetPosition(2);
            Assert.AreEqual("https://www.amazon.com", HistoryHandler.GetHistory()[HistoryHandler.GetPosition()]);
        }

        public void HistoryPointerOutOfBoundsTest() {

            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.Register("https://www.facebook.com");
            HistoryHandler.Register("https://www.amazon.com");

            Assert.Throws<HistoryOutOfBoundsException>(() => HistoryHandler.SetPosition(-1));
            Assert.Throws<HistoryOutOfBoundsException>(() => HistoryHandler.SetPosition(3));
        }
        #endregion

        #region Saving and Loading Tests
        [TestMethod]
        public void SavingHistoryCollectionTest() {

            HistoryHandler.Register("https://www.example.com");
            HistoryHandler.Register("https://www.google.com");
            HistoryHandler.SaveHistory(SaveTestDirectory);

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
        public void LoadingHistoryCollectionTest() {

            List<string> expected = new() {
                "https://www.example.com",
                "https://www.google.com"
            };

            string fileContent = JsonSerializer.Serialize(expected);
            File.WriteAllText(LoadTestDirectory, fileContent);

            HistoryHandler.LoadHistory(LoadTestDirectory);
            CollectionAssert.AreEqual(expected, HistoryHandler.GetHistory());
        }

        #endregion
    }
}
