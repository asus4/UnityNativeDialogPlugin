using System;
using System.Collections.Generic;
using UnityEngine;

namespace NativeDialog
{
    /// <summary>
    /// Manages Native Dialog popups across different platforms.
    /// Provides a unified interface for showing native select and submit dialogs on iOS, Android, and Unity Editor.
    /// </summary>
    public sealed class DialogManager : MonoBehaviour, IDialogReceiver
    {

        #region Singleton
        private static DialogManager instance;
        
        /// <summary>
        /// Gets the singleton instance of DialogManager.
        /// Creates a new instance if one doesn't exist.
        /// </summary>
        public static DialogManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find if there is already DialogManager in the scene
                    instance = FindObjectOfType<DialogManager>();
                    if (instance == null)
                    {
                        instance = new GameObject("DialogManager").AddComponent<DialogManager>();
                    }
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
        #endregion

        #region Members
        private Dictionary<int, Action<bool>> callbacks;

        private IDialog dialog;
        #endregion

        #region Life cycles
        private void Awake()
        {
            if (instance == null)
            {
                // If I am the first instance, make me the Singleton
                instance = this;
                DontDestroyOnLoad(this);

                callbacks = new Dictionary<int, Action<bool>>();
                dialog = CreateDialog();

                // Set default label
                SetLabel("YES", "NO", "CLOSE");
            }
            else
            {
                // If s singleton already exists and you find
                // another reference in scene, destroy it!
                if (this != instance)
                {
                    Destroy(gameObject);
                }
            }
        }

        private IDialog CreateDialog()
        {
#if UNITY_EDITOR
            return new DialogEditor(this);
#elif UNITY_ANDROID
            return new DialogAndroid();
#elif UNITY_IOS
            return new DialogIOS();
#else
            Debug.LogWarning($"{Application.platform} is not supported.");
            var mock = gameObject.AddComponent<DialogMock>();
            mock.Initialize(this, true);
            return mock;
#endif
        }

        private void OnDestroy()
        {
            if (callbacks != null)
            {
                callbacks.Clear();
                callbacks = null;
            }

            dialog.Dispose();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets or sets the custom dialog implementation.
        /// </summary>
        public IDialog CustomDialog
        {
            get { return dialog; }
            set { dialog = value; }
        }

        /// <summary>
        /// Sets the button labels for all future dialogs.
        /// </summary>
        /// <param name="decide">Label for the positive/confirm button</param>
        /// <param name="cancel">Label for the negative/cancel button</param>
        /// <param name="close">Label for the close button in submit dialogs</param>
        public static void SetLabel(string decide, string cancel, string close)
        {
            Instance.dialog.SetLabel(decide, cancel, close);
        }

        /// <summary>
        /// Shows a selection dialog with OK/Cancel buttons.
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="callback">Callback invoked with true for OK, false for Cancel</param>
        /// <returns>Dialog ID that can be used to dismiss the dialog</returns>
        public static int ShowSelect(string message, Action<bool> callback)
        {
            int id = Instance.dialog.ShowSelect(message);
            Instance.callbacks.Add(id, callback);
            return id;
        }

        /// <summary>
        /// Shows a selection dialog with title and OK/Cancel buttons.
        /// </summary>
        /// <param name="title">The dialog title</param>
        /// <param name="message">The message to display</param>
        /// <param name="callback">Callback invoked with true for OK, false for Cancel</param>
        /// <returns>Dialog ID that can be used to dismiss the dialog</returns>
        public static int ShowSelect(string title, string message, Action<bool> callback)
        {
            int id = Instance.dialog.ShowSelect(title, message);
            Instance.callbacks.Add(id, callback);
            return id;
        }

        /// <summary>
        /// Shows a submit dialog with only an OK button.
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="callback">Callback invoked when the dialog is closed</param>
        /// <returns>Dialog ID that can be used to dismiss the dialog</returns>
        public static int ShowSubmit(string message, Action<bool> callback)
        {
            int id = Instance.dialog.ShowSubmit(message);
            Instance.callbacks.Add(id, callback);
            return id;
        }

        /// <summary>
        /// Shows a submit dialog with title and only an OK button.
        /// </summary>
        /// <param name="title">The dialog title</param>
        /// <param name="message">The message to display</param>
        /// <param name="callback">Callback invoked when the dialog is closed</param>
        /// <returns>Dialog ID that can be used to dismiss the dialog</returns>
        public static int ShowSubmit(string title, string message, Action<bool> callback)
        {
            int id = Instance.dialog.ShowSubmit(title, message);
            Instance.callbacks.Add(id, callback);
            return id;
        }

        /// <summary>
        /// Programmatically dismisses a dialog.
        /// Invokes the callback with false (cancelled).
        /// </summary>
        /// <param name="id">The ID of the dialog to dismiss</param>
        public static void Dismiss(int id)
        {
            Instance.dialog.Dismiss(id);

            var callbacks = Instance.callbacks;
            if (callbacks.ContainsKey(id))
            {
                Instance.callbacks[id](false);
                callbacks.Remove(id);
            }
            else
            {
                Debug.LogWarning("undefined id:" + id);
            }
        }

        #endregion // Public Methods

        #region Invoked from Native Plugin
        public void OnSubmit(string idStr)
        {
            int id = int.Parse(idStr);
            if (callbacks.ContainsKey(id))
            {
                callbacks[id](true);
                callbacks.Remove(id);
            }
            else
            {
                Debug.LogWarning("Undefined id:" + idStr);
            }
        }

        public void OnCancel(string idStr)
        {
            int id = int.Parse(idStr);
            if (callbacks.ContainsKey(id))
            {
                callbacks[id](false);
                callbacks.Remove(id);
            }
            else
            {
                Debug.LogWarning("Undefined id:" + idStr);
            }
        }
        #endregion
    }
}
