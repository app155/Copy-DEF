using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public int level;
    public bool isWizard;
    public List<GameObject> unitList;
    public int cost;
    public Bases mybase;
    public GameObject upgradeBtn;

    public List<Sprite> imageList;
    public Image upImage;
    public Text hpText;
    public Text atkText;
    public Text costText;


    void LateUpdate()
    {
        BtnSetActive();
        BuyUISync();
    }

    void BuyUISync()
    {
        upImage.sprite = imageList[level];

        if (isWizard)
        {
            if (level == 0)
                return;

            atkText.text = unitList[level].GetComponent<Units>().atk.ToString();
            return;
        }

        
        hpText.text = unitList[level].GetComponent<Units>().hp.ToString();
        atkText.text = unitList[level].GetComponent<Units>().atk.ToString();
        costText.text = cost.ToString();
    }

    void BtnSetActive()
    {
        if (level == unitList.Count - 1 || mybase.cost < 10)
        {
            upgradeBtn.SetActive(false);
            return;
        }

        upgradeBtn.SetActive(true);
    }

    public void Buy()
    {
        if (mybase.cost < cost)
            return;

        Transform go = Instantiate(unitList[level].transform);
        mybase.cost -= cost;
        mybase.unitList.Add(go.gameObject);

        if (mybase.gameObject.layer == 7)
            mybase.manager.sound.SFX(SoundManager.Sound.Buy);

    }

    public void Upgrade()
    {
        if (level == unitList.Count - 1)
            return;

        if (mybase.cost < 10)
            return;

        mybase.cost -= 10;
        level++;

        if (isWizard)
        {
            if (unitList[level - 1] != null)
                unitList[level - 1].SetActive(false);

            unitList[level].SetActive(true);
        }

        if (mybase.gameObject.layer == 7)
            mybase.manager.sound.SFX(SoundManager.Sound.Upgrade);
    }
}
