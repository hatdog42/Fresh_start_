using UnityEngine;
using UnityEngine.UI;

public class PlayerUserInterfaceManeger : MonoBehaviour
{

    public PlayerHealthManager _PlayerHealth;
    public Image[] hearts;

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = i < _PlayerHealth.lives
                ? new Color(1, 1, 1, 1)
                : new Color(1, 1, 1, 0.1f);
        }
    }
}