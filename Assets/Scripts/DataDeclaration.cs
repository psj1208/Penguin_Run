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
        Score
    }

    public enum UIState
    {
        None,
        Game,
        GameOver
    }

    public enum AudioResType
    {
        Background,
        sfx
    }

    public enum SettingUIState
    {
        Sound,
        Control
    }
    #endregion

    #region Struct
    #endregion

    #region Delegate
    public delegate void ChangeHP(int figure);
    public delegate void ChangeSpeed(float figure);
    #endregion
}