using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
#endif

public enum InteractionType {NPC, Item, ShopNPC};

public class Interactable : MonoBehaviour
{
    public InteractionType InteractableObjectType;
    string NPCName;
    [HideInInspector]
    public List<Dialogue> dialogues;
    bool showDialogues;


#region Editor
  #if UNITY_EDITOR
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Interactable interact = (Interactable)target;
            if(interact.InteractableObjectType == InteractionType.NPC){
              DrawDetails(interact);
            }

            interact.showDialogues = EditorGUILayout.Foldout(interact.showDialogues, "Dialogues", true);

            if(interact.showDialogues){

              List<Dialogue> dialogueList = interact.dialogues;
              int size = Mathf.Max(0, EditorGUILayout.IntField("Size", dialogueList.Count)); 

              while(size > dialogueList.Count){
                dialogueList.Add(null);
              }

              while(size < dialogueList.Count){
                dialogueList.RemoveAt(dialogueList.Count - 1);
              }

              for (int i = 0; i < dialogueList.Count; i++)
              {
                EditorGUI.indentLevel++;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Dialogue {i}", GUILayout.MaxWidth(352));
                dialogueList[i].speach = EditorGUILayout.TextArea(dialogueList[i].speach, GUILayout.MaxHeight(100));
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel++;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Dialogue Is Promp", GUILayout.MaxWidth(200));
                dialogueList[i].isPrompt = EditorGUILayout.Toggle(dialogueList[i].isPrompt);

                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
              }
            }
        }

        private void DrawDetails(Interactable interact)
        {
            
            EditorGUILayout.LabelField("NPC Options");
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("NPC Name", GUILayout.MaxWidth(367));
            interact.NPCName = EditorGUILayout.TextField(interact.NPCName, GUILayout.MaxWidth(200));
            EditorGUILayout.EndHorizontal();




            EditorGUILayout.EndVertical();
        }
    }
  #endif
#endregion

}