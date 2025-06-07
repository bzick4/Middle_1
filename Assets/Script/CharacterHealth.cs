using UnityEngine;
using UnityEngine.UI;


public class CharacterHealth : MonoBehaviour
{
    public Settings Settings;
    private float _health = int.MaxValue;
    public PlayerStats HealthStats;
    [SerializeField] private Image _healthBar;
   

    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, 100);
            WriteStatistics();
            if (_health <= 0) Destroy(this.gameObject);
        }
    }

    private void Damage()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Health -= 20;
        }
    }

    private void WriteStatistics()
    {
        Settings.HeroHealth = _health;
    }

    private void Awake()
    {
        _health = Settings.HeroHealth;
       
    }

    private void Update()
    {
        Damage();

        if (_healthBar != null)
        {
            _healthBar.fillAmount = _health / 100f;
        }
        
        if (_health != Settings.HeroHealth)
    {
        _health = Settings.HeroHealth;
    }

    }



}
