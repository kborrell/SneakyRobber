using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

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

    [SerializeField] private float m_currentWeight;

    public float m_maxSpeed;
}
