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
        /*

        //calcul des coordonnées de la camera
        // [-1 pour tourner dans le bon sens] [+90 pour positionner la caméra derrière le player] [Mathf.Pi/180 --> deg en rad] [+ posX ou  + posY pour le déplacement] 
        float x = (Mathf.Cos((tr_goToFollow.rotation.eulerAngles.y * (-1) + 90) * (Mathf.PI / 180)) * fl_offset) + tr_goToFollow.transform.position.x;
        float y = this.transform.position.y;
        float z = (Mathf.Sin((tr_goToFollow.rotation.eulerAngles.y * (-1) + 90) * (Mathf.PI / 180)) * fl_offset) + tr_goToFollow.transform.position.z;

        this.transform.position = new Vector3(x, y, z);
        this.transform.forward = tr_goToFollow.transform.forward;*/

        float x = (Mathf.Cos((tr_goToFollow.rotation.eulerAngles.y * (-1) + 90) * (Mathf.PI / 180)) * offsetXZ) + tr_goToFollow.transform.position.x;
        float y = tr_goToFollow.position.y + offsetY;
        float z = (Mathf.Sin((tr_goToFollow.rotation.eulerAngles.y * (-1) + 90) * (Mathf.PI / 180)) * offsetXZ) + tr_goToFollow.transform.position.z;

        this.transform.position = new Vector3(x, y, z);
        this.transform.forward = tr_goToFollow.transform.forward;
    }
}
