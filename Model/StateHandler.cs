using System.Diagnostics;
using System.Text.Json;

namespace Web_Browser_CW1.Handlers {

/// <summary>
/// A catch all singleton for managing the state of the application.
/// Handles any state information that is not directly related to bookmarks or history, such as the homepage URL.
/// </summary>
public class StateHandler {

        private readonly string StateFilePath = AppConstants.DataFilePath + "/config.json";
        private const string DefaultHome = "https://www.google.co.uk";

    public string homePageUrl { get; set; }

    /// <summary>
    /// Initializes a new instance of the StateHandler class with the default home page URL.
    /// </summary>
    public StateHandler() { homePageUrl = DefaultHome; }

    public StateHandler(string StateFilePath) {
        this.StateFilePath = StateFilePath;
        homePageUrl = DefaultHome;
    }

    /// <summary>
    /// Initializes a new instance of the StateHandler class with a specified state file path and homepage URL.
    /// </summary>
    public StateHandler(string StateFilePath, string DefaultHome) {
        this.StateFilePath = StateFilePath;
        this.homePageUrl = DefaultHome;
    }

    /// <summary>
    /// Loads the application state from persistent storage.
    /// </summary>
    public void LoadState() {

        try {
            string json = File.ReadAllText(StateFilePath);
            StateHandler? previousState = JsonSerializer.Deserialize<StateHandler>(json);

            if (previousState != null) this.homePageUrl = previousState.homePageUrl;

        } catch (FileNotFoundException) {

            // File doesn't exist, create it.
            File.Create(StateFilePath).Close();
            
        } catch (JsonException) {

            // File does not contain json tokens or json is corrupted.
            File.Delete(StateFilePath);
            File.Create(StateFilePath).Close();
        }
    }

    /// <summary>
    /// Saves the state to persistent storage.
    /// </summary>
    public void SaveState() {
        Debug.WriteLine("Saving State...");
        string json = JsonSerializer.Serialize<StateHandler>(this);
        File.WriteAllText(StateFilePath, json);
        Debug.WriteLine("State Saved:");
        Debug.WriteLine(json);
    }
}
}
