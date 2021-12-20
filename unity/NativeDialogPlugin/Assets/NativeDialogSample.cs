using UnityEngine;
using System.Collections;

public class NativeDialogSample : MonoBehaviour
{
    [SerializeField] private string decideLabel = "Decide";
    [SerializeField] private string cancelLabel = "Cancel";
    [SerializeField] private string closeLabel = "Close";

    private void Start()
    {
        DialogManager.Instance.SetLabel(decideLabel, cancelLabel, closeLabel);
    }

    #region Invoked from Unity GUI

    public void ShowSelectDialog()
    {
        const string message = "A simple select dialog";
        DialogManager.Instance.ShowSelectDialog(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
    }

    public void ShowSelectDialogWithTitle()
    {
        const string title = "A title";
        const string message = "A message for select dialog";
        DialogManager.Instance.ShowSelectDialog(title, message, (bool result) =>
        {
            Debug.Log($"{result}: {title} / {message}");
        });
    }

    public void ShowSubmitDialog()
    {
        const string message = "A simple submit dialog";
        DialogManager.Instance.ShowSubmitDialog(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
    }

    public void ShowSubmitDialogWithTitle()
    {
        const string title = "A title";
        const string message = "A message for submit dialog";
        DialogManager.Instance.ShowSubmitDialog(title, message, (bool result) =>
        {
            Debug.Log($"{result}: {title} / {message}");
        });
    }

    public void ShowDialogWithAutoDissmiss()
    {
        const string message = "A dialog with auto dismiss";
        int id = DialogManager.Instance.ShowSelectDialog(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
        StartCoroutine(Dissmiss(id, 3f));
    }

    #endregion // Invoked from Unity GUI

    private IEnumerator Dissmiss(int id, float time)
    {
        yield return new WaitForSeconds(time);
        DialogManager.Instance.DissmissDialog(id);
    }
}
