using UnityEditor;
using UnityEditorInternal;

namespace Kameffee.AudioPlayer
{
    [CustomEditor(typeof(SeBundle))]
    public class SeBundleEditor : Editor
    {
        private ReorderableList _reorderableList;
    
        private void OnEnable()
        {
            var _list = serializedObject.FindProperty("_seDataList");
            _reorderableList = new ReorderableList(serializedObject, _list)
            {
                drawElementCallback = (rect, index, active, focused) =>
                {
                    rect.xMin += 10;
                    EditorGUI.PropertyField(rect, _list.GetArrayElementAtIndex(index), true);
                },
                elementHeightCallback = index => EditorGUI.GetPropertyHeight(_list.GetArrayElementAtIndex(index)),
            };
        }
    
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
