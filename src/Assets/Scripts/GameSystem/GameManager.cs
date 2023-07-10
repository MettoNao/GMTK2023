using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;

public enum GameState
{
    opening,
    game,
    result,
    pause,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerCore player;
    [SerializeField] private List<LevelData> levels = new List<LevelData>();

    private int level;
    private List<EnemyCore> allEnemies = new List<EnemyCore>();

    private GameState state;

    [SerializeField] private CanvasGroup fade;
    [SerializeField] private GameOverMenuScript gameOverMenuScript;
    [SerializeField] private ScoreManager scoreManager;

    private EnemySpawner spawner;
    private EnemyUpdate enemyUpdate;

    private InstantiateManager instantiateManager;

    private float ElapsedTime;

    private async void Start()
    {
        instantiateManager = GetComponent<InstantiateManager>();

        spawner = new EnemySpawner(allEnemies, levels, instantiateManager);
        enemyUpdate = new EnemyUpdate(player, allEnemies, levels);

        //オープニング演出を開始
        state = GameState.opening;
        var cts = new CancellationTokenSource();
        await Opening(cts.Token);
        cts.Cancel();
    }

    async UniTask Opening(CancellationToken token)
    {
        await DOTween.To(() => fade.alpha, (v) => fade.alpha = v, 0, 0.3f).AsyncWaitForCompletion();

        var playercts = new CancellationTokenSource();
        await player.PlayerInit(playercts.Token);
        playercts.Cancel();

        state = GameState.game;

        BGMManager.Instance.Play(BGMPath.STAGE4, 0.3f, 1.0f);
    }

    public void GameOver()
    {
        BGMManager.Instance.Stop();
        state = GameState.result;
        gameOverMenuScript.InitMenu(scoreManager.GetScore());
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.game)
        {
            spawner.CreateEnemySequence(level);
            enemyUpdate.OnEnemyUpdate(level);

            ElapsedTime += Time.deltaTime;

            if (level >= levels.Count - 1) return;

            if (ElapsedTime >= levels[level].elapsedTime)
            {
                level++;
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == GameState.game)
        {
            enemyUpdate.OnEnemyFixedUpdate();
        }
    }

    //全ての味方が敵に寝返る
    public void SetAllyToEnemy()
    {
        foreach (var e in allEnemies)
        {
            e.SetAlly(false);
        }
    }
}
