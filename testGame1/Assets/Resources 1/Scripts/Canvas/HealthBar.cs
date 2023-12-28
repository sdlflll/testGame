using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _healthBarSlider;
    private Player _player;

    void Start()
    {
     _healthBarSlider = GetComponent<Slider>();
     _player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        HealthBarHandler();
    }

    private void HealthBarHandler()
    {
        _healthBarSlider.value = _player.Health / 100;
    }
}
