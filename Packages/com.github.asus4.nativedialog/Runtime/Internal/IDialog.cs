namespace NativeDialog
{
    internal interface IDialog : System.IDisposable
    {
        void SetLabel(string decide, string cancel, string close);
        int ShowSelect(string message);
        int ShowSelect(string title, string message);
        int ShowSubmit(string message);
        int ShowSubmit(string title, string message);
        void Dissmiss(int id);
    }
}
