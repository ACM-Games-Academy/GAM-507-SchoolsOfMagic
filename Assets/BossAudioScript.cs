using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudioScript : MonoBehaviour
{
    public AK.Wwise.Event bossTreeSlam;
    public AK.Wwise.Event bossSporeShot;

    public void PlayBossTreeSlam()
    {
        bossTreeSlam.Post(gameObject);
    }

    public void PlayBossSporeShot()
    {
        bossSporeShot.Post(gameObject);
    }
}
