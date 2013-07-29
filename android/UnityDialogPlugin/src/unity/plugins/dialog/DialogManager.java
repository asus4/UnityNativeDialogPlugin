package unity.plugins.dialog;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;

import com.unity3d.player.UnityPlayer;

/**
 * @author koki ibukuro
 *
 */
public class DialogManager {
	private static DialogManager _instance;
	
	private int _id = 0;
	
	/**
	 * singleton class 
	 */
	private DialogManager() {
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
		
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + _id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(_id));
					}
				};
				
				DialogInterface.OnClickListener negativeListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("defuse " + _id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnCancel", String.valueOf(_id));
					}
				};
				
				
				new AlertDialog.Builder(a)
				.setMessage(msg)
				.setNegativeButton("いいえ", negativeListener)
				.setPositiveButton("はい", positiveListener)
				.show();
			}
		});
		return _id;
	}
	
	/**
	 * @param title
	 * @param msg
	 * @return id
	 */
	public int showSelectDialog(final String title, final String msg) {
		++_id;
		
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + _id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(_id));
					}
				};
				
				DialogInterface.OnClickListener negativeListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("defuse " + _id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnCancel", String.valueOf(_id));
					}
				};
				
				new AlertDialog.Builder(a)
				.setTitle(title)
				.setMessage(msg)
				.setNegativeButton("いいえ", negativeListener)
				.setPositiveButton("はい", positiveListener)
				.show();
			}
		});
		
		return _id;
	}
	
	/**
	 * @param msg
	 * @return id
	 */
	public int showSubmitDialog(final String msg) {
		++_id;
		
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + _id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(_id));
					}
				};
				
				new AlertDialog.Builder(a)
				.setMessage(msg)
				.setPositiveButton("閉じる", positiveListener)
				.show();
			}
		});
		
		return _id;
	}
	
	/**
	 * @param title
	 * @param msg
	 * @return id
	 */
	public int showSubmitDialog(final String title, final String msg) {
		++_id;
		
		final Activity a = UnityPlayer.currentActivity;
		a.runOnUiThread(new Runnable() {
			
			public void run() {
				DialogInterface.OnClickListener positiveListener = new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						log("submit " + _id + ":" + msg);
						UnityPlayer.UnitySendMessage("DialogManager", "OnSubmit", String.valueOf(_id));
					}
				};
				
				new AlertDialog.Builder(a)
				.setTitle(title)
				.setMessage(msg)
				.setPositiveButton("閉じる", positiveListener)
				.show();
			
			}
		});
		
		return _id;
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
	
	private void log(String msg) {
		//Log.d("DialogsManager", msg);
	}
	
}
