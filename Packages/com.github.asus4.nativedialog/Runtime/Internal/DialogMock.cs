using UnityEngine;

namespace NativeDialog
{
    public sealed class DialogMock : IDialog
    {
        private int id = 0;

        public void Dispose()
        {
        }

        public void SetLabel(string decide, string cancel, string close)
        {
            Debug.Log($"SetLabel: {decide}, {cancel}, {close}");
        }

        public int ShowSelect(string message)
        {
            Debug.Log($"ShowSelect: {message}");
            return ++id;
        }

        public int ShowSelect(string title, string message)
        {
            Debug.Log($"ShowSelect: {title}, {message}");
            return ++id;
        }

        public int ShowSubmit(string message)
        {
            Debug.Log($"ShowSubmit: {message}");
            return ++id;
        }

        public int ShowSubmit(string title, string message)
        {
            Debug.Log($"ShowSubmit: {title}, {message}");
            return ++id;
        }

        public void Dissmiss(int id)
        {
            Debug.Log($"Dissmiss: {id}");
        }
    }
}
