using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    private UIState curUIState;

    private StartUI startUI;
    private GameUI gameUI;
    private GameOverUI gameOverUI;

    public void ChangeUIState()
    {

    }
}
