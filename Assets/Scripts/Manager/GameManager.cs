using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using williamTool;

[System.Serializable]
public class GameManagerPlayResponseEvent : UnityEvent<int> { }


public class GameManager : Singleton<GameManager>
{
    public GameObject preShowGameObjectRoot;
    public GameObject[] preShowGameObject;
    public UnityEvent onSpinClickEvent = new UnityEvent();
    public GameManagerPlayResponseEvent onGameManagerPlayResponseEvent = new GameManagerPlayResponseEvent();
    public UnityEvent onGameManagerRefresResponseEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        onGameManagerRefresResponseEvent.Invoke();
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
        preShowGame();
    }

    public void onPlayResponse(int index)
    {
        Debug.Log("get the spin step = " + index);
        onGameManagerPlayResponseEvent.Invoke(index);
    }

    private void preShowGame()
    {
        for (int i = 0; i < preShowGameObject.Length; i++)
        {
            GameObject go = Instantiate(preShowGameObject[i], Vector3.zero, Quaternion.identity);
            go.transform.SetParent(preShowGameObjectRoot.transform);
        }
    }
}
