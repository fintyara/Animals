using System.Collections.Generic;


namespace CollectCreatures
{
    public interface IState
    {
        #region VAR
        bool Active { get; }
        bool Finish { get; }
        StateTag Tag { get; } 
        float Duration { get; }
        float ElastedTime { get; }
        #endregion
        
        #region FUNC
        void EnterState();
        void State();
        void ExitState();
        #endregion
    }
}
