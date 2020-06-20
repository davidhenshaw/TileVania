using UnityEngine;

public class KeyboardInput : IPlayerInput, IMenuInput
{
    public bool JumpPressed { get => Input.GetButtonDown("Jump"); }
    public bool JumpReleased { get => Input.GetButtonUp("Jump"); }

    public bool InteractPressed { get => Input.GetButtonDown("Interact"); }
    public bool InteractReleased { get => Input.GetButtonUp("Interact"); }

    public float Horizontal { get => Input.GetAxis("Horizontal"); }
    public float Vertical { get => Input.GetAxis("Vertical"); }

    public bool SelectDown { get => Input.GetKeyDown(KeyCode.Return); }
    public bool SelectUp { get => Input.GetKeyUp(KeyCode.Return); }

    public bool DeselectDown { get => Input.GetKeyDown(KeyCode.Escape); }
    public bool DeselectUp { get => Input.GetKeyUp(KeyCode.Escape); }

    public bool PouncePressed { get => Input.GetKeyDown(KeyCode.Q); }
    public bool PounceReleased { get => Input.GetKeyUp(KeyCode.Q); }

    public bool ClimbPressed { get => Input.GetButtonDown("Vertical"); }
    public bool ClimbReleased { get => Input.GetButtonUp("Vertical"); }
}
