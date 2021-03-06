﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerData))]
public class TreasureInteraction : MonoBehaviour {

    private void Awake()
    {
        m_playerData = GetComponent<PlayerData>();
        m_transform = GetComponent<Transform>();

        InventoryItem.OnInventoryItemSelected += OnInventoryItemSelected;
    }

    private void OnDestroy()
    {
        InventoryItem.OnInventoryItemSelected -= OnInventoryItemSelected;
    }

    void PickTreasure(TreasureData treasure)
    {
        SoundController.Instance.PlayAudio(SoundController.AudioKey.PickObject);

        UIManager.Instance.HideInfoTooltip();
        m_playerData.AddTreasureToInventory(treasure);
        treasure.gameObject.SetActive(false);
    }

    void ThrowTreasure(TreasureData treasure)
    {
        SoundController.Instance.PlayAudio(SoundController.AudioKey.ThrowObject);

        m_playerData.RemoveTreasureFromInventory(treasure);
        treasure.gameObject.SetActive(true);
        treasure.transform.position = m_transform.position + (transform.GetChild(0).forward * -1);
        treasure.transform.rotation = transform.GetChild(0).rotation;
        treasure.GetComponent<Rigidbody>().AddForce((transform.GetChild(0).forward * -1) * 150000, ForceMode.Impulse);
        treasure.tag = "TreasureMisplaced";
    }

    void CollectRewards()
    {
        var treasuresList = m_playerData.GetAllItems();
        if (treasuresList.Count > 0)
        {
            SoundController.Instance.PlayAudio(SoundController.AudioKey.CollectScore);

            for (int i = 0; i < treasuresList.Count; i++)
            {
                var treasure = treasuresList[i];
                m_playerData.AddScore(treasure.GetValue());
                StartCoroutine(ShowScore(i, treasure.GetValue()));
            }

            m_playerData.ClearInventory();

            GameManager.Instance.AddGuard();
        }
    }

    IEnumerator ShowScore(int index, int value)
    {
        yield return new WaitForSeconds(index * 0.4f);

        var text = Instantiate(m_scoreTextMesh) as TextMesh;
        text.text = value.ToString();
        text.transform.parent = transform;
        text.transform.localPosition = new Vector3(Random.Range(-2.0f, 2.0f), 4.0f, 0);
        text.transform.localScale = new Vector3(0.06047976f, 0.08292421f, 0.08292421f);

        text.transform.DOLocalMoveY(6.0f, 1.0f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(text.gameObject);
        });
    }

    void OnInventoryItemSelected(int index)
    {
        ThrowTreasure(m_playerData.GetTreasureDataByIndex(index));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectZone"))
        {
            CollectRewards();
        }
        else if (other.CompareTag("Treasure") || other.CompareTag("TreasureMisplaced"))
        { 
            UIManager.Instance.ShowInfoTooltip(other.GetComponent<TreasureData>(), other.transform.position);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (InputManager.Instance.GetButtonDown(InputManager.Button.PickItem) && (other.gameObject.CompareTag("Treasure") || other.gameObject.CompareTag("TreasureMisplaced")))
        {
            PickTreasure(other.GetComponent<TreasureData>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SpawnZone"))
        {
            GameManager.Instance.StartTimer();
        }
        else if (other.CompareTag("Treasure") || other.gameObject.CompareTag("TreasureMisplaced"))
        {
            UIManager.Instance.HideInfoTooltip();
        }
    }

    private PlayerData m_playerData;
    private Transform  m_transform;

    public TextMesh m_scoreTextMesh;
}
