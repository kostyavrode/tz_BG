using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerPart playerPart;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetZ;
    [SerializeField] public List<PlayerPart> playerParts = new List<PlayerPart>();

    private PlayerAnimatorController playerAnimatorController;
    private SplineFollower splineFollower;

    private void Awake()
    {
        SetStartCobstruction();
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.enabled = !splineFollower.enabled;
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
    }
    private void OnEnable()
    {
        LineController.onGameStarted += StartMovement;
    }
    private void OnDisable()
    {
        LineController.onGameStarted -= StartMovement;
    }
    private void SetStartCobstruction()
    {
        for (int i = 0; i < 20; i++)
        {
            var newPart = Instantiate(playerPart,this.transform.position,Quaternion.identity, this.transform);
            playerParts.Add(newPart);
        }
        for (int i = 0; i < 20; i++)
        {
            if (i < 10)
                playerParts[i].transform.position = new Vector3(playerParts[i].transform.position.x + offsetX * i, playerParts[i].transform.position.y
                    , playerParts[i].transform.position.z);
            if (i >= 10)
                playerParts[i].transform.position = new Vector3(playerParts[i].transform.position.x + offsetX * (i - 10), playerParts[i].transform.position.y
                    , playerParts[i].transform.position.z + offsetZ);
        }
    }
    public void RemovePlayerPart(PlayerPart removePart)
    {
        playerParts.Remove(removePart);
        removePart.gameObject.SetActive(false);
    }
    public void AddPlayerPart()
    {
        var newPart = Instantiate(playerPart, playerParts[playerParts.Count].transform.position+ new Vector3(0.3f,0,0.3f), Quaternion.identity, this.transform);
        playerParts.Add(newPart);
        playerAnimatorController.ChangeSingleAnimRunState(newPart.GetComponentInChildren<Animator>());
    }
    private void StartMovement()
    {
        splineFollower.enabled = true;
        playerAnimatorController.ChangeRunAnimState();
    }
    public void PlayVictoryAnim()
    {
        playerAnimatorController.ChangeVictoryAnimState();
    }    
}
