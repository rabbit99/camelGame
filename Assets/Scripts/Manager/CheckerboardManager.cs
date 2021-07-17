using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public struct BoardBase
{
    public TileBase tb;
    public Vector3 worldPos;
}

public class CheckerboardManager : MonoBehaviour
{
    public Tilemap tileMap;

    public GameObject markNum;
    public GameObject markNumParent;

    public List<Vector3> availablePlaces;
    // Start is called before the first frame update
    void Start()
    {
        availablePlaces = new List<Vector3>();
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        int i = 1;
        foreach (var go in availablePlaces)
        {
            GameObject mark = Instantiate<GameObject>(markNum);
            mark.GetComponentInChildren<Text>().text = i.ToString();
            mark.transform.SetPositionAndRotation(go, Quaternion.identity);
            mark.transform.SetParent(markNumParent.transform);
            mark.transform.Translate(new Vector3(0, 0.3f));
            i++;
        }

        BoundsInt bounds = tileMap.cellBounds;
        TileBase[] allTiles =  tileMap.GetTilesBlock(bounds);
        List<BoardBase> allUsedTiles = new List<BoardBase>();
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                BoardBase bb;
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    bb.tb = tile;
                    bb.worldPos = tileMap.GetCellCenterWorld(new Vector3Int( x, y, 0));
                    allUsedTiles.Add(bb);
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
        //int i = 1;
        //foreach(var go in allUsedTiles)
        //{
        //    Debug.Log(go.tb.name + " " + go.worldPos);
        //    GameObject mark = Instantiate<GameObject>(markNum);
        //    mark.transform.SetPositionAndRotation(go.worldPos, Quaternion.identity);
        //    mark.GetComponentInChildren<Text>().text = i.ToString();
        //    mark.transform.SetParent(markNumParent.transform);
        //    i++;
        //}
        //Debug.Log(allUsedTiles.Count+ "¼Æ¶q");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
