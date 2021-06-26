using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using williamTool;

public class GameManager : Singleton<GameManager>
{
    public GameObject preShowGameObjectRoot;
    public GameObject[] preShowGameObject;
    public UnityEvent onSpinClickEvent = new UnityEvent();
    public UnityEvent onGameManagerRefresResponseEvent = new UnityEvent();
    public UnityEvent onStartRoundkEvent = new UnityEvent();
    public UnityEvent onStopRoundkEvent = new UnityEvent();

    [HideInInspector]
    public GameManagerPlayResponseEvent onGameManagerPlayResponseEvent = new GameManagerPlayResponseEvent();
    [HideInInspector]
    public InitPlayerDataEvent onInitPlayerDataEvent = new InitPlayerDataEvent();
    [HideInInspector]
    public ActivateAIEvent onActivateAIEvent = new ActivateAIEvent();
    [HideInInspector]
    public ShowCurrentControlUIEvent onShowCurrentControlUIEvent = new ShowCurrentControlUIEvent();


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
        Debug.Log("Refresh get the data , player name = " + refreshData.GameName);
        preShowGame();
        onInitPlayerDataEvent.Invoke(refreshData.playersData);
        onShowCurrentControlUIEvent.Invoke(false);
        onStartRoundkEvent.Invoke();
    }

    public void onPlayResponse(int index)
    {
        Debug.Log("get the spin step = " + index);
        onGameManagerPlayResponseEvent.Invoke(index);
        onStopRoundkEvent.Invoke();
    }

    private void preShowGame()
    {
        for (int i = 0; i < preShowGameObject.Length; i++)
        {
            GameObject go = Instantiate(preShowGameObject[i], Vector3.zero, Quaternion.identity);
            go.transform.SetParent(preShowGameObjectRoot.transform);
        }
    }

    public void ShowCurrentControlUI(bool show)
    {
        onShowCurrentControlUIEvent.Invoke(show);
    }

    public void ActivateAI(string name)
    {
        onActivateAIEvent.Invoke(name);
    }
}
