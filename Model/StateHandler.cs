using System.Diagnostics;
using System.Text.Json;

namespace Web_Browser_CW1.Handlers {

    internal class StateHandler {

        private const string StateFilePath = AppConstants.DataFilePath + "/config.json";
        private const string DefaultHome = "https://www.google.co.uk";

        public string homePageUrl { get; set; }

        public StateHandler() { homePageUrl = DefaultHome; }

        public void LoadState() {

            try {
                string json = File.ReadAllText(StateFilePath);
                StateHandler? previousState = JsonSerializer.Deserialize<StateHandler>(json);

                if (previousState != null) this.homePageUrl = previousState.homePageUrl;

            } catch (FileNotFoundException) {

                // File doesn't exist, create it.
                File.Create(StateFilePath).Close();
            
            } catch (JsonException) {

                // FIle does not contian json tokens or json is corrupted.
                File.Delete(StateFilePath);
                File.Create(StateFilePath).Close();
            }
        }

        public void SaveState() {
            Debug.WriteLine("Saving State...");
            string json = JsonSerializer.Serialize<StateHandler>(this);
            File.WriteAllText(StateFilePath, json);
            Debug.WriteLine("State Saved:");
            Debug.WriteLine(json);
        }
    }
}
