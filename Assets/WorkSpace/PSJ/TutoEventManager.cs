using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEventManager : MonoBehaviour
{
    public static TutoEventManager Instance;
    TutoUiManager uiManager;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        uiManager = GetComponent<TutoUiManager>();
    }
    public void EventOne()
    {
        uiManager.TextHappen("안녕하세요.", txtPos.Down);
        uiManager.TextHappen("안녕하세요!!!", txtPos.Up);
    }
}
