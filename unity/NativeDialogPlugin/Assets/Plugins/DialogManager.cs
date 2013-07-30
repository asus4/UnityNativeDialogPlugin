using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Popup Native Dialog
/// </summary>
public class DialogManager : MonoBehaviour
{
	
	#region static Singleton
	static DialogManager _instance;
	
	public static DialogManager Instance {
		get {
			if (_instance == null) {
				_instance = new GameObject ("DialogManager").AddComponent<DialogManager> ();
				DontDestroyOnLoad (_instance.gameObject);
			}
			return _instance;
		}
	}
	#endregion
	
	#region members
	Dictionary<int, System.Action<bool>> _delegates;	
	#endregion
	
	#region Lyfecycles
	void Awake ()
	{
		_delegates = new Dictionary<int, Action<bool>> ();
		// set default label
		SetLabel("YES", "NO", "CLOSE");
	}
	
	void OnDestroy ()
	{
		if (_delegates != null) {
			_delegates.Clear ();
			_delegates = null;
		}
	}
	#endregion
	
	
	
	#if UNITY_EDITOR
	// only editor.. show dummy gui
	void OnGUI ()
	{
		EditorDialog.Draw ();
	}
	#elif UNITY_IPHONE
	[DllImport("__Internal")]
    private static extern int _showSelectDialog (string msg);
	[DllImport("__Internal")]
	private static extern int _showSelectTitleDialog (string title, string msg);
	[DllImport("__Internal")]
	private static extern int _showSubmitDialog (string msg);
	[DllImport("__Internal")]
	private static extern int _showSubmitTitleDialog (string title, string msg);
	[DllImport("__Internal")]
	private static extern void _dissmissDialog (int id);
	[DllImport("__Internal")]
	private static extern void _setLabel(string decide, string cancel, string close);
	#endif
	
	
	
	public int ShowSelectDialog (string msg, Action<bool> del)
	{
		int id;
		#if UNITY_EDITOR
		id = 0;
		#elif UNITY_ANDROID
		using (AndroidJavaClass cls = new AndroidJavaClass("unity.plugins.dialog.DialogManager")) {
            id = cls.CallStatic<int>("ShowSelectDialog", msg);
			_delegates.Add(id, del);
        }	
		#elif UNITY_IPHONE
			id = _showSelectDialog(msg);
			_delegates.Add(id, del);
		#endif
		return id;
	}
	
	public int ShowSelectDialog (string title, string msg, Action<bool> del)
	{
		int id;
		#if UNITY_EDITOR
		id = 0;
		#elif UNITY_ANDROID
		using (AndroidJavaClass cls = new AndroidJavaClass("unity.plugins.dialog.DialogManager")) {
            id = cls.CallStatic<int>("ShowSelectTitleDialog", title, msg);
			_delegates.Add(id, del);
        }	
		#elif UNITY_IPHONE
			id = _showSelectTitleDialog(title, msg);
			_delegates.Add(id, del);
		#endif
		return id;
	}
	
	public int ShowSubmitDialog (string msg, Action<bool> del)
	{
		int id;
		#if UNITY_EDITOR
		id = 0;
		#elif UNITY_ANDROID
		using (AndroidJavaClass cls = new AndroidJavaClass("unity.plugins.dialog.DialogManager")) {
            id = cls.CallStatic<int>("ShowSubmitDialog", msg);
			_delegates.Add(id, del);
        }
		#elif UNITY_IPHONE
			id = _showSubmitDialog(msg);
			_delegates.Add(id, del);
		#endif
		return id;
	}
	
	public int ShowSubmitDialog (string title, string msg, Action<bool> del)
	{
		int id;
		#if UNITY_EDITOR
		id = 0;
		#elif UNITY_ANDROID
		using (AndroidJavaClass cls = new AndroidJavaClass("unity.plugins.dialog.DialogManager")) {
            id = cls.CallStatic<int>("ShowSubmitTitleDialog", title, msg);
			_delegates.Add(id, del);
        }
		#elif UNITY_IPHONE
			id = _showSubmitTitleDialog(title, msg);
			_delegates.Add(id, del);
		#endif
		return id;
	}
	
	public void DissmissDialog(int id) {
		#if UNITY_EDITOR
		
		#elif UNITY_ANDROID
		using (AndroidJavaClass cls = new AndroidJavaClass("unity.plugins.dialog.DialogManager")) {
            cls.CallStatic("DissmissDialog", id);
        }
		#elif UNITY_IPHONE
			_dissmissDialog(id);
		#endif
		
		if(_delegates.ContainsKey(id)) {
			_delegates [id] (false);
			_delegates.Remove (id);
		} else {
			Debug.LogWarning ("undefined id:" + id);
		}
	}
	
	public void SetLabel(string decide, string cancel, string close) {
		
		#if UNITY_EDITOR
		
		#elif UNITY_ANDROID
		using (AndroidJavaClass cls = new AndroidJavaClass("unity.plugins.dialog.DialogManager")) {
            cls.CallStatic("SetLabel", decide, cancel, close);
        }
		#elif UNITY_IPHONE
			_setLabel(decide, cancel, close);
		#endif
		
	}
	
	#region Invoked from Native Plugin
	public void OnSubmit (string idStr)
	{
		int id = int.Parse (idStr);
		if (_delegates.ContainsKey (id)) {
			_delegates [id] (true);
			_delegates.Remove (id);
		} else {
			Debug.LogWarning ("undefined id:" + idStr);
		}
	}

	public void OnCancel (string idStr)
	{
		int id = int.Parse (idStr);
		if (_delegates.ContainsKey (id)) {
			_delegates [id] (false);
			_delegates.Remove (id);
		} else {
			Debug.LogWarning ("undefined id:" + idStr);
		}
	}
	#endregion
	
	#if UNITY_EDITOR
	// this is test class for editor
	class EditorDialog {
		static int _TOTAL_ID;
		
		Rect _windowRect;
		int _id;
		string msg;
		
		public static void Draw ()
		{
			
		}
		
		public void _Draw ()
		{
			int w = Screen.width;
			int h = Screen.height;
		
			if (_windowRect.width == 0) {
				_windowRect = new Rect (w * 0.1f, h / 2 - 300, w - w * 0.2f, 600);
			}
			_windowRect = GUILayout.Window (_TOTAL_ID, _windowRect, DoMyWindow, "My Window");
		}
		
		void DoMyWindow (int windowID)
		{
			GUILayout.Label ("lakdjfalskdjfalskdjfalk");
			GUILayout.BeginHorizontal ();
			
			if (GUILayout.Button ("NO")) {
				
			}
			if (GUILayout.Button ("YES")) {
				
			}
			GUILayout.EndHorizontal ();
		}
		
		static public int ShowSelectDialog ()
		{
			++_TOTAL_ID;
			return _TOTAL_ID;
		}
		
		static public int ShowSubmitDialog ()
		{
			++_TOTAL_ID;
			return _TOTAL_ID;
		}
		
	}
	#endif

}

