using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Units : MonoBehaviour
{
    public UnitType.unitType type;

    public int hp;
    public int atk;
    public float speed;
    public float atkRange;
    public bool isOver;

    public Bases mybase;
    public Units target;
    public Bases targetBase;
    public ParticleSystem dust;
    public BoxCollider2D colliders;
    public SpriteRenderer renderers;
    public Coroutine co;
    public Animator anim;

    public enum State
    {
        Stop,
        Move,
        Attack,
        Hit,
        Die,
    }

    public State state;

    public State _state
    {
        get { return state; }

        set
        {
            if (isOver)
                return;

            if (state == State.Die)
                return;

            if (state == value)
                return;


            anim.SetBool($"do{state.ToString()}", false);
            anim.SetBool($"do{value.ToString()}", true);
            Invoke($"Stop{state.ToString()}", 0);
            Invoke($"{value.ToString()}", 0);

            
            state = value;
        }
    }

    void Awake()
    {
        mybase = GameObject.Find($"Base {LayerMask.LayerToName(gameObject.layer)}").GetComponent<Bases>();
        anim = GetComponent<Animator>();
        colliders = GetComponent<BoxCollider2D>();
        renderers = GetComponent<SpriteRenderer>();
        _state = State.Move;
        

        switch (type)
        {
            case UnitType.unitType.Wizard:
                atkRange = 3.5f;
                break;
            case UnitType.unitType.Range:
                atkRange = 2.5f;
                break;
            case UnitType.unitType.Bullet:
                atkRange = 0.4f;
                break;
            default:
                atkRange = 0.7f;
                break;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        LifeCycle();
    }

    void FixedUpdate()
    {
        Scan();
    }

    void LifeCycle()
    {
        switch (_state)
        {
            case State.Stop:
                break;
            case State.Move:
                if (type != UnitType.unitType.Wizard)
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                break;
            case State.Attack:
                break;
            case State.Hit:
                break;
            case State.Die:
                break;
            default:
                break;
        }
    }

    void Scan()
    {
        if (_state == State.Stop || _state == State.Move)
            ScanEnemy();
    }

    void ScanEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(8 - gameObject.layer, 0, 0), atkRange, LayerMask.GetMask(LayerMask.LayerToName(16 - gameObject.layer)));
        Debug.DrawRay(transform.position, new Vector3((8 - gameObject.layer) * atkRange, 0, 0), Color.red);

        if (hit.transform == null)
        {
            ScanAlly();
            return;
        }
            

        target = hit.transform.GetComponent<Units>();
        targetBase = hit.transform.GetComponent<Bases>();

        _state = State.Attack;
    }

    void ScanAlly()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(8 - gameObject.layer, 0, 0), 0.3f, LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer)));
        Debug.DrawRay(transform.position, new Vector3((8 - gameObject.layer) * 0.3f, 0, 0), Color.blue);

        if (hit.transform == null)
            return;

        _state = State.Stop;
    }


    void Move()
    {
        if (dust != null && !dust.isPlaying)
            dust.Play();
    }

    void StopMove()
    {
        if (dust != null)
            dust.Stop();
    }

    void Stop()
    {
        co = StartCoroutine(CoStop());
    }

    IEnumerator CoStop()
    {
        if (dust != null)
            dust.Stop();

        if (isOver)
            yield break;

        yield return new WaitForSeconds(0.5f);

        _state = State.Move;
    }

    void StopStop()
    {

    }

    void Attack()
    {
        if (type == UnitType.unitType.Sword || type == UnitType.unitType.Guard)
            co = StartCoroutine(CoAttack());

        else if (type == UnitType.unitType.Range)
            co = StartCoroutine(CoShot());

        else if (type == UnitType.unitType.Wizard)
            co = StartCoroutine(CoSpell());
    }

    IEnumerator CoAttack()
    {
        if (target != null)
        {
            target.anim.SetBool("doHit", true);
            target.Hit(atk);
        }

        else
        {
            targetBase.Hit(atk);
        }

        if (type == UnitType.unitType.Sword)
            mybase.manager.sound.SFX(SoundManager.Sound.Sword);

        else
            mybase.manager.sound.SFX(SoundManager.Sound.Guard);

        yield return new WaitForSeconds(Random.Range(0.8f, 1.2f));

        _state = State.Move;
    }

    IEnumerator CoShot()
    {
        Bullet myarrow = Instantiate(mybase.arrow, transform.position, Quaternion.identity).GetComponent<Bullet>();
        myarrow.atk = atk;

        mybase.manager.sound.SFX(SoundManager.Sound.Range);

        yield return new WaitForSeconds(Random.Range(0.8f, 1.2f));

        _state = State.Move;
    }

    IEnumerator CoSpell()
    {
        Bullet mymagic = Instantiate(mybase.magic, target.transform.position, Quaternion.identity).GetComponent<Bullet>();
        mymagic.atk = atk;

        mybase.manager.sound.SFX(SoundManager.Sound.Magic);

        yield return new WaitForSeconds(Random.Range(0.8f, 1.2f));

        _state = State.Move;
    }

    void StopAttack()
    {

    }

    void Die()
    {
        colliders.enabled = false;
        renderers.sortingOrder = 1;

        Destroy(gameObject, 3);
        mybase.unitList.Remove(gameObject);
    }

    public void Hit(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
            _state = State.Die;
    }



    void StopHit()
    {

    }

    void None()
    {

    }

}
