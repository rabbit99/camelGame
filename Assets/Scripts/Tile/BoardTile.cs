using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BoardTile : Tile
{
    public int number;
    public string boardName;
    public BoardTileType boardTileType = BoardTileType.Normal;
    public bool hasObstacles = false;
    public virtual string GetBoardName()
    {
        string _boardName = !string.IsNullOrEmpty(boardName) ? boardName : this.GetType().ToString();
        return _boardName;
    }
    public virtual bool CheckObstacles() {
        bool result = hasObstacles;
        //TODO: 暫定障礙只擋一次
        hasObstacles = true? false : false;
        return result; 
    }
    public async virtual Task PassByAction() { }
    public async virtual Task ArrivalAction() { }

}
