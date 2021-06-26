using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NormalAI : BaseAI
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void ActionBehaviour()
    {
        Debug.Log(aiName + " do ActionBehaviour");
        spinBehaviour();
    }
    protected override void spinBehaviour()
    {
        StartCoroutine(DelayRequest(() => { aiManager.DoSpin(); }));

    }
    protected override void PickOneBehaviour()
    {

    }

    private IEnumerator DelayRequest(Action callback)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1, 3));
        callback();
    }
}
