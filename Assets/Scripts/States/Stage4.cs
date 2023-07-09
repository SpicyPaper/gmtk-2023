using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stage4 : StageState
{
    private float neededTime = 2;

    private bool onlyOnce = true;

    public Stage4(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level4;
        waitTimeBeforeLevelStart = gameController.WaitTimeBeforeLevel4Start;
    }

    public override void Execute()
    {
        if (levelStarted && fireController.BurnPower <= gameController.BurnPowerThreshold)
        {
            // Kill all burnables
            GameObject[] burnables = GameObject.FindGameObjectsWithTag("Burnable");
            foreach (GameObject burnable in burnables)
            {
                Burnable b = (Burnable)(burnable.GetComponentInChildren<Burnable>());
                b.TakeDamage(1000);
            }

            FireController.instance.SetMinBurnPower(0);
            FireController.instance.SetBurnPowerDecay(5);

            if (onlyOnce)
            {
                onlyOnce = false;
                gameController.StartCoroutine(WaitLevelStart());
            }

        }
    }
    IEnumerator WaitLevelStart()
    {
        yield return new WaitForSeconds(neededTime);
        SceneManager.LoadScene("IntroScene");
    }



}