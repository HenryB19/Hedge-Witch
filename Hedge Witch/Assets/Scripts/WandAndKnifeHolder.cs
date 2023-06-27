using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAndKnifeHolder : MonoBehaviour
{
    public Transform wand;
    public Transform knife;
    public Transform wandHolder;
    public Transform knifeHolder;
    void Start()
    {
        ResetKnife();
        ResetWand();
    }

    public void ResetKnife() => knife.SetPositionAndRotation(knifeHolder.position, knifeHolder.rotation);
    public void ResetWand() => wand.SetPositionAndRotation(wandHolder.position, wandHolder.rotation);
}
