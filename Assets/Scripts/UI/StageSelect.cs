using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    [SerializeField] Button Tuto;
    [SerializeField] Button stage1;
    [SerializeField] Button stage2;
    [SerializeField] Button ExitButton;
    // Start is called before the first frame update
    void Start()
    {
        Tuto.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        stage1.onClick.AddListener(() => { SceneManager.LoadScene(2); });
        stage2.onClick.AddListener(() => { SceneManager.LoadScene(3); });
        StartSceneManager sManager = GetComponentInParent<StartSceneManager>();
        ExitButton.onClick.AddListener(() => sManager.SelectState(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
