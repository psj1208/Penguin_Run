using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField] private TutoUiManager TutouiManager;

    private void Awake()
    {
        //싱글톤 (중복 생성시 파괴)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EventHappen()
    {
        Time.timeScale = 0;
        TutouiManager.ControlDarkBG(true);
    }

    public void EventEnd()
    {
        TutouiManager.ControlText(false);
        TutouiManager.ControlDarkBG(false);
        Time.timeScale = 1.0f;
    }
}
