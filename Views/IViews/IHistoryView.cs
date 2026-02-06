
namespace Web_Browser_CW1.Views {

    internal interface IHistoryView {

        event EventHandler<SelectedUrlArgs> HistoryDropDownClick;

        void UpdateHistoryDropDown(List<string> items);
    }
}
