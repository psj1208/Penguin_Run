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
        Start,
        Game,
        GameOver
    }
    #endregion

    #region Struct
    #endregion

    #region Delegate
    public delegate void ChangeHP(int figure);
    public delegate void ChangeSpeed(float figure);
    #endregion
}