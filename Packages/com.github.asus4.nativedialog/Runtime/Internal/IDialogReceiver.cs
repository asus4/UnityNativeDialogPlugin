namespace NativeDialog
{
    internal interface IDialogReceiver
    {
        void OnSubmit(string idStr);
        void OnCancel(string idStr);
    }
}
