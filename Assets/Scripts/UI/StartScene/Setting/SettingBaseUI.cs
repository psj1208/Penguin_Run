using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingBaseUI : MonoBehaviour
{
    protected StartSceneManager sManager;

    public virtual void Init(StartSceneManager start)
    {
        this.sManager = start;
    }

    protected abstract SettingUIState GetUIState();
    public void SetActive(SettingUIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
