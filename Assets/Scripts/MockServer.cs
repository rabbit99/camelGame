using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using williamTool;

public class MockServer : Singleton<MockServer>
{
    public int delaySeconds = 1;
    public PlayResponseEvent onPlayResponseEvent = new PlayResponseEvent();
    public RefreshResponseEvent onRefreshesponseEvent = new RefreshResponseEvent();

    private int gameIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onRefreshRequest()
    {
        Debug.Log("MockServer onRefreshRequest");
        StartCoroutine(DelayRequest(() => { onRefreshResponse(); }));
    }

    public void onPlayRequest()
    {
        Debug.Log("MockServer onPlayRequest");
        StartCoroutine(DelayRequest(() => { onPlayResponse(); }));
    }

    private IEnumerator DelayRequest(Action callback)
    {
        yield return new WaitForSeconds(delaySeconds);
        callback();
    }

    private void onPlayResponse()
    {
        onPlayResponseEvent.Invoke(UnityEngine.Random.Range(1, 7));
    }

    private void onRefreshResponse()
    {
        //TODO
        //init data and something have to do ..
        RefreshData refreshData = new RefreshData();
        refreshData.GameName = "new" + gameIndex;
        PlayerData mainPlayer = new PlayerData("mainPlayer", 0, false, true);
        PlayerData aiPlayer1 = new PlayerData("aiPlayer1", 0, true, false);
        PlayerData aiPlayer2 = new PlayerData("aiPlayer2", 0, true, false);
        PlayerData aiPlayer3 = new PlayerData("aiPlayer3", 0, true, false);
        refreshData.playersData = new List<PlayerData>();
        refreshData.playersData.Add(mainPlayer);
        refreshData.playersData.Add(aiPlayer1);
        refreshData.playersData.Add(aiPlayer2);
        refreshData.playersData.Add(aiPlayer3);
        onRefreshesponseEvent.Invoke(refreshData);
    }
}
