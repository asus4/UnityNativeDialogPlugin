using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	void OnGUI() {
		if(GUILayout.Button("aaa", GUILayout.MinWidth(200), GUILayout.MinHeight(100))) {
			DialogManager.Instance.ShowSelectDialog("aaa", (bool result) => {
				Debug.Log("aaa"+result);
			});
		}
		if(GUILayout.Button("bbb", GUILayout.MinWidth(200), GUILayout.MinHeight(100))) {
			DialogManager.Instance.ShowSelectDialog("b title" ,"bbb", (bool result) => {
				Debug.Log("bbb"+result);
			});
		}
		if(GUILayout.Button("ccc", GUILayout.MinWidth(200), GUILayout.MinHeight(100))) {
			DialogManager.Instance.ShowSubmitDialog("ccc", (bool result) => {
				Debug.Log ("ccc");
			});
		}
		if(GUILayout.Button("ddd", GUILayout.MinWidth(200), GUILayout.MinHeight(100))) {
			DialogManager.Instance.ShowSubmitDialog("d title", "ddd", (bool result) => {
				Debug.Log ("ddd");
			});
		}
		if(GUILayout.Button("eee auto dissmiss", GUILayout.MinWidth(200), GUILayout.MinHeight(100))) {
			int id = DialogManager.Instance.ShowSelectDialog("eee", (bool result) => {
				Debug.Log("eee"+result);
			});
			StartCoroutine(dissmiss(id, 3f));
		}
	}
	
	IEnumerator dissmiss (int id, float time) {
		yield return new WaitForSeconds (time);
		DialogManager.Instance.DissmissDialog (id);
	}
}
