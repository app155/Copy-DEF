using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Bases : MonoBehaviour
{
    SpriteRenderer renderers;

    public GameManager manager;
    public GameObject arrow;
    public GameObject magic;
    public int cost;
    public int hp;
    public int maxHp;

    public List<GameObject> unitList;

    public Image costBar;
    public Text costText;
    public Text shadowcostText;

    public Image healthBar;
    public Text healthText;
    public Text shadowhealthText;
    public GameObject destroyEff;

    public Sprite normalSp;
    public Sprite hitSp;
    public Sprite destroySp;

    float costwidth;
    float costheight;
    float hpwidth;
    float hpheight;

    void Awake()
    {
        renderers = GetComponent<SpriteRenderer>();
        if (costBar != null)
        {
            costwidth = costBar.rectTransform.sizeDelta.x;
            costheight = costBar.rectTransform.sizeDelta.y;
        }

        hpwidth = healthBar.rectTransform.sizeDelta.x;
        hpheight = healthBar.rectTransform.sizeDelta.y;
    }

    void Start()
    {
        StartCoroutine(Income());
    }

    IEnumerator Income()
    {
        if (gameObject.layer == 7)
        {
            while (!manager.isOver)
            {
                yield return new WaitForSeconds(2f);

                if (cost < 10)
                    cost += 1;
            }
        }

        else
        {
            while (true)
            {
                yield return new WaitForSeconds(2 - 0.25f * manager.level);

                if (cost < 10)
                    cost += 1;
            }
        }
        
    }

    void Update()
    {
        Sync();
        HpSync();
    }

    void Sync()
    {
        if (costBar != null && costText != null && shadowcostText != null)
        {
            costText.text = cost.ToString();
            shadowcostText.text = costText.text;

            costBar.rectTransform.sizeDelta = new Vector2(costwidth * cost * 0.1f, costheight);
        }
    }

    void HpSync()
    {
        healthText.text = hp.ToString();
        shadowhealthText.text = healthText.text;

        healthBar.rectTransform.sizeDelta = new Vector2(hpwidth * hp / maxHp, hpheight);
    }

    public void Hit(int dmg)
    {
        hp -= dmg;

        if (hp > 0)
        {
            StartCoroutine(CoHit());
        }

        else
        {
            hp = 0;

            renderers.sprite = destroySp;
            destroyEff.gameObject.SetActive(true);
            HpSync();
            manager.GameOver(gameObject.name.Substring(5));
        }
    }

    IEnumerator CoHit()
    {
        renderers.sprite = hitSp;
        yield return new WaitForSeconds(0.1f);

        renderers.sprite = normalSp;
    }
}
