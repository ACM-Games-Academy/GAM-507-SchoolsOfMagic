using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LMGAnimation : WeaponAnimations
{
    [SerializeField] Transform LMGParent;
    [SerializeField] Transform LMGBullets;

    [SerializeField] float shakeAmount;
    [SerializeField] float shakeTime;
    [SerializeField] float oneShotRotation;
    [SerializeField] float rotationSpeed;

    public override void AnimateGunShot()
    {
        base.AnimateGunShot();
        //lmg will spin bullets and move 
        //this will simulate one bullet being shot

        //first will spin the bullets;
        StartCoroutine(bulletSpin());

        //now will shake the gun
        //this will be another coroutine cus yeah
        StartCoroutine(gunShake());
    }

    private IEnumerator bulletSpin()
    {
        //the bullets spinning will be done over multiple frames to make it smoother 

        for (float i = 0; i < oneShotRotation;)
        {
            //if the next rotation exceeds the rotation needed for one shot it will
            //set the next rotation to the exact rotation
            if (rotationSpeed * Time.deltaTime + i >= oneShotRotation)
            {
                //has rotated all the way round 
                LMGBullets.Rotate(Vector3.forward, oneShotRotation - i);
                i = oneShotRotation;
            }
            else
            {
                LMGBullets.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                i += rotationSpeed * Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private IEnumerator gunShake()
    {
        Vector3 oringinalPos = LMGParent.transform.localPosition;

        for (float shakeDuration = shakeTime; shakeDuration > 0;)
        {
            LMGParent.localPosition = oringinalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * 1f;

            yield return new WaitForEndOfFrame();
        }

        LMGParent.localPosition = oringinalPos;

        yield return null;
    }
}
