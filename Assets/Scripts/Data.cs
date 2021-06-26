using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{

}

public struct RefreshData
{
    public string GameName;
    public List<PlayerData> playersData;
}

public class PlayerData
{
    public string playerName;
    public int coin;
    public bool isAI;
    public bool isMine;
    public PlayerData(string _playerName, int _coin, bool _isAI, bool _isMine)
    {
        playerName = _playerName;
        coin = _coin;
        isAI = _isAI;
        isMine = _isMine;
    }
}