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
        if (m_allowDiagonals)
        {
            float timeFactor = Time.deltaTime * m_playerSpeed;

            m_newDirection = Vector3.zero;
            if (InputManager.Instance.GetButton(InputManager.Button.Right) || InputManager.Instance.GetButton(InputManager.Button.Left))
            {
                m_newDirection.x = InputManager.Instance.GetButton(InputManager.Button.Right) ? 1 : -1;
            }
            if (InputManager.Instance.GetButton(InputManager.Button.Up) || InputManager.Instance.GetButton(InputManager.Button.Down))
            {
                m_newDirection.z = InputManager.Instance.GetButton(InputManager.Button.Up) ? 1 : -1;
            }

            m_currentDirection = Vector3.Lerp(m_currentDirection, m_newDirection.normalized * timeFactor, Time.deltaTime * m_smoothValue);
        }
        else
        {
            if (InputManager.Instance.GetButtonDown(InputManager.Button.Right))
            {
                m_pressedButtons.Push(InputManager.Button.Right);
                m_newDirection = m_directionsMap[InputManager.Button.Right];
            }
            else if (InputManager.Instance.GetButtonDown(InputManager.Button.Left))
            {
                m_pressedButtons.Push(InputManager.Button.Left);
                m_newDirection = m_directionsMap[InputManager.Button.Left];
            }
            else if (InputManager.Instance.GetButtonDown(InputManager.Button.Up))
            {
                m_pressedButtons.Push(InputManager.Button.Up);
                m_newDirection = m_directionsMap[InputManager.Button.Up];
            }
            else if (InputManager.Instance.GetButtonDown(InputManager.Button.Down))
            {
                m_pressedButtons.Push(InputManager.Button.Down);
                m_newDirection = m_directionsMap[InputManager.Button.Down];
            }

            if (m_pressedButtons.Count > 0 && InputManager.Instance.GetButtonUp(m_pressedButtons.Peek()))
            {
                while (m_pressedButtons.Count > 0 && !InputManager.Instance.GetButton(m_pressedButtons.Peek()))
                {
                    m_pressedButtons.Pop();
                }

                if (m_pressedButtons.Count > 0)
                {
                    m_newDirection = m_directionsMap[m_pressedButtons.Peek()];
                }
                else
                {
                    m_newDirection = Vector3.zero;
                }
            }

            if (m_currentDirection == Vector3.zero || m_newDirection == Vector3.zero)
            {
                m_currentDirection = Vector3.Lerp(m_currentDirection, m_newDirection, Time.deltaTime * m_smoothValue);
            }
            else
            {
                m_currentDirection = m_newDirection;
            }
        }
    }

    private void FixedUpdate()
    {
        m_rigidBody.MovePosition(m_transform.position + m_currentDirection);
        m_transform.GetChild(0).localRotation = Quaternion.LookRotation(m_currentDirection);
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

    public bool  m_allowDiagonals;
    public float m_smoothValue;
    public float m_playerSpeed;
    public Dictionary<InputManager.Button, Vector3> m_directionsMap = new Dictionary<InputManager.Button, Vector3>();

    private Stack<InputManager.Button> m_pressedButtons = new Stack<InputManager.Button>();
    private Vector3   m_currentDirection;
    private Vector3   m_newDirection;

    private Transform  m_transform;
    private Rigidbody  m_rigidBody;
}
