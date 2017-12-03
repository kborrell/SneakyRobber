using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public void ResetPlayerData()
    {
        m_playerScore = 0;
        ClearInventory();

        NotifyCoinsChange();
    }

    public int GetNumItems()
    {
        return m_inventory.Count;
    }

    public List<TreasureData> GetAllItems()
    {
        return m_inventory;
    }

    public TreasureData GetTreasureDataByIndex(int index)
    {
        if (index < m_inventory.Count)
        {
            return m_inventory[index];
        }

        return null;
    }

    public void AddTreasureToInventory(TreasureData data)
    {
        m_inventory.Add(data);
        AddWeight(data.GetWeight());

        NotifyInventoryChange();
    }

    public void RemoveTreasureFromInventory(int index)
    {
        if (index < m_inventory.Count)
        {
            RemoveTreasureFromInventory(m_inventory[index]);
        }
    }

    public void RemoveTreasureFromInventory(TreasureData data)
    {
        if (m_inventory.Contains(data))
        {
            m_inventory.Remove(data);
            RemoveWeight(data.GetWeight());

            NotifyInventoryChange();
        }
    }

    public void ClearInventory()
    {
        m_inventory.Clear();
        m_currentWeight = 0.0f;

        NotifyInventoryChange();
    }

    public void AddScore(int score)
    {
        m_playerScore += score;

        NotifyCoinsChange();
    }

    public int GetScore()
    {
        return m_playerScore;
    }

    public void AddWeight(float weight)
    {
        m_currentWeight += weight;
    }

    public void RemoveWeight(float weight)
    {
        m_currentWeight -= weight;
    }

    public float GetCurrentWeight()
    {
        return m_currentWeight;
    }

    public float GetCurrentSpeed()
    {
        return m_maxSpeed - (m_currentWeight);
    }

    private void NotifyCoinsChange()
    {
        if (OnCoinsAmountChanged != null)
        {
            OnCoinsAmountChanged(m_playerScore);
        }
    }

    private void NotifyInventoryChange()
    {
        if (OnInventoryChanged != null)
        {
            OnInventoryChanged(GetAllItems());
        }
    }

    private void Awake()
    {
        ResetPlayerData();
    }

    public float m_maxSpeed;

    [SerializeField] private int m_playerScore;
    [SerializeField] private float m_currentWeight;
    private List<TreasureData> m_inventory = new List<TreasureData>();

    public delegate void OnCoinsAmountChangedDelegate(int newAmount);
    public static OnCoinsAmountChangedDelegate OnCoinsAmountChanged;

    public delegate void OnInventoryChangedDelegate(List<TreasureData> newInventory);
    public static OnInventoryChangedDelegate OnInventoryChanged;
}
