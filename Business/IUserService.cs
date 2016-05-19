namespace Services
{
    /// <summary>
    /// Hi, I'm UserService interface.
    /// Procedural programmers love interfaces. They write loosely coupled systems with our help, don't they?
    /// </summary>
    public interface IUserService
    {
        bool Activate(int id);

        bool Deactivate(int id);

        bool LevelUp(int id);
    }
}
