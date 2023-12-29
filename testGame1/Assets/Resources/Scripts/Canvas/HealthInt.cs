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
            _healthInt.color = new Color(1f, 47f / 51f, 4f / 255f, 0.505f);
            if (_player.Health <= 30)
            {
                _healthInt.color = new Color(1,0,0, 0.505f);
            }
        }
        else
        {
            _healthInt.color = new Color(1,1,1, 0.505f);
        }
        
    }
}
