using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saw : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.DORotate(new Vector3(90f, 0f, 180f), 2.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
