using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeInOutText : MonoBehaviour
{
    private TMP_Text text;

    private float elapsed = 0;

    private float neededTime = 1.5f;

    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime * direction;

        if (elapsed < 0 || elapsed > neededTime)
        {
            direction *= -1;
        }

        float perc = Mathf.Lerp(0, 1, elapsed / neededTime);
        text.color = new Color(1, 1, 1, perc);
    }
}
