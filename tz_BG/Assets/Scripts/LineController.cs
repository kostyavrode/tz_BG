using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening; 

public class LineController : MonoBehaviour
{
    public static Action onGameStarted;
    private List<Vector3> mousePositions = new List<Vector3>();
    [SerializeField]private LayerMask clickable;
    [SerializeField] private PlayerController playerController;
    private IDisposable mousePosCollector;
    private bool isFirstLineCreated;
    [SerializeField] private GameObject prefab;
    private void Update()
    {
            CollectPositions();
        if(Input.GetMouseButtonUp(0))
        {
            //int start = (mousePositions.Count - 1) / 2 - (playerController.playerParts.Count - 1) / 2;
            Debug.Log(mousePositions.Count);
            int start = 0;
            for (int i = 0; i < playerController.playerParts.Count; i++)
            {
                Vector3 newPos = new Vector3(mousePositions[start + i].x+2f, mousePositions[start + i].y, mousePositions[start + i].z+0.1f);
                //if (i > 1 && playerController.playerParts[i - 1].transform.position.x - newPos.x > 0.2)
                //{ }

                //if (i > 1 && playerController.playerParts[i - 1].transform.position.x - newPos.x <= .2)
                //{
                //    newPos.x += 0.1f * i;
                //}
                //if (i > 1 && playerController.playerParts[i - 1].transform.position.z - newPos.z <= .2)
                //{
                //    newPos.z += 0.1f * i;
                //}
                playerController.playerParts[i].transform.DOMove(newPos, 0.25f);
                if (!isFirstLineCreated)
                {
                    onGameStarted?.Invoke();
                    isFirstLineCreated = true;
                }
            }
        }
    }
    private void CollectPositions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePositions.Clear();
            mousePosCollector = Observable.Interval(System.TimeSpan.FromSeconds(0.01f)).TakeUntilDisable(this).Subscribe(x =>
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(myRay, out hit, 100, clickable))
                {
                mousePositions.Add(hit.point);
                }
            });
        }
    }
}
