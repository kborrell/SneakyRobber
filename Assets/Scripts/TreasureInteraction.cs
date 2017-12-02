﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData))]
public class TreasureInteraction : MonoBehaviour {

    private void Awake()
    {
        m_playerData = GetComponent<PlayerData>();
        m_transform = GetComponent<Transform>();
    }

    void PickTreasure(TreasureData treasure)
    {
        m_playerData.AddTreasureToInventory(treasure);
        treasure.gameObject.SetActive(false);
    }

    void ThrowTreasure(TreasureData treasure)
    {
        m_playerData.RemoveTreasureFromInventory(treasure);
        treasure.gameObject.SetActive(true);
        treasure.transform.position = m_transform.position;
    }

    void CollectRewards()
    {
        var treasuresList = m_playerData.GetAllItems();
        for (int i = 0; i < treasuresList.Count; i++)
        {
            var treasure = treasuresList[i];
            m_playerData.AddScore(treasure.GetValue());
        }

        m_playerData.ClearInventory();
    }

    private void Update()
    {
        if (InputManager.Instance.GetButtonDown(InputManager.Button.ThrowItem))
        {
            var treasureList = m_playerData.GetAllItems();
            if (treasureList.Count > 0)
            {
                ThrowTreasure(treasureList[treasureList.Count - 1]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectZone"))
        {
            CollectRewards();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (InputManager.Instance.GetButtonDown(InputManager.Button.PickItem) && other.gameObject.CompareTag("Treasure"))
        {
            PickTreasure(other.GetComponent<TreasureData>());
        }
    }

    private PlayerData m_playerData;
    private Transform  m_transform;
}
