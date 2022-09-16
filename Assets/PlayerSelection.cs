using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public List<GameObject> tankSprites = new List<GameObject>();
    int currentIndex = 0;

    void Start()
    {
        foreach (GameObject tank in tankSprites)
        {
            tank.SetActive(false);
        }
        tankSprites[currentIndex].SetActive(true);
    }

    public void NextPlayerSprite()
    {
        tankSprites[currentIndex].SetActive(false);
        currentIndex++;
        if (currentIndex > tankSprites.Count - 1)
        {
            currentIndex = 0;
        }
        tankSprites[currentIndex].SetActive(true);
    }

    public void PreviousPlayerSprite()
    {
        tankSprites[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = tankSprites.Count - 1;
        }
        tankSprites[currentIndex].SetActive(true);
    }
}
