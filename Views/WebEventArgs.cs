//
// Defines custom event argument classes
//
namespace Web_Browser_CW1.Views {

    /// <summary>
    /// Provides event data for when a is new URL being requested from a drop-down.
    /// </summary>
    public class SelectedUrlArgs : EventArgs {
        public required string url;
    }

    /// <summary>
    /// Provides event data for state-related operations, including the requested action.
    /// </summary>
    /// <remarks>The class is used to pass details about state requests, such as loading or saving state, to
    /// event handlers. The <see cref="Requests"/> enumeration specifies the type of operation being requested.</remarks>
    public class StateArgs : EventArgs {

        // Enum of possible requests to make to the state handler.
        public enum Requests {
            homePageLoad,
            homePageSet,
            save,
            load
        }

        public string homepage = string.Empty;

        public required Requests request;
    }

    /// <summary>
    /// Provides event data for shortcut events, including the keys and a callback for handling confirmation.
    /// </summary>
    public class ShortcutEventArgs : EventArgs {
        public Keys Keys { get; }
        public bool Handled { get; set; }

        public ShortcutEventArgs(Keys keys) {
            Keys = keys;
        }
    }
}
