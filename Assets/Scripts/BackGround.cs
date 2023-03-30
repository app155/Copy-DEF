using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameCamera mainCamera;
    public float offset;


    void LateUpdate()
    {
        FollowCamera();
    }

    void FollowCamera()
    {
        if (!mainCamera.isMove)
            return;

        transform.Translate(mainCamera.speed * offset * Time.deltaTime, 0, 0);
    }
}
