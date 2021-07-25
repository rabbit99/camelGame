using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class DivergentRoadTile : BoardTile
{
    
    public string Direction;
    public async override Task ArrivalAction()
    {
        await Task.Yield();
        Debug.Log("路過分歧路，暫行行走，選擇下一個前進的方向");

    }

    public async override Task PassByAction()
    {
        await Task.Yield();
        Debug.Log("抵達分歧路，選擇下一個前進的方向");

    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/BoardTile/DivergentRoadTile")]
    public static void CreateDivergentRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save DivergentRoad Tile", "New DivergentRoad Tile", "Asset", "Save DivergentRoad Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DivergentRoadTile>(), path);
    }
#endif
}
