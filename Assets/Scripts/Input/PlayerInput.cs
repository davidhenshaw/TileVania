
public abstract class PlayerInput : ICharacterInput
{
    public virtual float Horizontal { get; }
    public virtual float Vertical { get; }
    public virtual bool JumpPressed { get; }
    public virtual bool JumpReleased { get; } 
    public virtual bool InteractPressed { get; }
    public virtual bool InteractReleased { get; }
}
