using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction
{
    None,
    WeatNorth,
    WeatSouth,
    EastNorth,
    EastSouth,
}


public class PlayerController : MonoBehaviour
{
    public string name;
    public Tilemap tileMap;
    private Direction lastDir = Direction.None;
    private Direction nowDir = Direction.WeatNorth;

    public delegate TResult CheckTileActionByVector3Int<TResult>(Vector3Int gridPos);
    public delegate Task<bool> CheckTileTaskByVector3Int(Vector3Int gridPos, bool isArrived);
    public delegate TResult CheckTileActionByVector3<TResult>(Vector3 nextPos);
    public CheckTileActionByVector3Int<bool> PlayerCheckTileAction;
    public CheckTileTaskByVector3Int PlayerDoTileAction;
    public CheckTileActionByVector3Int<Vector3> PlayerGetMovementPos;
    public CheckTileActionByVector3<Vector3Int> PlayerGetGridPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void Movement(int step)
    {
        bool Interrupted = false;
        for (int i = 0; i < step; i++)
        {
            Interrupted = await moveOneWayOneStep(lastDir != Direction.None ? lastDir : nowDir, i == step - 1);
            await Task.Delay(System.TimeSpan.FromSeconds(0.2f));
            if (Interrupted && i < step)
            {
                Debug.Log("player 仍有步數，被打斷");
                break;
            }
        }
        //StartCoroutine(Move(step));
    }

    //IEnumerator Move(int step)
    //{
    //    for (int i = 0; i < step; i++)
    //    {
    //        moveOneWayOneStep(lastDir != Direction.None ? lastDir : nowDir);
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}

    private async Task<bool> moveOneWayOneStep(Direction direction, bool isArrived)
    {
        Vector3Int gridPos = getGridPos(direction);
        bool Interrupted = false;
        if (PlayerCheckTileAction(gridPos))
        {
            Vector3 movementPos = PlayerGetMovementPos(gridPos);
            this.gameObject.transform.position = new Vector3(movementPos.x, movementPos.y + 0.3f);
            lastDir = direction;
        }
        else
        {
            tryOtherWay(direction);
        }
        Interrupted = await PlayerDoTileAction(gridPos, isArrived);
        return Interrupted;
    }

    private bool checkDirVector3(Direction direction)
    {
        Vector3Int gridPos = getGridPos(direction);
        return PlayerCheckTileAction(gridPos);
    }

    private Vector3 getMovementPos(Direction direction)
    {
        Vector3Int gridPos = getGridPos(direction);
        Vector3 movementPos = PlayerGetMovementPos(gridPos);
        return movementPos;
    }

    private Vector3Int getGridPos(Direction direction)
    {
        Vector3 oriPos = this.gameObject.transform.localPosition;
        Vector3 nextPos;
        switch (direction)
        {
            case Direction.WeatNorth:
                nextPos = new Vector3(0.4f + oriPos.x, 0.2f + oriPos.y);
                break;
            case Direction.WeatSouth:
                nextPos = new Vector3(0.4f + oriPos.x, -0.2f + oriPos.y);
                break;
            case Direction.EastNorth:
                nextPos = new Vector3(-0.4f + oriPos.x, 0.2f + oriPos.y);
                break;
            case Direction.EastSouth:
                nextPos = new Vector3(-0.4f + oriPos.x, -0.2f + oriPos.y);
                break;
            default:
                nextPos = new Vector3(oriPos.x, oriPos.y);
                break;
        }
        Vector3Int gridPos = tileMap.WorldToCell(nextPos);
        return gridPos;
    }

    private void tryOtherWay(Direction direction)
    {
        Vector3 movementPos = Vector3.zero;
        switch (direction)
        {
            case Direction.WeatNorth:
                if (checkDirVector3(Direction.EastNorth))
                {
                    movementPos = getMovementPos(Direction.EastNorth);
                    direction = Direction.EastNorth;
                }
                else if (checkDirVector3(Direction.WeatSouth))
                {
                    movementPos = getMovementPos(Direction.WeatSouth);
                    direction = Direction.WeatSouth;
                }
                break;
            case Direction.WeatSouth:
                if (checkDirVector3(Direction.WeatNorth))
                {
                    movementPos = getMovementPos(Direction.WeatNorth);
                    direction = Direction.WeatNorth;
                }
                else if (checkDirVector3(Direction.EastSouth))
                {
                    movementPos = getMovementPos(Direction.EastSouth);
                    direction = Direction.EastSouth;
                }
                break;
            case Direction.EastNorth:
                if (checkDirVector3(Direction.WeatNorth))
                {
                    movementPos = getMovementPos(Direction.WeatNorth);
                    direction = Direction.WeatNorth;
                }
                else if (checkDirVector3(Direction.EastSouth))
                {
                    movementPos = getMovementPos(Direction.EastSouth);
                    direction = Direction.EastSouth;
                }
                break;
            case Direction.EastSouth:
                if (checkDirVector3(Direction.EastNorth))
                {
                    movementPos = getMovementPos(Direction.EastNorth);
                    direction = Direction.EastNorth;
                }
                else if (checkDirVector3(Direction.WeatSouth))
                {
                    movementPos = getMovementPos(Direction.WeatSouth);
                    direction = Direction.WeatSouth;
                }
                break;
            default:
                break;
        }
        Debug.Log("tryOtherWay " + direction.ToString() + "is corrent");
        this.gameObject.transform.position = new Vector3(movementPos.x, movementPos.y + 0.3f);
        lastDir = direction;
    }
}
