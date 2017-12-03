﻿using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
  TurnController turnController;
  GameObject visuals;
  Player player;

  protected void Awake()
  {
    player = GetComponentInParent<Player>();
    visuals = transform.GetChild(0).gameObject;
    turnController = GameObject.FindObjectOfType<TurnController>();
    turnController.onTurnChange += TurnController_onTurnChange;
  }

  protected void OnDestroy()
  {
    turnController.onTurnChange -= TurnController_onTurnChange;
  }

  void TurnController_onTurnChange()
  {
    visuals.SetActive(player.isMyTurn);
  }
}
