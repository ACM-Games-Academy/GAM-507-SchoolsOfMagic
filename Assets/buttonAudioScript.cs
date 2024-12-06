using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonAudioScript : MonoBehaviour
{
    [Header("Wwise UI Sounds")]
    public AK.Wwise.Event playSelect;
    public AK.Wwise.Event uiBack;
    public AK.Wwise.Event uiHover;
    public AK.Wwise.Event uiPause;
    public AK.Wwise.Event uiSelect;
    public AK.Wwise.Event stopTitleMusic;
    public AK.Wwise.Event startTitleMusic;
    public AK.Wwise.Event playDeathTheme;
    public AK.Wwise.Event stopDeathTheme;


    public void Start()
    {
        startTitleMusic.Post(this.gameObject);
    }

    public void OnEnable()
    {
        if (this.gameObject.tag == "deathMenu")
        {
            playDeathTheme.Post(this.gameObject);
        }
        if (this.gameObject.tag == "pauseMenu")
        {
            PlayUiPause();
        }
    }

    public void OnDisable()
    {
        if (this.gameObject.tag == "deathMenu")
        {
            stopDeathTheme.Post(this.gameObject);
        }
    }
    public void PlayPlaySelect()
    {
        playSelect.Post(this.gameObject);
        stopTitleMusic.Post(this.gameObject);
        
    }
    public void PlayUiBack()
    {
        uiBack.Post(this.gameObject);
    }
    public void PlayUiHover()
    {
        uiHover.Post(this.gameObject);
    }
    public void PlayUiPause()
    {
        uiPause.Post(this.gameObject);
    }
    public void PlayUiSelect()
    {
        uiSelect.Post(this.gameObject);
    }


}
