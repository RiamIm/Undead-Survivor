using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool bLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    
    IEnumerator GameOverRoutine()
    {
        bLive = false;

        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

    }
    
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    
    IEnumerator GameVictoryRoutine()
    {
        bLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Victory();
        Stop();
    }

    private void Update()
    {
        if (!bLive)
        {
            return;
        }

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if(!bLive)
        {
            return;
        }

        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        bLive = false;
        // 유니티의 시간 속도 (배율)
        Time.timeScale = 0;
    }

    public void Resume()
    {
        bLive = true;
        Time.timeScale = 1;
    }
}
