using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuardController : MonoBehaviour
{
    [SerializeField]
    private Transform flashlight;

    [SerializeField]
    private State currentState;

    public enum State
    {
        Patrol,
        Wonder,
        Alert,
        Chase
    }

    private void Start()
    {
        StartCoroutine(FlashlightLookAnimation());
    }

    IEnumerator FlashlightLookAnimation()
    {
        while (true)
        {
            float angle = (Random.value * 60) - 30;
            yield return flashlight.DOLocalRotate(new Vector3(5, angle, 0), 1f).SetEase(Ease.OutSine);
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
    }
}
