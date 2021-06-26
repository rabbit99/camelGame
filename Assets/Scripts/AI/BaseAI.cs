using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAI : MonoBehaviour
{
    public string aiName;
    public AIManager aiManager;
    public abstract void ActionBehaviour();
    protected abstract void spinBehaviour();
    protected abstract void PickOneBehaviour();
}
