using UnityEngine;

public class KeyboardInput : PlayerInput, IMenuInput
{
    public override bool JumpPressed { get => Input.GetButtonDown("Jump"); }

    public override bool JumpReleased { get => Input.GetButtonUp("Jump"); }

    public override bool InteractPressed { get => Input.GetButtonDown("Interact"); }

    public override bool InteractReleased { get => Input.GetButtonUp("Interact"); }

    public override float Horizontal { get => Input.GetAxis("Horizontal"); }

    public override float Vertical { get => Input.GetAxis("Vertical"); }

    public bool SelectDown { get => Input.GetKeyDown(KeyCode.Return); }

    public bool SelectUp { get => Input.GetKeyUp(KeyCode.Return); }

    public bool DeselectDown { get => Input.GetKeyDown(KeyCode.Escape); }

    public bool DeselectUp { get => Input.GetKeyUp(KeyCode.Escape); }
}
