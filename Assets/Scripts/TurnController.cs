using System;
using UnityEngine;

public class TurnController : MonoBehaviour
{
  const int actionPointsPerTurn = 3;

  public event Action onTurnChange;

  public event Action onActionPointsChange;

  int _numberOfActionsRemaining = actionPointsPerTurn;

  bool _isCurrentlyPlayer0sTurn = true;

  PhotonView photonView;

  public bool isCurrentlyPlayer0sTurn
  {
    get
    {
      return _isCurrentlyPlayer0sTurn;
    }
    set
    {
      if(isCurrentlyPlayer0sTurn == value)
      {
        return;
      }

      _isCurrentlyPlayer0sTurn = value;
      _numberOfActionsRemaining = actionPointsPerTurn;
      onTurnChange?.Invoke();
    }
  }

  public int numberOfActionsRemaining
  {
    get
    {
      return _numberOfActionsRemaining;
    }
    set
    {
      if(numberOfActionsRemaining == value)
      {
        return;
      }

      photonView.RPC("SetActionPoints", PhotonTargets.All, value);
    }
  }

  protected void Awake()
  {
    photonView = GetComponent<PhotonView>();
  }

  [PunRPC]
  void SetActionPoints(
    int value)
  {
    _numberOfActionsRemaining = value;
    onActionPointsChange?.Invoke();
    if (numberOfActionsRemaining <= 0)
    {
      isCurrentlyPlayer0sTurn = !isCurrentlyPlayer0sTurn;
    }
  }
}
