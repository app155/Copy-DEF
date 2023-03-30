using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Units target;
    Bases targetBases;
    GameManager manager;
    public int atk;
    public float speed;
    public float atkRange = 0.4f;
    public bool isHit;
    

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void FixedUpdate()
    {
        ScanEnemy();
    }

    void ScanEnemy()
    {
        if (isHit)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(8 - gameObject.layer, 0, 0), atkRange, LayerMask.GetMask(LayerMask.LayerToName(16 - gameObject.layer)));
        Debug.DrawRay(transform.position, new Vector3((8 - gameObject.layer) * atkRange, 0, 0), Color.red);

        if (hit.transform == null)
            return;

        target = hit.transform.GetComponent<Units>();
        targetBases = hit.transform.GetComponent<Bases>();

        if (target != null)
        {
            target.anim.SetBool("doHit", true);
            target.Hit(atk);
            manager = target.mybase.manager;
        }

        else
        {
            targetBases.Hit(atk);
            manager = targetBases.manager;
        }
        
        isHit= true;

        if (speed != 0)
        {
            Destroy(gameObject);
            manager.sound.SFX(SoundManager.Sound.RangeHit);
        }
            

        else
            Destroy(gameObject, 3f);
    }
}
