using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using System;

public enum txtPos
{
    Up,
    Down
}
public class TutoUiManager : MonoBehaviour
{
    [SerializeField] GameObject darkBackGround;
    [SerializeField] GameObject txtUpPanel;
    [SerializeField] GameObject txtDownPanel;

    TextMeshProUGUI Uptext;

    TextMeshProUGUI Downtext;

    Coroutine corutine = null;
    Queue<(string, txtPos, KeyCode, Action)> coroutinesWaiting = new Queue<(string, txtPos, KeyCode, Action)>();
    void Start()
    {
        Uptext = txtUpPanel.GetComponentInChildren<TextMeshProUGUI>();
        Downtext = txtDownPanel.GetComponentInChildren<TextMeshProUGUI>();
        darkBackGround.SetActive(false);
        txtUpPanel.SetActive(false);
        txtDownPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlText(bool input)
    {
        txtUpPanel.SetActive(input);
        txtDownPanel.SetActive(input);
    }

    public void ControlDarkBG(bool input)
    {
        darkBackGround.SetActive(input);
    }

    public void TextHappen(string txt,txtPos tp, KeyCode key = KeyCode.Return, Action ac = null)
    {
        if (corutine != null)
        {
            coroutinesWaiting.Enqueue((txt, tp, key, ac));
        }
        else
        {
            corutine = StartCoroutine(MakeText(txt, tp, key, ac));
        }
        Debug.Log(coroutinesWaiting.Count);
    }

    private IEnumerator MakeText(string txt,txtPos tp, KeyCode key,Action ac)
    {
        if (Time.timeScale > 0)
            TutorialManager.Instance.EventHappen();
        GameObject selectPanel = null;
        TextMeshProUGUI selectTxt = null;
        switch (tp)
        {
            case txtPos.Up:
                selectPanel = txtUpPanel;
                selectTxt = Uptext;
                break;
            case txtPos.Down:
                selectPanel = txtDownPanel;
                selectTxt = Downtext;
                break;
        }
        if (selectPanel.activeSelf == false)
            selectPanel.SetActive(true);
        selectTxt.text = txt;

        while (Input.GetKey(key)) //트러블 슈팅 요소(엔터 키 입력이 남아있었음) 엔터키 입력 초기화.
            yield return null;

        yield return new WaitUntil(() => Input.GetKeyDown(key)); //엔터 키 입력 대기
        if (coroutinesWaiting.Count > 0) //큐 카운팅 후 재귀 호출 혹은 종료.
        {
            (string nextTxt, txtPos nextTp, KeyCode nextkey, Action nextac) = coroutinesWaiting.Dequeue();
            corutine = StartCoroutine(MakeText(nextTxt, nextTp, nextkey, nextac));
        }
        else
        {
            TutorialManager.Instance.EventEnd();
            corutine = null;
        }
        if (ac != null)
            ac.Invoke();
        yield return null;
    }
}