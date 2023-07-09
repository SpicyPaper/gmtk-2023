using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigCoalBehavior : MonoBehaviour
{
    public GameObject coalboyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {

        Vector3 direction = transform.right;
        float distance = 4f;

        Instantiate(
            coalboyPrefab,
            transform.position + direction* distance,
            Quaternion.identity
        );

        Instantiate(
            coalboyPrefab,
            transform.position - direction* distance,
            Quaternion.identity
        );
    }
}
