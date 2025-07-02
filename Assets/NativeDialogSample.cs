using UnityEngine;
using System.Collections;
using NativeDialog;

/// <summary>
/// Sample component demonstrating the usage of native dialog functionality.
/// Provides various examples of showing select and submit dialogs with different configurations.
/// </summary>
public class NativeDialogSample : MonoBehaviour
{
    /// <summary>
    /// Label text for the positive/confirm button in dialogs.
    /// </summary>
    [SerializeField] private string decideLabel = "Decide";
    
    /// <summary>
    /// Label text for the negative/reject button in dialogs.
    /// </summary>
    [SerializeField] private string cancelLabel = "Cancel";
    
    /// <summary>
    /// Label text for the close button in submit-only dialogs.
    /// </summary>
    [SerializeField] private string closeLabel = "Close";

    /// <summary>
    /// Initializes the dialog labels when the component starts.
    /// </summary>
    private void Start()
    {
        DialogManager.SetLabel(decideLabel, cancelLabel, closeLabel);
    }

    #region Invoked from Unity GUI

    /// <summary>
    /// Shows a simple selection dialog with OK/Cancel buttons.
    /// Logs the user's choice to the console.
    /// </summary>
    public void ShowSelectDialog()
    {
        const string message = "A simple select dialog";
        DialogManager.ShowSelect(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
    }

    /// <summary>
    /// Shows a selection dialog with both title and message.
    /// Useful for providing more context to the user.
    /// </summary>
    public void ShowSelectDialogWithTitle()
    {
        const string title = "A title";
        const string message = "A message for select dialog";
        DialogManager.ShowSelect(title, message, (bool result) =>
        {
            Debug.Log($"{result}: {title} / {message}");
        });
    }

    /// <summary>
    /// Shows a submit-only dialog with a single OK button.
    /// Used for notifications or acknowledgments.
    /// </summary>
    public void ShowSubmitDialog()
    {
        const string message = "A simple submit dialog";
        DialogManager.ShowSubmit(message, (bool result) =>
        {
            Debug.Log($"{result}: {message}");
        });
    }

    /// <summary>
    /// Shows a submit dialog with both title and message.
    /// Provides a more detailed notification to the user.
    /// </summary>
    public void ShowSubmitDialogWithTitle()
    {
        const string title = "A title";
        const string message = "A message for submit dialog";
        DialogManager.ShowSubmit(title, message, (bool result) =>
        {
            Debug.Log($"{result}: {title} / {message}");
        });
    }

    /// <summary>
    /// Shows a dialog that automatically dismisses after 3 seconds.
    /// Demonstrates how to programmatically close dialogs.
    /// </summary>
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
