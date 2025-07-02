namespace NativeDialog
{
    /// <summary>
    /// Interface for platform-specific dialog implementations.
    /// Defines methods for showing native dialogs across different platforms.
    /// </summary>
    public interface IDialog : System.IDisposable
    {
        void SetLabel(string decide, string cancel, string close);
        int ShowSelect(string message);
        int ShowSelect(string title, string message);
        int ShowSubmit(string message);
        int ShowSubmit(string title, string message);
        void Dismiss(int id);
    }
}
