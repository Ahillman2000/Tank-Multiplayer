using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private List<Image> hearts = new List<Image>();

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private Tank player;

    private void Update()
    {
        UpdateHearts(player.CurrentHealth);
    }

    public void UpdateHearts(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if(health > i)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
