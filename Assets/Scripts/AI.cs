using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Bases aiBase;
    int pattern;
    int patternValue;

    public Buttons wizardBtn;
    public Buttons archerBtn;
    public Buttons swordBtn;
    public Buttons guardBtn;

    public Coroutine co;


    void Awake()
    {
        aiBase = GetComponent<Bases>();
    }

    void Start()
    {
        pattern = Random.Range(0, 3);
        co = StartCoroutine(AIAction());
    }

    IEnumerator AIAction()
    {
        while (!aiBase.manager.isOver)
        {
            yield return new WaitForSeconds(1f);

            switch (pattern)
            {
                case 0:

                    yield return new WaitUntil(() => aiBase.cost >= 7);

                    patternValue = Random.Range(0, 2);

                    if (patternValue == 0)
                    {
                        swordBtn.Buy();
                        pattern = Random.Range(0, 3);
                    }
                        
                    else
                    {
                        archerBtn.Buy();
                        pattern = Random.Range(0, 3);
                    }
                        

                    break;

                case 1:

                    yield return new WaitUntil(() => aiBase.cost >= 7);

                    patternValue = Random.Range(0, 2);

                    if (patternValue == 0)
                    {
                        pattern = 0;
                        break;
                    }

                    else
                    {
                        guardBtn.Buy();
                        pattern = Random.Range(0, 3);
                    }
                        
                    break;

                case 2:

                    yield return new WaitUntil(() => aiBase.cost >= 10);

                    patternValue = Random.Range(0, 2);

                    if (patternValue == 0)
                    {
                        pattern = 1;
                        break;
                    }

                    else
                    {
                        int uprandom = Random.Range(0, 4);

                        switch (uprandom)
                        {
                            case 0:
                                wizardBtn.Upgrade();
                                break;
                            case 1:
                                swordBtn.Upgrade();
                                break;
                            case 2:
                                archerBtn.Upgrade();
                                break;
                            case 3:
                                guardBtn.Upgrade();
                                break;

                        }

                        pattern = Random.Range(0, 3);
                    }

                    break;
            }

        }
    }

}
