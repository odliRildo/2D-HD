using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour
{
    
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;

    private void FixedUpdate(){
        Follow();
    }

    void Follow(){
        Vector3 target_pos = target.position + offset;
        Vector3 smoothed_pos = Vector3.Lerp(transform.position,target_pos, smoothFactor*Time.fixedDeltaTime);

        transform.position = smoothed_pos;
    }
}
