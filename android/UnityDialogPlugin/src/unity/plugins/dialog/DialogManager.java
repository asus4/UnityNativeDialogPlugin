package unity.plugins.dialog;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.util.SparseArray;

import com.unity3d.player.UnityPlayer;

/**
 * @author koki ibukuro
 *
 */
public class DialogManager {
	private static DialogManager _instance;
	
	private int _id;
	private SparseArray<AlertDialog> _dialogs;
	
	/**
	 * singleton class 
	 */
	private DialogManager() {
		_id = 0;
		_dialogs = new SparseArray<AlertDialog>();
	}
	
	public static DialogManager getInstance() {
		if(_instance == null) {
			_instance = new DialogManager();
		}
		return _instance;
	}
	
	/**
	 * @param msg
	 * @return id
	 */
	public int showSelectDialog(final String msg) {
		++_id;
		
		final int id = _id; 
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(id));
						_dialogs.delete(id);
					}
				};
				
				DialogInterface.OnClickListener negativeListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("defuse " + id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnCancel", String.valueOf(id));
						_dialogs.delete(id);
					}
				};
				
				AlertDialog dialog = new AlertDialog.Builder(a)
				.setMessage(msg)
				.setNegativeButton("いいえ", negativeListener)
				.setPositiveButton("はい", positiveListener)
				.show();
				
				_dialogs.put(Integer.valueOf(id), dialog);
			}
		});
		return id;
	}
	
	/**
	 * @param title
	 * @param msg
	 * @return id
	 */
	public int showSelectDialog(final String title, final String msg) {
		++_id;
		
		final int id = _id;
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(id));
						_dialogs.delete(id);
					}
				};
				
				DialogInterface.OnClickListener negativeListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("defuse " + id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnCancel", String.valueOf(id));
						_dialogs.delete(id);
					}
				};
				
				AlertDialog dialog = new AlertDialog.Builder(a)
				.setTitle(title)
				.setMessage(msg)
				.setNegativeButton("いいえ", negativeListener)
				.setPositiveButton("はい", positiveListener)
				.show();
				
				_dialogs.put(Integer.valueOf(id), dialog);
			}
		});
		
		return id;
	}
	
	/**
	 * @param msg
	 * @return id
	 */
	public int showSubmitDialog(final String msg) {
		++_id;
		
		final int id = _id;
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(id));
						_dialogs.remove(id);
					}
				};
				
				AlertDialog dialog = new AlertDialog.Builder(a)
				.setMessage(msg)
				.setPositiveButton("閉じる", positiveListener)
				.show();
				
				_dialogs.put(Integer.valueOf(id), dialog);
			}
		});
		
		return id;
	}
	
	/**
	 * @param title
	 * @param msg
	 * @return id
	 */
	public int showSubmitDialog(final String title, final String msg) {
		++_id;
		
		final int id = _id;
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(id));
						_dialogs.remove(id);
					}
				};
				
				AlertDialog dialog = new AlertDialog.Builder(a)
				.setTitle(title)
				.setMessage(msg)
				.setPositiveButton("閉じる", positiveListener)
				.show();
				
				_dialogs.put(Integer.valueOf(id), dialog);
			}
		});
		
		return id;
	}
	
	public void dissmissDialog(int id) {
		AlertDialog dialog = _dialogs.get(id);
		if(dialog == null) {
			return;
		}
		dialog.dismiss();
		_dialogs.remove(id);
	}
	
	/**
	 * for unity static
	 * @param msg
	 * @return id
	 */
	public static int ShowSelectDialog(String msg) {
		return DialogManager.getInstance().showSelectDialog(msg);
	}
	
	/**
	 * for unity static
	 * @param title
	 * @param msg
	 * @return id
	 */
	public static int ShowSelectTitleDialog(String title, String msg) {
		return DialogManager.getInstance().showSelectDialog(title, msg);
	}
	
	/**
	 * for unity static
	 * @param msg
	 * @return id
	 */
	public static int ShowSubmitDialog(String msg) {
		return DialogManager.getInstance().showSubmitDialog(msg);
	}
	
	/**
	 * for unity static
	 * @param title
	 * @param msg
	 * @return id
	 */
	public static int ShowSubmitTitleDialog(String title, String msg) {
		return DialogManager.getInstance().showSubmitDialog(title, msg);
	}
	
	public static void DissmissDialog(int id) {
		DialogManager.getInstance().dissmissDialog(id);
	}
	
	private void log(String msg) {
		//Log.d("DialogsManager", msg);
	}
	
}
