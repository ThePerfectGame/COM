public interface IUse
{
    string Name { get; }
    bool IsActive { get; }
    void Use();
}
