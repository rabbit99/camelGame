using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using williamTool;

[System.Serializable]
public class PlayResponseEvent : UnityEvent<int> { }

[System.Serializable]
public class RefreshResponseEvent : UnityEvent<RefreshData> { }

public class MockServer : Singleton<MockServer>
{
    public int delaySeconds = 1;
    public PlayResponseEvent onPlayResponseEvent = new PlayResponseEvent();
    public RefreshResponseEvent onRefreshesponseEvent = new RefreshResponseEvent();

    private int playerIndex = 0;
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
        refreshData.name = "new" + playerIndex;
        onRefreshesponseEvent.Invoke(refreshData);
    }
}
