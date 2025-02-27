using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //튜토리얼 이벤트를 관리하기 위한 매니저.
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
    //이벤트 시작 시 어두운 배경화면과 시간 정지.(외부 호출용)
    public void EventHappen()
    {
        Time.timeScale = 0;
        TutouiManager.ControlDarkBG(true);
    }
    //그 반대(외부 호출용)
    public void EventEnd()
    {
        TutouiManager.ControlText(false);
        TutouiManager.ControlDarkBG(false);
        Time.timeScale = 1.0f;
    }
}
