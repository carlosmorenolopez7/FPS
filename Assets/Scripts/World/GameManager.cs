using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public static GameManager Instance {get; private set;}

    public int ammo = 10;
    public int health = 100;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ammoText.text = ammo.ToString();
        healthText.text = health.ToString();
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
    }
}
