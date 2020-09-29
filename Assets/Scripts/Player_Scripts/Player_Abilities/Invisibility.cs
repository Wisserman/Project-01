using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invisibility : Ability
{
    public float invisTimerMax;
    float invisTimer = 0;
    public SkinnedMeshRenderer playerSkin;

    public Image invisBar;

    int i = 0;

    private void Awake()
    {
        if (invisBar == null)
            invisBar = GameObject.Find("InvisibilityBarImage").GetComponent<Image>();
        _audioPlayer = GameObject.Find("AudioPlayerObject").GetComponent<AudioPlayer>();
        _particleScript = GameObject.Find("ThirdPersonPlayer").GetComponent<ParticleScript>();
        playerSkin = GameObject.Find("Ch24").GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        Debug.Log(invisTimerMax);
        invisBar.GetComponent<UIBar_Fill>().SetMaxValue(invisTimerMax);
    }

    private void Update()
    {
        if (invisBar == null)
        {
            invisBar = GameObject.Find("InvisibilityBarImage").GetComponent<Image>();

            invisBar.GetComponent<UIBar_Fill>().SetMaxValue(invisTimerMax);
        }

        if (playerSkin.enabled == true && invisTimer < invisTimerMax)
            invisTimer += 3 / invisTimerMax * Time.deltaTime;
        else if (playerSkin.enabled == false && invisTimer > 0)
            invisTimer -= 3 / invisTimerMax * Time.deltaTime;

        if (invisTimer > invisTimerMax)
            invisTimer = invisTimerMax;
        else if (invisTimer < 0)
            invisTimer = 0;

        invisBar.GetComponent<UIBar_Fill>().SetValue(invisTimer);

    }

    public override void Use()
    {
        if (playerSkin.enabled == true && invisTimer >= invisTimerMax)
        {
            _particleScript.SummonParticles("Invisible");
            _audioPlayer.PlayClip("Invisible");
            StartCoroutine("InvisTimer");
        }
    }


    IEnumerator InvisTimer()
    {
        playerSkin.enabled = !playerSkin.enabled;
        inUse = !inUse;
        yield return new WaitForSecondsRealtime(invisTimer);
        playerSkin.enabled = !playerSkin.enabled;
        inUse = !inUse;
    }
}
