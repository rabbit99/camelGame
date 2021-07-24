using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

//Game Manager ActivateAI
[System.Serializable]
public class GameManagerPlayResponseEvent : UnityEvent<int> { }
[System.Serializable]
public class InitPlayerDataEvent : UnityEvent<List<PlayerData>> { }
[System.Serializable]
public class ActivateAIEvent : UnityEvent<string> { }
[System.Serializable]
public class ShowCurrentControlUIEvent : UnityEvent<bool> { }
[System.Serializable]
public class UpdateNowPlayerInfoEvent : UnityEvent<PlayerData> { }

//Mock Server
[System.Serializable]
public class PlayResponseEvent : UnityEvent<int> { }

[System.Serializable]
public class RefreshResponseEvent : UnityEvent<RefreshData> { }


public enum BoardTileType
{
    Normal = 0,
    Shop,
    DivergentRoad,
}