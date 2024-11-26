using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnimation : WeaponAnimations
{
    [SerializeField] float shakeAmount;
    [SerializeField] float shakeTime;

    [SerializeField] Transform GunParent;

    public override void AnimateGunShot()
    {
        base.AnimateGunShot();

        StartCoroutine(gunShake());
    }

    private IEnumerator gunShake()
    {
        Vector3 oringinalPos = GunParent.transform.localPosition;

        for (float shakeDuration = shakeTime; shakeDuration > 0;)
        {
            GunParent.localPosition = oringinalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * 1f;

            yield return new WaitForEndOfFrame();
        }

        GunParent.localPosition = oringinalPos;

        yield return null;
    }
}
