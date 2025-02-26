using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelathUI : MonoBehaviour
{
    [SerializeField] private RectTransform eff;
    [SerializeField] private GameObject healthBar;

    // Update is called once per frame
    void Update()
    {
        SetEffectPos();
    }

    void SetEffectPos()
    {
        RectTransform healthRect = healthBar.GetComponent<RectTransform>();
        if (eff == null)
            return;
        eff.localPosition = new Vector3(healthRect.sizeDelta.x * healthBar.GetComponent<Image>().fillAmount - 20, 0, 0);
    }
}
