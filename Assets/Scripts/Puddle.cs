using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{

    [SerializeField] private float timeToLive = 5.0f;

    private float timer = 0.0f;

    private float initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeToLive){
            Destroy(gameObject.transform.parent.gameObject);
        }
        else{
            float scale = initialScale * (1 - timer / timeToLive);
            transform.localScale = new Vector3(scale, transform.localScale.y, scale);
        }
        
    }
}
