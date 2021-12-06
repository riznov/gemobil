using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        if (Target == null)
            return;
        this.transform.position = new Vector3(Target.position.x, this.transform.position.y, Target.position.z);

    }
}
