
public interface IPlayerInput : ICharacterInput
{
    bool JumpPressed { get; }
    bool JumpReleased { get; } 
    bool InteractPressed { get; }
    bool InteractReleased { get; }
    bool PouncePressed { get; }
    bool PounceReleased { get; }
    bool ClimbPressed { get; }
    bool ClimbReleased { get; }
}
