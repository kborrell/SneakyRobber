using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureData : MonoBehaviour {

	public int GetValue()
    {
        return m_value;
    }

    public float GetWeight()
    {
        return m_weight;
    }

    public Sprite GetItemSprite()
    {
        return m_itemSprite;
    }

    private void Awake()
    {
        gameObject.tag = "Treasure";
    }

    [SerializeField] private int m_value;
    [SerializeField] private float m_weight;
    [SerializeField] private Sprite m_itemSprite;
}
