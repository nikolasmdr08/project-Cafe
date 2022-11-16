using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour
{
    public static Lifes Instance;

    [SerializeField] private GameObject[] hearts;
    [SerializeField] private int lifeCounter;

    public int LifeCounter { get => lifeCounter; }


    private void Awake()
    {
        Instance = this;
    }

    public void Hurt()
    {
        if (LifeCounter==3)
        {
            hearts[2].SetActive(false);
            lifeCounter--;
        }
        else if (LifeCounter==2)
        {
            hearts[1].SetActive(false);
            lifeCounter--;
        }
        else if (LifeCounter==1)
        {
            hearts[0].SetActive(false);
            lifeCounter--;

        }
    }

    public void Potion()
    {
        if (LifeCounter == 2)
        {
            hearts[2].SetActive(true);
            lifeCounter++;
        }
        else if (LifeCounter == 1)
        {
            hearts[1].SetActive(true);
            lifeCounter++;
        }
    }
}
