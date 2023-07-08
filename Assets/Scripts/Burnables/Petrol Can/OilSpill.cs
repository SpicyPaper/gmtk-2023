using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpill : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject firePrefab;

    [SerializeField] private float BurnDurationMultiplier = 1.0f;
    

    [SerializeField] private float propagationDelay = 0.025f;

    private float spillScale;

    private float burnDuration;

    private bool isBurning = false;

    private float burnTimer = 0.0f;

    // Start is called before the first frame update

    void Start()
    {
        spillScale = transform.localScale.x;
        burnDuration = spillScale * BurnDurationMultiplier;

        if(anyNeighboursOnFire()){
            SetOnFire();
        }
        
    }

    public void SetOnFire()
    {
        if(isBurning){
            return;
        }

        Debug.Log("OilSpillController.SetOnFire()");

        // enable firePrefab
        firePrefab.SetActive(true);
        Debug.Log("firePrefab.activeSelf: " + firePrefab.activeSelf);

        isBurning = true;
        

        // TODO set fire to neighbor oil spills
        setFireToNeighbours();

    }

    void setFireToNeighbours(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, spillScale);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Oil Spill"))
            {
                OilSpill oilSpill = collider.gameObject.GetComponent<OilSpill>();
                if (oilSpill != null)
                {
                    // invoke SetOnFire() on oilSpill with delay
                    Debug.Log("Delaying oilSpill.SetOnFire() by " + propagationDelay + " seconds.");                    
                    oilSpill.Invoke("SetOnFire", propagationDelay);
                }
            }
        }
    }

    public bool IsBurning(){
        return isBurning;
    }

    bool anyNeighboursOnFire(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, spillScale);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Oil Spill"))
            {
                OilSpill oilSpill = collider.gameObject.GetComponent<OilSpill>();
                if (oilSpill != null && oilSpill.IsBurning())
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Update()
    {
        if (isBurning)
        {
        
            burnTimer += Time.deltaTime;
            if (burnTimer >= burnDuration)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OilSpill.OnCollisionEnter()");
        if (collision.gameObject.CompareTag("Fire"))
        {
            SetOnFire();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OilSpill.OnTriggerEnter()");
        if (other.gameObject.CompareTag("Fire"))
        {
            SetOnFire();
        }
    }
}
