using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDeclaration
{
    #region Enum
    public enum ItemType
    {
        Heal,
        Speed,
        Score,
        Magnetic
    }

    public enum UIState
    {
        None,
        Game,
        GameOver
    }

    public enum AudioResType
    {
        etc,
        Background,
        sfx
    }

    public enum SettingUIState
    {
        Sound,
        Control
    }

    public enum SceneType
    {
        None = -1,
        Start,
        Tutorial,
        Stage1,
        Stage2,
    }
    #endregion
}