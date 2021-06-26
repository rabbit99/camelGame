using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using williamTool;
public class ConsoleManager : MonoBehaviour
{
    public Text stepText;
    public Button spinBtn;
    // Start is called before the first frame update
    void Start()
    {
        Singleton<GameManager>.Instance.onGameManagerPlayResponseEvent.AddListener(ShowStepText);
        spinBtn.onClick.AddListener(Singleton<GameManager>.Instance.Spin);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowStepText(int index)
    {
        stepText.text = index + "步";
    }
}
