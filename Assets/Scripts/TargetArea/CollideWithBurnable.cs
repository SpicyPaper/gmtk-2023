using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithBurnable : MonoBehaviour
{
    private List<Burnable> collisionList;
    // Start is called before the first frame update
    void Start()
    {
        collisionList = new List<Burnable>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown(){
        Debug.Log("Mouse clicked.");
        Debug.Log(collisionList.Count);
        List<Burnable> toRemove = new List<Burnable>();
        for (int i = 0 ; i < collisionList.Count; i++){
            Burnable burnable = collisionList[i];
            if (burnable == null){
                toRemove.Add(burnable);
                continue;
            }
            bool isDead = GameController.instance.BurnableClicked(burnable);
            if (isDead){
                toRemove.Add(burnable);
            }
        }
        for(int i=0;i<toRemove.Count;i++){
            collisionList.Remove(toRemove[i]);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Burnable"){
            collisionList.Add(other.GetComponent<Burnable>());
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Burnable"){
            collisionList.Remove(other.GetComponent<Burnable>());
        }
    }
}
