using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DivergentRoadTile : BoardTile
{
    public static bool result = false;
    public UnityEvent onCustomEvent = new UnityEvent();
    public string Direction;
    public async override Task PassByAction()
    {
        Debug.Log("路過分歧路，暫行行走，選擇下一個前進的方向");
        onCustomEvent.Invoke();
        await waitForSetDirection();
    }

    public async override Task ArrivalAction()
    {
        Debug.Log("抵達分歧路，選擇下一個前進的方向");
        onCustomEvent.Invoke();
        await waitForSetDirection();
    }

    private async Task waitForSetDirection()
    {
        while (true)
        {
            await Task.Delay(System.TimeSpan.FromSeconds(0.1));
            if (result)
            {
                result = false;
                await Task.Yield();
                return;
            }
        }
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
