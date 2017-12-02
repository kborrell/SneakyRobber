using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour {

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();

        m_directionsMap[InputManager.Button.Right] = Vector3.right;
        m_directionsMap[InputManager.Button.Left] = Vector3.left;
        m_directionsMap[InputManager.Button.Up] = Vector3.forward;
        m_directionsMap[InputManager.Button.Down] = Vector3.back;
    }

    private void Update()
    {
        if (InputManager.Instance.GetButtonDown(InputManager.Button.Right))
        {
            m_pressedButtons.Push(InputManager.Button.Right);
            m_currentDirection = m_directionsMap[InputManager.Button.Right];
        }
        else if (InputManager.Instance.GetButtonDown(InputManager.Button.Left))
        {
            m_pressedButtons.Push(InputManager.Button.Left);
            m_currentDirection = m_directionsMap[InputManager.Button.Left];
        }
        else if (InputManager.Instance.GetButtonDown(InputManager.Button.Up))
        {
            m_pressedButtons.Push(InputManager.Button.Up);
            m_currentDirection = m_directionsMap[InputManager.Button.Up];
        }
        else if (InputManager.Instance.GetButtonDown(InputManager.Button.Down))
        {
            m_pressedButtons.Push(InputManager.Button.Down);
            m_currentDirection = m_directionsMap[InputManager.Button.Down];
        }

        if (m_pressedButtons.Count > 0 && InputManager.Instance.GetButtonUp(m_pressedButtons.Peek()))
        {
            while (m_pressedButtons.Count > 0 && !InputManager.Instance.GetButton(m_pressedButtons.Peek()))
            {
                m_pressedButtons.Pop();
            }

            if (m_pressedButtons.Count > 0)
            {
                m_currentDirection = m_directionsMap[m_pressedButtons.Peek()];
            }
            else
            {
                m_currentDirection = Vector3.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        m_rigidBody.MovePosition(m_transform.position + m_currentDirection * m_playerSpeed * Time.deltaTime);
    }

    private void OnGUI()
    {
        printInputStackStatus();
    }

    void printInputStackStatus()
    {
        string status = "STACK: [";
        if (m_pressedButtons.Count > 0)
        {
            foreach (InputManager.Button button in m_pressedButtons)
            {
                status = status + button.ToString() + ", ";
            }
        }
        status = status + "]";
        GUILayout.Label(status);
    }

    public float m_playerSpeed;
    public Dictionary<InputManager.Button, Vector3> m_directionsMap = new Dictionary<InputManager.Button, Vector3>();

    private Stack<InputManager.Button> m_pressedButtons = new Stack<InputManager.Button>();
    private Vector3   m_currentDirection;

    private Transform m_transform;
    private Rigidbody m_rigidBody;
}
