using UnityEngine;
using System.Collections;
using NativeDialog;

public class NativeDialogSample : MonoBehaviour
{
    [SerializeField] private string decideLabel = "Decide";
    [SerializeField] private string cancelLabel = "Cancel";
    [SerializeField] private string closeLabel = "Close";

    private void Start()
    {
        DialogManager.SetLabel(decideLabel, cancelLabel, closeLabel);
    }

    #region Invoked from Unity GUI

    public void ShowSelectDialog()
    {
        const string message = "A simple select dialog";
        DialogManager.ShowSelect(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
    }

    public void ShowSelectDialogWithTitle()
    {
        const string title = "A title";
        const string message = "A message for select dialog";
        DialogManager.ShowSelect(title, message, (bool result) =>
        {
            Debug.Log($"{result}: {title} / {message}");
        });
    }

    public void ShowSubmitDialog()
    {
        const string message = "A simple submit dialog";
        DialogManager.ShowSubmit(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
    }

    public void ShowSubmitDialogWithTitle()
    {
        const string title = "A title";
        const string message = "A message for submit dialog";
        DialogManager.ShowSubmit(title, message, (bool result) =>
        {
            Debug.Log($"{result}: {title} / {message}");
        });
    }

    public void ShowDialogWithAutoDismiss()
    {
        const string message = "A dialog with auto dismiss";
        int id = DialogManager.ShowSelect(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
        StartCoroutine(Dismiss(id, 3f));
    }

    #endregion // Invoked from Unity GUI

    private IEnumerator Dismiss(int id, float time)
    {
        yield return new WaitForSeconds(time);
        DialogManager.Dismiss(id);
    }
}
