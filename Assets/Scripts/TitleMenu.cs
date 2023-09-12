using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject titleLogo;
    [SerializeField]
    private GameObject splashLogo;
    [SerializeField]
    private GameObject buttonsCanvas;
    [SerializeField]
    private GameObject settingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayGame()
    {
        //SoundManagerScript.PlaySound("sfx_menu_select1");
        StartCoroutine(PlayButtonDelay());
    }

    public void onQuitGame()
    {
        StartCoroutine(QuitButtonDelay());
        //SoundManagerScript.PlaySound("sfx_menu_select1");
    }

    public void onHoverOverButton()
    {
        //SoundManagerScript.PlaySound("sfx_menu_move4");
    }

    private IEnumerator PlayButtonDelay()
    {
        yield return new WaitForSeconds(0.352f);
        SceneManager.LoadScene("ConnectToServer");
    }

    private IEnumerator QuitButtonDelay()
    {
        yield return new WaitForSeconds(0.352f);
        Application.Quit();
    }

    public void onSettingsButton()
    {
        titleLogo.SetActive(false);
        splashLogo.SetActive(false);
        buttonsCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        //SoundManagerScript.PlaySound("sfx_menu_select1");
    }

    public void onExitSettingsButton()
    {
        titleLogo.SetActive(true);
        splashLogo.SetActive(true);
        buttonsCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }
}
