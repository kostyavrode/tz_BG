using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    [SerializeField] private GameObject drawPrefab;
    [SerializeField] private Plane plane;

    private GameObject trail;
    private Vector3 startPosition;
    private void Start()
    {
        plane = new Plane(Camera.main.transform.forward, this.transform.position);
        plane.Flip();
    }
    private void FixedUpdate()
    {
        Draw();
    }
    private void Draw()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            trail = Instantiate(drawPrefab, transform.position, Quaternion.identity, this.transform);
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(mouseRay, out float distance))
            {
                startPosition = mouseRay.GetPoint(distance);
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(mouseRay, out float distance))
            {
                trail.transform.position = mouseRay.GetPoint(distance);
            }
        }
        else
        {
            Destroy(trail);
        }
    }
}
