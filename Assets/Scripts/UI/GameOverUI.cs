using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public struct GameOverModel
{
    public static int enemiesKilled = 0;
}
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    void Awake()
    {
        enemiesKilledText.text = GameOverModel.enemiesKilled.ToString();
    }

    public void Restart()
    {
        StartCoroutine(CloseGameOverScene());
    }

    IEnumerator CloseGameOverScene()
    {                                                        
        AsyncOperation load = SceneManager.UnloadSceneAsync("GameOverScene");
        yield return load;
    }
}
