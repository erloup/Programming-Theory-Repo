using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerTurret : MonoBehaviour
{
    public Transform tr_goToFollow;
    [SerializeField] private float offsetXZ;
    [SerializeField] private float offsetY;

    void LateUpdate()
    {
        float x = (Mathf.Cos((tr_goToFollow.rotation.eulerAngles.y * (-1) + 90) * (Mathf.PI / 180)) * offsetXZ) + tr_goToFollow.transform.position.x;
        float y = tr_goToFollow.position.y + offsetY;
        float z = (Mathf.Sin((tr_goToFollow.rotation.eulerAngles.y * (-1) + 90) * (Mathf.PI / 180)) * offsetXZ) + tr_goToFollow.transform.position.z;

        this.transform.position = new Vector3(x, y, z);
        this.transform.forward = tr_goToFollow.transform.forward;
    }
}
