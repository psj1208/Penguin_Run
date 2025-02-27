using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class AchievePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI des;

    private void OnEnable()
    {
        StartCoroutine(Alarm());
    }
    public void SetTitletxt(string input)
    {
        title.text = input;
    }

    public void SetDestxt(string input)
    {
        des.text = input;
    }
    
    IEnumerator Alarm()
    {
        CanvasGroup can = GetComponent<CanvasGroup>();
        can.alpha = 0;
        can.DOFade(1, 1).SetUpdate(true);
        yield return new WaitForSecondsRealtime(3);
        Invisible();
    }
    void Invisible()
    {
        GetComponent<CanvasGroup>().DOFade(0, 1)
            .OnComplete(()=> Destroy(gameObject)).SetUpdate(true);
    }
}
