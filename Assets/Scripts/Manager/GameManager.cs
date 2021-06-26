using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using williamTool;

[System.Serializable]
public class GameManagerPlayResponseEvent : UnityEvent<int> { }

public class GameManager : Singleton<GameManager>
{
    public UnityEvent onSpinClickEvent = new UnityEvent();
    public GameManagerPlayResponseEvent onGameManagerPlayResponseEvent = new GameManagerPlayResponseEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spin()
    {
        Debug.Log("GameManager send spin event");
        onSpinClickEvent.Invoke();
    }

    public void onRefreshResponse(RefreshData refreshData)
    {
        Debug.Log("Refresh get the data , player name = " + refreshData.name);
    }

    public void onPlayResponse(int index)
    {
        Debug.Log("get the spin step = " + index);
        onGameManagerPlayResponseEvent.Invoke(index);
    }
}
