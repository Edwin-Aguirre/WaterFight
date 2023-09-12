using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip menuMoveSound;
    public static AudioClip menuClickSound;
    public static AudioClip deathSound;
    public static AudioClip hitSound;
    public static AudioClip shieldSound;
    public static AudioClip  balloonSound;
    public static AudioClip heartPowerupSound;
    public static AudioClip waterBottlePowerupSound;
    public static AudioClip waterBalloonPowerupSound;
    public static AudioClip dumbbellPowerupSound;

    static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        menuMoveSound = Resources.Load<AudioClip>("sfx_menu_move4");
        menuClickSound = Resources.Load<AudioClip>("sfx_menu_select1");
        deathSound = Resources.Load<AudioClip>("sfx_exp_odd7");
        hitSound = Resources.Load<AudioClip>("sfx_sound_neutral11");
        shieldSound = Resources.Load<AudioClip>("sfx_sound_neutral6");
        balloonSound = Resources.Load<AudioClip>("sfx_exp_short_hard3");
        heartPowerupSound =  Resources.Load<AudioClip>("sfx_sounds_powerup3");
        waterBottlePowerupSound = Resources.Load<AudioClip>("sfx_sounds_powerup5");
        waterBalloonPowerupSound = Resources.Load<AudioClip>("sfx_sounds_powerup10");
        dumbbellPowerupSound = Resources.Load<AudioClip>("sfx_sounds_powerup16");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "sfx_menu_move4":
                audioSource.PlayOneShot(menuMoveSound);
                break;
            case "sfx_menu_select1":
                audioSource.PlayOneShot(menuClickSound);
                break;
            case "sfx_exp_odd7":
                audioSource.PlayOneShot(deathSound);
                break;
            case "sfx_sound_neutral11":
                audioSource.PlayOneShot(hitSound);
                break;
            case "sfx_sound_neutral6":
                audioSource.PlayOneShot(shieldSound);
                break;
            case "sfx_exp_short_hard3":
                audioSource.PlayOneShot(balloonSound);
                break;
            case "sfx_sounds_powerup3":
                audioSource.PlayOneShot(heartPowerupSound);
                break;
            case "sfx_sounds_powerup5":
                audioSource.PlayOneShot(waterBottlePowerupSound);
                break;
            case "sfx_sounds_powerup10":
                audioSource.PlayOneShot(waterBalloonPowerupSound);
                break;
            case "sfx_sounds_powerup16":
                audioSource.PlayOneShot(dumbbellPowerupSound);
                break;
        }
    }
}
