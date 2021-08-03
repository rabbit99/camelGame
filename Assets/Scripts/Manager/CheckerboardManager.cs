using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public List<PlayerController> players;

    private MouseInput mouseInput;

    public GameObject itemTest;

    public GameObject DivergentRoadPanel;

    private void Awake()
    {
        mouseInput = new MouseInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < players.Count; j++)
        {
            players[j].PlayerCheckTileAction += CheckTileAction;
            players[j].PlayerDoTileAction += DoTileAction;
            players[j].PlayerGetMovementPos += GetMovementPos;
            players[j].PlayerGetGridPos += GetGridPos;
        }
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
        TileBase[] allTiles = tileMap.GetTilesBlock(bounds);
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
                    bb.worldPos = tileMap.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    allUsedTiles.Add(bb);
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    if(tile as DivergentRoadTile)
                    {
                        Debug.Log("設定" + tile.name);
                        (tile as DivergentRoadTile).onCustomEvent.AddListener(()=> {
                            DivergentRoadPanel.SetActive(true);
                        });
                    }
                }
                else
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }

        mouseInput.Mouse.MouseClick.performed += x => MouseClick();
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MouseClick()
    {
        Vector2 mpos = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mpos = Camera.main.ScreenToWorldPoint(mpos);
        Vector3Int gridPos = tileMap.WorldToCell(mpos);
        if (tileMap.HasTile(gridPos))
        {
            Debug.Log("MouseClick HasTile ");
            //TODO: 分離出去對格子使用道具的行為
            Vector3 fpos = GetMovementPos(gridPos);
            GameObject item = Instantiate<GameObject>(itemTest);
            item.transform.position = new Vector3(fpos.x, fpos.y + 0.3f);

            BoardTile bt = tileMap.GetTile(gridPos) as BoardTile;
            if (bt)
                bt.hasObstacles = true;
        }
    }

    private bool CheckTileAction(Vector3Int gridPos)
    {
        return tileMap.HasTile(gridPos);
    }

    private async Task<bool> DoTileAction(Vector3Int gridPos, bool isArrived)
    {
        BoardTile bt = tileMap.GetTile(gridPos) as BoardTile;
        if (bt)
        {
            Debug.Log("走到 " + bt.GetBoardName() + " pos = " + gridPos);
            switch (bt.boardTileType)
            {
                case BoardTileType.Normal:
                    break;
                case BoardTileType.Shop:
                    break;
                case BoardTileType.DivergentRoad:
                    break;
            }
            if (bt.CheckObstacles())
            {
                Debug.Log(bt.GetBoardName() + " 發現障礙，中斷前進");
                //TODO: 使障礙物消失
                isArrived = true;
            }
            if (!isArrived)
            {
                await bt.PassByAction();
                Debug.Log("路過" + bt.GetBoardName() + "完畢");
            }
            else
            {
                await bt.ArrivalAction();
                Debug.Log("抵達" + bt.GetBoardName() + "完畢");
            }
            return isArrived;
        }
        else
        {
            Debug.Log("走到 " + "普通格" + " pos = " + gridPos);
            return false;
        }
    }

    private Vector3 GetMovementPos(Vector3Int gridPos)
    {
        return tileMap.GetCellCenterWorld(gridPos);
    }
    private Vector3Int GetGridPos(Vector3 nextPos)
    {
        return tileMap.WorldToCell(nextPos);
    }

    public void SetAdditionalTileAttributes(Vector3Int gridPos)
    {
        BoardTile bt = tileMap.GetTile(gridPos) as BoardTile;
    }

    public void SetDirection()
    {
        DivergentRoadTile.result = true;
    }
}
