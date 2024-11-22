namespace CrowEngine.Core.fsm;

public class StateMachine
{
    public State CurrentState { get; set; }

    
    public void Update(float deltaTime)
    {
        CurrentState?.Update(deltaTime);
    }


    public void ChangeState(State newState)
    {
        if(CurrentState != null)
            CurrentState.OnExit();
        
        CurrentState = newState;
        
        CurrentState?.OnEnter();
    }
}