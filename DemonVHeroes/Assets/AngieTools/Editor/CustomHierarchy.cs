using System.Linq;
using AngieTools.Editor.Data.HierarchyData;
using UnityEditor;
using UnityEngine;
namespace AngieTools.Editor
{
    [InitializeOnLoad]
    public class CustomHierarchy : MonoBehaviour
    {
        private static readonly HierarchyDatabase HierarchyData;

        private static HierarchyObjectLink _previousLink;
        
        static CustomHierarchy()
        {
            if (HierarchyData == default)
            {
                HierarchyData = Resources.Load<HierarchyDatabase>("AngieTools/Data/HierarchyDatabase");

                if (HierarchyData == null)
                {
                    if(!AssetDatabase.IsValidFolder("Assets/Resources"))
                        AssetDatabase.CreateFolder("Assets", "Resources");
                    
                    if(!AssetDatabase.IsValidFolder("Assets/Resources/AngieTools"))
                        AssetDatabase.CreateFolder("Assets/Resources", "AngieTools");
                    
                    if(!AssetDatabase.IsValidFolder("Assets/Resources/AngieTools/Data"))
                        AssetDatabase.CreateFolder("Assets/Resources/AngieTools", "Data");
                    
                    var newDatabase = ScriptableObject.CreateInstance<HierarchyDatabase>();
                    
                    AssetDatabase.CreateAsset(newDatabase, "Assets/Resources/AngieTools/Data/HierarchyDatabase.asset");

                    HierarchyData = Resources.Load<HierarchyDatabase>("AngieTools/HierarchyDatabase");
                }
            }
            
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGui;
        }

        private static void HandleHierarchyWindowItemOnGui(int p_instanceId, Rect p_selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(p_instanceId);

            if (obj == null) return;

            if (HierarchyData == null) return;


            var prefabType = PrefabUtility.GetCorrespondingObjectFromSource(obj);

            foreach (var objectLink in HierarchyData.Data)
            {
                if (PrefabUtility.GetPrefabInstanceStatus(obj) == PrefabInstanceStatus.NotAPrefab) continue;

                if (prefabType != objectLink.ObjectToColor)
                {
                    if (_previousLink == null) break;
                    
                    UpdateItem(p_selectionRect, obj as GameObject, _previousLink);
                }
                else
                {
                    _previousLink = objectLink;
                    UpdateItem(p_selectionRect, obj as GameObject, objectLink);
                }
            }
        }

        private static void UpdateItem(Rect p_selectionRect,GameObject p_obj, HierarchyObjectLink p_objectLink, 
            TextAnchor p_alignment = TextAnchor.MiddleCenter)
        {
            EditorGUI.DrawRect(p_selectionRect, p_objectLink.BoxColor);
            EditorGUI.LabelField(p_selectionRect, p_obj.name, new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = p_objectLink.TextColor,
                },
                        
                alignment = p_alignment
            });
        }
    }
}
