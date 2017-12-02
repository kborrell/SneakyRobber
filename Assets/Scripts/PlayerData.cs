using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public void ResetPlayerData()
    {
        m_playerScore = 0;
        ClearInventory();
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
        }
    }

    public void ClearInventory()
    {
        m_inventory.Clear();
        m_currentWeight = 0.0f;
    }

    public void AddScore(int score)
    {
        m_playerScore += score;
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

    public float m_maxSpeed;

    [SerializeField] private int m_playerScore;
    [SerializeField] private float m_currentWeight;
    private List<TreasureData> m_inventory = new List<TreasureData>();
}
