using System.Collections;
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
        Action
    }

    public bool GetButton(Button button)
    {
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
        else if (button == Button.Action)
        {
            return Input.GetKey(KeyCode.E);
        }

        return false;
    }

    public bool GetButtonUp(Button button)
    {
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
        else if (button == Button.Action)
        {
            return Input.GetKeyUp(KeyCode.E);
        }

        return false;
    }

    public bool GetButtonDown(Button button)
    {
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
        else if (button == Button.Action)
        {
            return Input.GetKeyDown(KeyCode.E);
        }

        return false;
    }

    private InputManager()
    {

    }

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
