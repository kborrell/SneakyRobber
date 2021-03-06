﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplayPanel : UIPanel {

    private void OnScoreChanged(int newAmount)
    {
        m_coinsLabel.text = newAmount.ToString();
    }

    private void OnInventoryChanged(List<TreasureData> newInventory)
    {
        int numItems = newInventory.Count;
        m_inventoryPanel.sizeDelta = new Vector2(45 * newInventory.Count, 40);

        int i = 0;
        while ( i < m_inventorySlots.Count)
        {
            var slot = m_inventorySlots[i];
            slot.gameObject.SetActive(false);

            if (i < numItems)
            {
                slot.SetItem(i, newInventory[i]);
                slot.gameObject.SetActive(true);
            }

            i++;
        }

    }

    private void OnTimerChanged(int secondsLeft)
    {
        int minutes = Mathf.FloorToInt(secondsLeft / 60f);
        int seconds = Mathf.FloorToInt(secondsLeft % 60f);

        m_timerLabel.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void Awake()
    {
        PlayerData.OnCoinsAmountChanged += OnScoreChanged;
        PlayerData.OnInventoryChanged += OnInventoryChanged;
        GameManager.OnTimerChanged += OnTimerChanged;
    }

    private void OnDestroy()
    {
        PlayerData.OnCoinsAmountChanged -= OnScoreChanged;
        PlayerData.OnInventoryChanged -= OnInventoryChanged;
        GameManager.OnTimerChanged -= OnTimerChanged;
    }

    public Text m_coinsLabel;
    public Text m_timerLabel;
    public RectTransform m_inventoryPanel;

    public List<InventoryItem> m_inventorySlots;
}
