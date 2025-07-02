using System.Collections;
using UnityEngine;

namespace NativeDialog
{
    internal sealed class DialogMock : MonoBehaviour, IDialog
    {
        [SerializeField]
        private float mockCallbackDelay = 1.0f;

        [SerializeField]
        private bool mockResult = true;

        private int id = 0;
        private IDialogReceiver receiver;


        public void Initialize(IDialogReceiver receiver, bool mockResult)
        {
            this.receiver = receiver;
            this.mockResult = mockResult;
        }

        public void Dispose()
        {
            receiver = null;
        }

        public void SetLabel(string decide, string cancel, string close)
        {
            Debug.Log($"SetLabel: {decide}, {cancel}, {close}");
        }

        public int ShowSelect(string message)
        {
            int newID = ++id;
            Debug.Log($"{newID}: ShowSelect: {message}");
            ExecuteMockCallback(newID);
            return newID;
        }

        public int ShowSelect(string title, string message)
        {
            int newID = ++id;
            Debug.Log($"{newID}: ShowSelect: {title}, {message}");
            ExecuteMockCallback(newID);
            return newID;
        }

        public int ShowSubmit(string message)
        {
            int newID = ++id;
            Debug.Log($"{newID}: ShowSubmit: {message}");
            ExecuteMockCallback(newID);
            return newID;
        }

        public int ShowSubmit(string title, string message)
        {
            int newID = ++id;
            Debug.Log($"{newID}: ShowSubmit: {title}, {message}");
            ExecuteMockCallback(newID);
            return newID;
        }

        public void Dismiss(int id)
        {
            Debug.Log($"Dismiss: {id}");
        }

        private void ExecuteMockCallback(int id)
        {
            StartCoroutine(MockCallback(id));
        }

        private IEnumerator MockCallback(int id)
        {
            yield return new WaitForSeconds(mockCallbackDelay);
            if (mockResult)
            {
                receiver.OnSubmit(id.ToString());
            }
        }
    }
}
