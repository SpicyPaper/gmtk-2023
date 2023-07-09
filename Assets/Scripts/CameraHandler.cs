using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10f;

    [SerializeField] private Vector3 targetPosition = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.RotateAround(targetPosition, Vector3.up, movementSpeed * Time.deltaTime);
    }
}
