using UnityEngine;

public class MatchController : MonoBehaviour
{
    private float time;
    private float lifetime = 10;

    private bool created = false;

    [SerializeField] private GameObject oilSpill = null;

    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - time;
        if (t > lifetime)
        {
            Destroy(gameObject);
        }

        if (gameObject.transform.position.y <= 0 && !created && t > 2)
        {
            GameObject oilOnFire = Instantiate(oilSpill, gameObject.transform.position, Quaternion.identity);
            oilOnFire.transform.GetChild(0).transform.GetComponent<OilSpill>().SetOnFire();

            created = true;
        }
    }
}
