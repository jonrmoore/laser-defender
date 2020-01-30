using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    Player p;
    void Start()
    {
        p = FindObjectOfType<Player>();
        healthText.text = p.GetPlayerHealth().ToString();
    }

    void Update() { healthText.text = p.GetPlayerHealth().ToString(); }
}
