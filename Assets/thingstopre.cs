using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class thingstopre
{
  
  /*  [MenuItem("Assets/Convert to Prefab")]
    static void SpriteToPrefab()
    {
        
        int i = 0;
        foreach (Object o in Selection.objects)
        {
            if (o is Texture2D || o is Sprite)
            {
                
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(o));
                var oldPath = AssetDatabase.GetAssetPath(o);
                var newPath = oldPath.Replace("Graphics", "Prefabs").Replace(".png", i +".prefab");
                var go = new GameObject();
                var sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
               
                PrefabUtility.SaveAsPrefabAsset(go, newPath);
            
            }
            else
            {
                Debug.LogError("This is not a sprite");
                Debug.Log(o.GetType());
            }
            i++;
        }
    }
  */
}
