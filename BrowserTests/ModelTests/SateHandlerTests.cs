using System.Text.Json;
using Web_Browser_CW1;
using Web_Browser_CW1.Handlers;

namespace BrowserTests.ModelTests {

    [TestClass, DoNotParallelize]
    public sealed class SateHandlerTests {

        //
        // Saving and Loading tests
        //
        // 1. ✔️ Test that saving the state to a file creates the file and saves the correct data.
        // 2. ✔️ Test that loading the state from a file loads the correct data into the collection.
        //
        // State Defaults tests
        //
        // 3. Test that the default state is set correctly when no file exists.

        private StateHandler StateHandler;

        private static readonly string SaveTestDirectory = AppConstants.TestDataFilePath + "/SaveState.json";
        private static readonly string LoadTestDirectory = AppConstants.TestDataFilePath + "/LoadState.json";

        public SateHandlerTests() {
            StateHandler = new();
        }

        #region Test Setup and Tear-down
        [TestInitialize]
        public void SetUp() {
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

        #region Saving and Loading Tests
        [TestMethod]
        public void StateHandlerSaveTest() {

            // Create a state handler to the test directory and set homepage URL.
            StateHandler = new(SaveTestDirectory, "https://www.example.com");

            // Save the state to the file.
            StateHandler.SaveState();

            // Assert that the file was created and contains the correct data.
            Assert.IsTrue(File.Exists(SaveTestDirectory));
            string json = File.ReadAllText(SaveTestDirectory);
            StateHandler? savedState = JsonSerializer.Deserialize<StateHandler>(json);
            Assert.IsNotNull(savedState);
            Assert.AreEqual("https://www.example.com", savedState.homePageUrl);

        }

        [TestMethod]
        public void StateHandlerLoadTest() {

            // Write test JSON string to the test file.
            File.WriteAllText(LoadTestDirectory, "{ \"homePageUrl\": \"https://www.example.com\" }");

            // Create a state handler and load the state from the file. 
            StateHandler = new(LoadTestDirectory);
            StateHandler.LoadState();

            // Assert that the homepage URL was loaded correctly.
            Assert.AreEqual("https://www.example.com", StateHandler.homePageUrl);
        }
        #endregion

        #region Test Defaults
        [TestMethod]
        public void StateHandlerDefaultTest() {

            // Assert that the homepage URL is set to the default value.
            Assert.AreEqual("https://www.google.co.uk", StateHandler.homePageUrl);
        }
        #endregion
    }
}
