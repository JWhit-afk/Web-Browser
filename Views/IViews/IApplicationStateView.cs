
namespace Web_Browser_CW1.Views {
    internal interface IApplicationStateView {

        event EventHandler<StateArgs> StateRequest;

        event EventHandler<ShortcutEventArgs> ShortcutPressed;
    }
}
