using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : SettingBaseUI
{
    protected override SettingUIState GetUIState()
    {
        return SettingUIState.Control;
    }

    public override void Init(StartSceneManager start)
    {
        base.Init(start);
    }
}
