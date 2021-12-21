using System;
using System.Collections.Generic;
using UnityEngine;

namespace NativeDialog
{
    /// <summary>
    /// Popup Native Dialog
    /// </summary>
    public sealed class DialogManager : MonoBehaviour, IDialogReceiver
    {

        #region Singleton
        private static DialogManager instance;
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

        #region Lyfecycles
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
            var mock = gameObject.AddComponent<DialogMock>();
            mock.Initialize(this, true);
            return mock;
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

        public static void SetLabel(string decide, string cancel, string close)
        {
            Instance.dialog.SetLabel(decide, cancel, close);
        }

        public static int ShowSelect(string message, Action<bool> callback)
        {
            int id = Instance.dialog.ShowSelect(message);
            Instance.callbacks.Add(id, callback);
            return id;
        }

        public static int ShowSelect(string title, string message, Action<bool> callback)
        {
            int id = Instance.dialog.ShowSelect(title, message);
            Instance.callbacks.Add(id, callback);
            return id;
        }

        public static int ShowSubmit(string message, Action<bool> callback)
        {
            int id = Instance.dialog.ShowSubmit(message);
            Instance.callbacks.Add(id, callback);
            return id;
        }

        public static int ShowSubmit(string title, string message, Action<bool> del)
        {
            int id = Instance.dialog.ShowSubmit(title, message);
            Instance.callbacks.Add(id, del);
            return id;
        }

        public static void Dissmiss(int id)
        {
            Instance.dialog.Dissmiss(id);

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
