using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInput;
    public void NewNameEnter(string name)
    {
        MainManager.Instance.name = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        nameInput.onValueChanged.AddListener(NewNameEnter);
        nameInput.text = MainManager.Instance.name;
    }

    public void StartNew()
    {
        if (MainManager.Instance.name.Trim().Length != 0)
        {
            MainManager.Instance.isGameOver = false;
            SceneManager.LoadScene(1);
        }
        else StartCoroutine(InputInvalide());
    }

    IEnumerator InputInvalide()
    {
        nameInput.image.color = Color.red;
        yield return new WaitForSeconds(1);
        nameInput.image.color = Color.white;
    }

    public void Exit()
    {
        MainManager.Instance.SaveInfo();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
