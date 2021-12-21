#if UNITY_ANDROID

using UnityEngine;

namespace NativeDialog
{
    public sealed class DialogAndroid : IDialog
    {
        private readonly AndroidJavaClass cls;

        public DialogAndroid()
        {
            cls = new AndroidJavaClass("unity.plugins.dialog.DialogManager");
        }

        public void Dispose()
        {
            cls.Dispose();
        }

        public void SetLabel(string decide, string cancel, string close)
        {
            cls.CallStatic("SetLabel", decide, cancel, close);
        }

        public int ShowSelect(string message)
        {
            return cls.CallStatic<int>("ShowSelectDialog", message);
        }

        public int ShowSelect(string title, string message)
        {
            return cls.CallStatic<int>("ShowSelectTitleDialog", title, message);
        }

        public int ShowSubmit(string message)
        {
            return cls.CallStatic<int>("ShowSubmitDialog", message);
        }

        public int ShowSubmit(string title, string message)
        {
            return cls.CallStatic<int>("ShowSubmitTitleDialog", title, message);
        }

        public void Dissmiss(int id)
        {
            cls.CallStatic("DissmissDialog", id);
        }
    }
}

#endif // UNITY_ANDROID
