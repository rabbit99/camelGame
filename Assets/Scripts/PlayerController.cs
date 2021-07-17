using System.Collections;
using System.Collections.Generic;
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
    private Vector3 errorV3 = new Vector3(-9999, 9999, 9999);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Movement(int step)
    {
        StartCoroutine(Move(step));
    }

    IEnumerator Move(int step)
    {
        for (int i = 0; i < step; i++)
        {
            if (lastDir != Direction.None)
            {
                moveOneWayOneStep(lastDir);
            }
            else
            {
                moveOneWayOneStep(nowDir);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void moveOneWayOneStep(Direction direction)
    {
        Vector3Int gridPos = getGridPos(direction);

        if (tileMap.HasTile(gridPos))
        {
            Vector3 movementPos = tileMap.GetCellCenterWorld(gridPos);
            this.gameObject.transform.position = new Vector3(movementPos.x, movementPos.y + 0.3f);
            lastDir = direction;
        }
        else
        {
            tryOtherWay(direction);
        }
    }

    private Vector3 checkDirVector3(Direction direction)
    {
        Vector3Int gridPos= getGridPos(direction);
        if (tileMap.HasTile(gridPos))
        {
            Vector3 movementPos = tileMap.GetCellCenterWorld(gridPos);
            return movementPos;
        }
        else
        {
            Debug.Log("worng dir " + direction.ToString());
        }
        return errorV3;
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
        Debug.Log("tryOtherWay " + direction.ToString());
        switch (direction)
        {
            case Direction.WeatNorth:
                if (checkDirVector3(Direction.EastNorth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.EastNorth);
                    direction = Direction.EastNorth;
                }
                else if (checkDirVector3(Direction.WeatSouth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.WeatSouth);
                    direction = Direction.WeatSouth;
                }
                break;
            case Direction.WeatSouth:
                if (checkDirVector3(Direction.WeatNorth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.WeatNorth);
                    direction = Direction.WeatNorth;
                }
                else if (checkDirVector3(Direction.EastSouth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.EastSouth);
                    direction = Direction.EastSouth;
                }
                break;
            case Direction.EastNorth:
                if (checkDirVector3(Direction.WeatNorth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.WeatNorth);
                    direction = Direction.WeatNorth;
                }
                else if (checkDirVector3(Direction.EastSouth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.EastSouth);
                    direction = Direction.EastSouth;
                }
                break;
            case Direction.EastSouth:
                if (checkDirVector3(Direction.EastNorth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.EastNorth);
                    direction = Direction.EastNorth;
                }
                else if (checkDirVector3(Direction.WeatSouth) != errorV3)
                {
                    movementPos = checkDirVector3(Direction.WeatSouth);
                    direction = Direction.WeatSouth;
                }
                break;
            default:
                break;
        }
        this.gameObject.transform.position = new Vector3(movementPos.x, movementPos.y + 0.3f);
        lastDir = direction;
    }
}
