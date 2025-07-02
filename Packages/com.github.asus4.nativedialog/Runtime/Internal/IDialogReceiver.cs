namespace NativeDialog
{
    /// <summary>
    /// Interface for receiving callbacks from native dialog implementations.
    /// Handles user interactions with dialog buttons.
    /// </summary>
    internal interface IDialogReceiver
    {
        void OnSubmit(string idStr);
        void OnCancel(string idStr);
    }
}
