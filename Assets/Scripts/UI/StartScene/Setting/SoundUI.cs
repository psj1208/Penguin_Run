using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : SettingBaseUI
{
    protected override SettingUIState GetUIState()
    {
        return SettingUIState.Sound;
    }

    public override void Init(StartSceneManager start)
    {
        base.Init(start);
    }
}
