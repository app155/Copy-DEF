using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCamera : MonoBehaviour
{
    public float speed;
    public bool isMove;



    void Update()
    {
        isMove = CameraMove();
    }


    bool CameraMove()
    {
        if ((transform.position.x > -2 && speed == -2) || (transform.position.x < 2 && speed == 2))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            return true;
        }

        return false;
    }

    public void RightDown()
    {
        speed += 2;
    }

    public void RightUp()
    {
        speed -= 2;
    }

    public void LeftDown()
    {
        speed -= 2;
    }

    public void LeftUp()
    {
        speed += 2;
    }

}
