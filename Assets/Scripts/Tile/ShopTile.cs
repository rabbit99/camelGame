using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class ShopTile : BoardTile
{
    public string ShopTileName;
    public async override Task PassByAction()
    {
        await Task.Delay(System.TimeSpan.FromSeconds(2));
        Debug.Log("���L�ө��A���ݤ@�U");
       
    }

    public async override Task ArrivalAction()
    {
        await Task.Delay(System.TimeSpan.FromSeconds(3));
        Debug.Log("��F�ө��A���}�ө����A�i�i���ʶR");


    }

    IEnumerator ShopPassByAction()
    {
        yield return new WaitForSeconds(2);
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/BoardTile/ShopTile")]
    public static void CreateDivergentRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Shop Tile", "New Shop Tile", "Asset", "Save Shop Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ShopTile>(), path);
    }
#endif
}
