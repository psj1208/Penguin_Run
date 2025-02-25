using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private RectTransform playerPosInUI;
    [SerializeField] private RectTransform endPosInUI;
    private float curPosRatio;
    public float CurPosRatio { get { return curPosRatio; } set { curPosRatio = Mathf.Clamp01(value); } }
    [SerializeField] private float backGroundWidth;

    private Transform startInGame;
    private Transform EndInGame;
    private Transform playerInGame;
    void Start()
    {
        backGroundWidth = endPosInUI.position.x - playerPosInUI.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (startInGame != null && EndInGame != null && playerInGame != null) 
            setCurpos((playerInGame.position.x - startInGame.position.x) / (EndInGame.position.x - startInGame.position.x));
    }

    public void Init(Transform st,Transform end, Transform player)
    {
        startInGame = st;
        EndInGame = end;
        playerInGame = player;
    }
    public void setCurpos(float ratio)
    {
        CurPosRatio = ratio;
        playerPosInUI.anchoredPosition = new Vector2(backGroundWidth * curPosRatio, playerPosInUI.anchoredPosition.y);
    }
}
