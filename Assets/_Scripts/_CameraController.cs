using UnityEngine;

public class _CameraController : MonoBehaviour
{
    public Transform targetToFollow;
    // Update is called once per frame
    void Update()
    {
        var position = targetToFollow.transform.position;
        transform.position = new Vector3(position.x, position.y,
            transform.position.z);
    }
}
