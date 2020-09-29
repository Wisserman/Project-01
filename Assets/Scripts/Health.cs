using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    AudioPlayer _audioPlayer;
    ParticleScript _particleScript;
    public event Action Hit = delegate { };

    public int maxHealth;
    public int currentHealth;
    public Image healthBar;
    GameObject gameControl;
    GameObject playerCharacter;

    private void Awake()
    {
        gameControl = GameObject.Find("GameController");
        playerCharacter = GameObject.Find("ThirdPersonPlayer");
        _audioPlayer = GetComponentInChildren<AudioPlayer>();
        _particleScript = GetComponent<ParticleScript>();

        currentHealth = maxHealth;

    }

    private void Start()
    {
         healthBar.GetComponent<UIBar_Fill>().SetMaxValue(maxHealth);
    }

    void Update()
    {
        if (healthBar == null)
        {
            healthBar = GameObject.Find("HealthBarImage").GetComponent<Image>();

            healthBar.GetComponent<UIBar_Fill>().SetMaxValue(maxHealth);
        }

            healthBar.GetComponent<UIBar_Fill>().SetValue(currentHealth);
    }

    public void TakeDamage(int dmgAmount)
    {
        currentHealth -= dmgAmount;

        StartCoroutine("HitTimer");
        _particleScript.SummonParticles("Hit");

        if (currentHealth <= 0)
        {
            Debug.Log("dead");
            Kill();
            _audioPlayer.PlayClip("Dead");
        }
        else
            _audioPlayer.PlayClip("Hit");
    }

    public void Kill()
    {
        // TODO add death menu
        gameObject.GetComponent<ThirdPersonMovement>().Release();
    }

    private IEnumerator HitTimer()
    {
        Hit?.Invoke();
        yield return new WaitForSeconds(0.45f);
    }
}
