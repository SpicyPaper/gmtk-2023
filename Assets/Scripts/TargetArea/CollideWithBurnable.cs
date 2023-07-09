using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithBurnable : MonoBehaviour
{
    [SerializeField] private float clickCooldown = 1.0f;

    [SerializeField] private float puddleCooldown = 1.0f;

    [SerializeField] private Light spotlight;

    [SerializeField] private GameObject puddlePrefab;

    private float lastClickTime = -1f;

    private float lastPuddleTime = -1f;

    private bool canClick = true;
    private List<Burnable> collisionList;

    private List<OilSpill> oilSpillList;
    // Start is called before the first frame update
    void Start()
    {
        collisionList = new List<Burnable>();
        oilSpillList = new List<OilSpill>();
    }


    // Update is called once per frame
    void Update()
    {

        if (lastClickTime + clickCooldown < Time.time || GameController.gameState == GameState.Tutorial)
        {
            canClick = true;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        if (lastPuddleTime + puddleCooldown < Time.time)
        {
            // if right click
            if (Input.GetMouseButtonDown(1))
            {
                lastPuddleTime = Time.time;
                float puddleSize = GameController.instance.GetPuddleSize();
                GameObject puddle = Instantiate(puddlePrefab, transform.position, Quaternion.identity);
                puddle.transform.localScale = new Vector3(puddleSize, puddle.transform.localScale.y, puddleSize);

                // random rotation
                puddle.transform.Rotate(0, Random.Range(0, 360), 0);

                // Find a game object with tag "Puddle Holder" and set the puddle as its child
                GameObject puddleHolder = GameObject.FindGameObjectWithTag("Puddle Holder");
                puddle.transform.parent = puddleHolder.transform;
            }
        }
    }

    void OnMouseDown()
    {
        if (canClick && GameController.instance.CanClick)
        {
            handleBurnableClicked();
            handleOilSpillClicked();
            SoundHandler.Instance.PlaySound(SoundHandler.SoundType.CLICK);

            canClick = false;
            lastClickTime = Time.time;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void handleBurnableClicked()
    {
        List<Burnable> toRemove = new List<Burnable>();
        for (int i = 0; i < collisionList.Count; i++)
        {
            Burnable burnable = collisionList[i];

            if (burnable == null)
            {
                toRemove.Add(burnable);
                continue;
            }
            bool isDead = GameController.instance.BurnableClicked(burnable);
            if (isDead)
            {
                toRemove.Add(burnable);
            }
        }
        for (int i = 0; i < toRemove.Count; i++)
        {
            collisionList.Remove(toRemove[i]);
        }
    }

    private void handleOilSpillClicked()
    {

        List<OilSpill> toRemove = new List<OilSpill>();
        for (int i = 0; i < oilSpillList.Count; i++)
        {
            OilSpill oilSpill = oilSpillList[i];
            if (oilSpill == null)
            {
                toRemove.Add(oilSpill);
                continue;
            }
            // destroy oil spill parent
            Destroy(oilSpill.transform.parent.gameObject);
        }
        for (int i = 0; i < toRemove.Count; i++)
        {
            oilSpillList.Remove(toRemove[i]);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Burnable")
        {
            collisionList.Add(other.GetComponent<Burnable>());
        }
        else if (other.gameObject.tag == "Oil Spill")
        {
            oilSpillList.Add(other.GetComponent<OilSpill>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Burnable")
        {
            collisionList.Remove(other.GetComponent<Burnable>());
        }
        else if (other.gameObject.tag == "Oil Spill")
        {
            oilSpillList.Remove(other.GetComponent<OilSpill>());
        }
    }

    public void UpgradeCooldown()
    {
        clickCooldown -= GameController.instance.UpgradeClickCooldownModifier;
    }

    public void UpgradeRange()
    {
        gameObject.transform.localScale *= GameController.instance.UpgradeRangeModifier;
        spotlight.spotAngle *= GameController.instance.UpgradeRangeModifier;
    }
}
