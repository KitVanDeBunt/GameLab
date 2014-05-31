using UnityEngine;
using System.Collections;
using UnityEditor;

//public class GuiEditorTest : MonoBehaviour
[CustomEditor(typeof(Main))]
public class MainEditor : Editor {
	public override void OnInspectorGUI() {
		Main main = (Main) target;
		EditorGUI.BeginChangeCheck();
		main.gui.currentActive = EditorGUILayout.IntSlider(
			"Test", main.gui.currentActive, 0, main.gui.guiData.Length-1);
		
		//main.guiCamera = (Camera)EditorGUILayout.ObjectField(
		//	"Camera", main.camera, typeof(Camera),true);
		
		serializedObject.Update();
		//EditorGUIUtility.LookLikeInspector();	
		SerializedProperty gui = serializedObject.FindProperty ("gui");
		
		EditorGUILayout.PropertyField(gui, true);
		if(EditorGUI.EndChangeCheck()){
			serializedObject.ApplyModifiedProperties();
			main.gui.BuildGui();
		}
		//EditorGUIUtility.LookLikeControls();
	}
}

