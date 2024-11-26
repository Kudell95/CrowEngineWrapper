namespace CrowEngine.Core.fsm;

public class StateMachine
{
     private State m_CurrentState { get; set; }

    
    public void Update(float deltaTime)
    {
        m_CurrentState?.Update(deltaTime);
    }


    public void ChangeState(State newState)
    {
        if(m_CurrentState != null)
            m_CurrentState.OnExit();
        
        m_CurrentState = newState;
        
        m_CurrentState?.OnEnter();
    }
}