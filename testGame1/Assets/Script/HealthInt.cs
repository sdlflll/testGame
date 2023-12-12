using TMPro;
using UnityEngine;

public class HealthInt : MonoBehaviour
{
    private TextMeshProUGUI _healthInt;
    private Player _player;
    void Start()
    {
        _healthInt = GetComponent<TextMeshProUGUI>();
        _player = FindObjectOfType<Player>();

    }

    void Update()
    {
        HealthIntHandler();
    }

    private void HealthIntHandler()
    {
        _healthInt.text = _player.Health.ToString();
        if (_player.Health <= 60)
        {
            _healthInt.color = Color.yellow;
            if (_player.Health <= 30)
            {
                _healthInt.color = Color.red;
            }
        }
        else
        {
            _healthInt.color = Color.white;
        }
        
    }
}
