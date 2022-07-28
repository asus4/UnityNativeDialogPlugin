#if UNITY_IOS

using System.Runtime.InteropServices;

namespace NativeDialog
{
    internal sealed class DialogIOS : IDialog
    {
        public void Dispose()
        {
        }

        public void SetLabel(string decide, string cancel, string close)
        {
            _setLabel(decide, cancel, close);
        }

        public int ShowSelect(string message)
        {
            return _showSelectDialog(message);
        }

        public int ShowSelect(string title, string message)
        {
            return _showSelectTitleDialog(title, message);
        }

        public int ShowSubmit(string message)
        {
            return _showSubmitDialog(message);
        }

        public int ShowSubmit(string title, string message)
        {
            return _showSubmitTitleDialog(title, message);
        }

        public void Dissmiss(int id)
        {
            _dissmissDialog(id);
        }


        [DllImport("__Internal")]
        private static extern int _showSelectDialog(string msg);
        [DllImport("__Internal")]
        private static extern int _showSelectTitleDialog(string title, string msg);
        [DllImport("__Internal")]
        private static extern int _showSubmitDialog(string msg);
        [DllImport("__Internal")]
        private static extern int _showSubmitTitleDialog(string title, string msg);
        [DllImport("__Internal")]
        private static extern void _dissmissDialog(int id);
        [DllImport("__Internal")]
        private static extern void _setLabel(string decide, string cancel, string close);
    }
}

#endif // UNITY_IOS