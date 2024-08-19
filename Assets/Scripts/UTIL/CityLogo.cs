using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLogo : MonoBehaviour
{
    public bool ShowInTut = false;


    public float toCenterSpeed = 5;
    public float InCenterSpeed = 5;

    float dur = 1;

    bool benInCenter = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x >0 ) benInCenter = true;
        transform.position += Time.fixedDeltaTime * (benInCenter ? InCenterSpeed : toCenterSpeed) * Vector3.right;

        if (benInCenter) dur -= Time.fixedDeltaTime;
        if (dur <= 0) gameObject.SetActive(false);
    }
}
