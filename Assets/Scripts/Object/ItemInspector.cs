using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Item))]
public class ItemInspector : Editor
{
    SerializedProperty itemType;
    SerializedProperty hpValue;
    SerializedProperty speedValue;
    SerializedProperty scoreValue;
    SerializedProperty duration;
    SerializedProperty magneticPower;
    SerializedProperty magneticDuration;
    SerializedProperty magneticObject;

    private void OnEnable()
    {
        itemType = serializedObject.FindProperty("itemType");
        hpValue = serializedObject.FindProperty("hpValue");
        speedValue = serializedObject.FindProperty("speedValue");
        scoreValue = serializedObject.FindProperty("scoreValue");
        duration = serializedObject.FindProperty("durationValue");
        magneticPower = serializedObject.FindProperty("magneticPower");
        magneticDuration = serializedObject.FindProperty("magneticDuration");
        magneticObject = serializedObject.FindProperty("magnetic");
    }
    //인스펙터에서 편히 보기 위함.
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // 현재 SerializedObject 업데이트

        EditorGUI.BeginChangeCheck(); // 변경 감지 시작
        ItemType selectedType = (ItemType)itemType.enumValueIndex; // 현재 Enum 값을 가져오기
        selectedType = (ItemType)EditorGUILayout.EnumPopup("아이템 타입", selectedType); // UI에서 변경

        if (EditorGUI.EndChangeCheck()) // 값이 변경되었는지 확인
        {
            itemType.enumValueIndex = (int)selectedType; // 새로운 값 저장
        }

        if(selectedType == ItemType.Heal)
        {
            EditorGUILayout.PropertyField(hpValue, new GUIContent("회복량"));
        }

        else if(selectedType == ItemType.Speed)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(speedValue, new GUIContent("속도 증가량"));
            EditorGUILayout.PropertyField(duration, new GUIContent("지속 시간"));
            GUILayout.EndHorizontal();
        }

        else if(selectedType == ItemType.Score)
        {
            EditorGUILayout.PropertyField(scoreValue, new GUIContent("점수 증가량"));
        }
        else if(selectedType == ItemType.Magnetic)
        {
            EditorGUILayout.PropertyField(magneticPower, new GUIContent("자력 강도"));
            EditorGUILayout.PropertyField(magneticDuration, new GUIContent("자력 지속시간"));
            EditorGUILayout.PropertyField(magneticObject, new GUIContent("자력 효과 프리팹"));
        }
        serializedObject.ApplyModifiedProperties(); // 변경 사항 적용
    }
}
#endif