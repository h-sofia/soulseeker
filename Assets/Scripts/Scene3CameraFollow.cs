using UnityEngine;

public class Scene3CameraFollow : MonoBehaviour
{
    public Transform Target { get; set; }

    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = new Vector3(
            Target.position.x,
            Target.position.y,
            transform.position.z
        );
    }
}
