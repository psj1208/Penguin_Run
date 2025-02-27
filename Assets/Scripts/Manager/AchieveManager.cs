using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public class Achieve
    {
        public string title;
        public string description;
        int CurValue;
        int TargetValue;
        bool isClear;

        public Achieve(string t, string d,int t_Value = 1)
        {
            title = t;
            description = d;
            TargetValue = t_Value;
            isClear = false;
        }

        public bool Renew()
        {
            if (isClear == false)
            {
                CurValue++;
                if (CurValue >= TargetValue)
                {
                    Debug.Log($"{title} 완료");
                    isClear = true;
                    return true;
                }
            }
            return false;
        }

        public void RenewZero()
        {
            isClear = false;
            CurValue = 0;
        }
    }
    [SerializeField] private GameObject AlarmPrefab;
    [SerializeField] private GameObject pos;
    [SerializeField] private AudioClip clip;
    public static AchieveManager Instance;
    Dictionary<int, Achieve> AchieveList;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AchieveList = new Dictionary<int, Achieve>();
        GenerateList();
    }

    void GenerateList()
    {
        AchieveList.Add(1, new Achieve("게임 기초 조작!", "설정 버튼을 누르세요."));
        AchieveList.Add(2, new Achieve("튜토리얼!", "튜토리얼을 클리어하세요."));
        AchieveList.Add(3, new Achieve("스테이지 클리어!", "아무 스테이지를 클리어하세요."));
    }

    public void AchieveRenew(int id)
    {
        if (!AchieveList.ContainsKey(id))
        {
            Debug.Log("해당 업적 없음");
            return;
        }
        if(AchieveList[id].Renew())
        {
            Debug.Log("알람 생성!");
            AudioManager.PlayClip(clip, AudioResType.sfx);
            GameObject obj = Instantiate(AlarmPrefab, pos.transform);
            AchievePanel panel = obj.GetComponent<AchievePanel>();
            panel.SetTitletxt(AchieveList[id].title);
            panel.SetDestxt(AchieveList[id].description);
        }
    }

    public void AchieveRenewZero(int id)
    {
        if (!AchieveList.ContainsKey(id))
        {
            Debug.Log("해당 업적 없음");
            return;
        }
        AchieveList[id].RenewZero();
    }
}
