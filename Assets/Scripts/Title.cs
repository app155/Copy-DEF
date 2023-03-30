using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    float count = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
        }

        count = count + 0.015f;
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(count) * 0.15f, transform.position.z);

        if (count > 314)
            count = 0;
    }
}
