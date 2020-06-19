public interface IMenuInput
{
    float Horizontal { get; }
    float Vertical { get; }
    bool SelectDown { get; }
    bool SelectUp { get; }
    bool DeselectDown { get; }
    bool DeselectUp { get; }
}
