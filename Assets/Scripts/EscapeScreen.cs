using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject escapeScreen;

    [SerializeField]
    private GameObject settingsScreen;

    [SerializeField]
    private GameObject powerupScreen;

    private bool isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        escapeScreen.SetActive(false);
        settingsScreen.SetActive(false);
        powerupScreen.SetActive(false);
        isGamePaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        escapeScreen.SetActive(true);
        isGamePaused = true;
        Cursor.visible = true;
    }

    public void onClickSettingsButton()
    {
        settingsScreen.SetActive(true);
        escapeScreen.SetActive(false);
        Cursor.visible = true;
    }
}
