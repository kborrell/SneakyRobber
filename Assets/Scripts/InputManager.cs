﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    public enum Button
    {
        None,
        Left,
        Right,
        Up,
        Down,
        PickItem,
        ThrowItem
    }

    public void SetInputEnabled(bool enabled)
    {
        m_inputEnabled = enabled;
    }

    public bool GetButton(Button button)
    {
        if (!m_inputEnabled)
        {
            return false;
        }

        if (button == Button.Left)
        {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        }
        else if (button == Button.Right)
        {
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        }
        else if (button == Button.Up)
        {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        }
        else if (button == Button.Down)
        {
            return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        }
        else if (button == Button.PickItem)
        {
            return Input.GetKey(KeyCode.Space);
        }
        else if (button == Button.ThrowItem)
        {
            return Input.GetKey(KeyCode.F);
        }

        return false;
    }

    public bool GetButtonUp(Button button)
    {
        if (!m_inputEnabled)
        {
            return false;
        }

        if (button == Button.Left)
        {
            return Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow);
        }
        else if (button == Button.Right)
        {
            return Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);
        }
        else if (button == Button.Up)
        {
            return Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
        }
        else if (button == Button.Down)
        {
            return Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow);
        }
        else if (button == Button.PickItem)
        {
            return Input.GetKeyUp(KeyCode.Space);
        }
        else if (button == Button.ThrowItem)
        {
            return Input.GetKeyUp(KeyCode.F);
        }

        return false;
    }

    public bool GetButtonDown(Button button)
    {
        if (!m_inputEnabled)
        {
            return false;
        }

        if (button == Button.Left)
        {
            return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        }
        else if (button == Button.Right)
        {
            return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        }
        else if (button == Button.Up)
        {
            return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        }
        else if (button == Button.Down)
        {
            return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        }
        else if (button == Button.PickItem)
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        else if (button == Button.ThrowItem)
        {
            return Input.GetKeyDown(KeyCode.F);
        }

        return false;
    }

    private InputManager()
    {

    }

    private bool m_inputEnabled = false;
    private Stack<KeyCode> m_pressedKeys = new Stack<KeyCode>();

    public static InputManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new InputManager();
            }

            return s_instance;
        }
    }

    private static InputManager s_instance;
}
