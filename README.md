# Unity Native Dialog Plugin

A lightweight Unity plugin for displaying native iOS and Android dialog boxes. Perfect for confirmation prompts, alerts, and user notifications with platform-specific styling.

## 🚀 Quick Start

### Basic Usage

```csharp
using NativeDialog;

// Show a simple OK/Cancel dialog
DialogManager.ShowSelect("Are you sure?", (bool result) => {
    Debug.Log($"User selected: {result}");
});

// Show a notification dialog with OK button only
DialogManager.ShowSubmit("Operation completed!", (bool result) => {
    Debug.Log("Dialog closed");
});
```

For more examples, see [NativeDialogSample.cs](Assets/NativeDialogSample.cs)

## 📱 Screenshots

### Android

https://github.com/user-attachments/assets/390e011a-7b3e-4128-8fd6-369c98a35054

### iOS

https://github.com/user-attachments/assets/4760a655-3fbf-4781-a084-6848f53da53c

### Editor Fallback

![Editor Fallback](https://github.com/user-attachments/assets/3fdb094d-397e-4af7-92e9-8ca75d323f50)

## Install via UPM

![package-from-git](https://github.com/user-attachments/assets/45562439-5c37-4940-afe5-a5fb59eb6849)

1. Open the Package Manager Window.
2. Click `+` and select "Add package from git URL".
3. Paste the following URL:  
`https://github.com/asus4/UnityNativeDialogPlugin.git?path=/Packages/com.github.asus4.nativedialog#v1.2.0`

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
