using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public void StartSceneBtn()
    {
        SceneManager.LoadScene(1);
    }
}
