#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

namespace NativeDialog
{
    /// <summary>
    /// Mock implementation of dialogs for Unity Editor.
    /// </summary>
    internal sealed class DialogEditor : IDialog
    {
        private string decideLabel = "YES";
        private string cancelLabel = "NO";
        private string closeLabel = "CLOSE";
        private int currentId = 0;
        private IDialogReceiver receiver;
        private readonly Dictionary<int, bool> pendingDialogs = new Dictionary<int, bool>();

        public DialogEditor(IDialogReceiver receiver)
        {
            this.receiver = receiver;
        }

        public void Dispose()
        {
            pendingDialogs.Clear();
            receiver = null;
        }

        public void SetLabel(string decide, string cancel, string close)
        {
            decideLabel = decide;
            cancelLabel = cancel;
            closeLabel = close;
        }

        public int ShowSelect(string message)
        {
            int id = ++currentId;
            EditorApplication.delayCall += () => ShowSelectDialog(id, null, message);
            return id;
        }

        public int ShowSelect(string title, string message)
        {
            int id = ++currentId;
            EditorApplication.delayCall += () => ShowSelectDialog(id, title, message);
            return id;
        }

        public int ShowSubmit(string message)
        {
            int id = ++currentId;
            EditorApplication.delayCall += () => ShowSubmitDialog(id, null, message);
            return id;
        }

        public int ShowSubmit(string title, string message)
        {
            int id = ++currentId;
            EditorApplication.delayCall += () => ShowSubmitDialog(id, title, message);
            return id;
        }

        public void Dismiss(int id)
        {
            UnityEngine.Debug.LogWarning($"Dismiss is not supported in Editor mode. ID: {id}");
            if (pendingDialogs.ContainsKey(id))
            {
                pendingDialogs.Remove(id);
            }
        }

        private void ShowSelectDialog(int id, string title, string message)
        {
            if (!pendingDialogs.ContainsKey(id))
            {
                pendingDialogs[id] = true;
            }
            else if (!pendingDialogs[id])
            {
                return;
            }

            bool result;
            if (string.IsNullOrEmpty(title))
            {
                result = EditorUtility.DisplayDialog("", message, decideLabel, cancelLabel);
            }
            else
            {
                result = EditorUtility.DisplayDialog(title, message, decideLabel, cancelLabel);
            }

            if (pendingDialogs.ContainsKey(id))
            {
                pendingDialogs.Remove(id);
                if (result)
                {
                    receiver?.OnSubmit(id.ToString());
                }
                else
                {
                    receiver?.OnCancel(id.ToString());
                }
            }
        }

        private void ShowSubmitDialog(int id, string title, string message)
        {
            if (!pendingDialogs.ContainsKey(id))
            {
                pendingDialogs[id] = true;
            }
            else if (!pendingDialogs[id])
            {
                return;
            }

            if (string.IsNullOrEmpty(title))
            {
                EditorUtility.DisplayDialog("", message, closeLabel);
            }
            else
            {
                EditorUtility.DisplayDialog(title, message, closeLabel);
            }

            if (pendingDialogs.ContainsKey(id))
            {
                pendingDialogs.Remove(id);
                receiver?.OnSubmit(id.ToString());
            }
        }
    }
}
#endif
