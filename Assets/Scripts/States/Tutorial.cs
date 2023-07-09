using UnityEngine;
using System.Collections;

public class Tutorial : State
{
    State nextState;

    private float duration = 10f;
    private float targetTimeScale = 0f;
    float initialSpeed = 3.0f;
    Burnable tutorialCoal;

    float initialFireBurn;

    bool started = false;

    IEnumerator slowDownTime;


    public Tutorial(GameController gameController, State nextState) : base(gameController)
    {
        this.nextState = nextState;
        slowDownTime = SlowDownTime();
    }

    public override void Enter()
    {
        // gameController.TutorialCanvas.SetActive(true);
        gameController.SetLevel(GameState.Tutorial);

        GameObject gameObject = GameObject.Instantiate(
                gameController.BurnablePrefabs[0],
                GetRandomSpawnPosition(),
                Quaternion.identity
            );
        tutorialCoal = gameObject.GetComponentInChildren<Burnable>();
        tutorialCoal.SetSpeed(initialSpeed);
        initialFireBurn = FireController.instance.BurnPower;
        gameController.StartCoroutine(slowDownTime);
        started = true;
    }

    IEnumerator SlowDownTime()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - start < duration)
        {
            Time.timeScale = Mathf.SmoothStep(1, targetTimeScale, (Time.realtimeSinceStartup - start) / duration);
            yield return null;
        }
        Time.timeScale = targetTimeScale;
        // displayTutorial();
    }

    private void displayTutorial()
    {
        gameController.TutorialCanvas.SetActive(true);
    }


    public override void Execute()
    {
        if ((tutorialCoal == null || tutorialCoal.health <= 0) && started)
        {
            gameController.StopCoroutine(slowDownTime);
            Time.timeScale = 1;
            gameController.ChangeState(nextState);
        }
    }

    public override void Exit()
    {
        gameController.TutorialCanvas.SetActive(false);
    }


    private Vector3 GetRandomSpawnPosition()
    {
        float randomAngle = 225.0f;
        Vector3 randomDirection = Quaternion.Euler(0.0f, randomAngle, 0.0f) * Vector3.forward;
        Vector3 spawnPosition = randomDirection * 40;
        return spawnPosition;
    }
}
