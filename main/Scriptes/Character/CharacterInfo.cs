using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterInfo : MonoBehaviour
{
    
    private int _score;
    private float _health;


    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public void AddScore(int value)
    {
        _score += value;
        UpdateUI();
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
    }

    private void UpdateUI()
    {
        _scoreText.text = "Score: " + _score.ToString();
    }
}
