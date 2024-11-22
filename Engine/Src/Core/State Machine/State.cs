namespace CrowEngine.Core.fsm;

public abstract class State
{
    public abstract void Update(float deltaTime);
    public abstract void OnEnter();
    public abstract void OnExit();
}