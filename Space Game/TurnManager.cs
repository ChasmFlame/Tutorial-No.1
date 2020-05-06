using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Space_Game
{
    public class TurnManager
    {
        public enum State 
        { 
            MOVEMENT, ACTION, UPKEEP
        }
        public enum SubState
        {
            MOVEMENT_SELECTAGENT, MOVEMENT_SELECTDESTINATION, MOVEMENT_FINISHED,
                ACTION_SPENDPOINTS, ACTION_FINISHED
        }

        #region Fields
        State CurrentState;
        SubState CurrentSubState;
        #endregion

        public TurnManager()
        {
            CurrentState = State.MOVEMENT;
            CurrentSubState = SubState.MOVEMENT_SELECTAGENT;
        }

        

        public SubState GetSubState()
        {
            return CurrentSubState;
        }

        public string GetStateMessage()
        {
            switch (CurrentSubState)
            {
                case SubState.MOVEMENT_SELECTAGENT:
                    return "Please select an agent to move.";
                case SubState.MOVEMENT_SELECTDESTINATION:
                    return "Please select a tile to move to.";
                default:
                    return "";
            }
        }

        internal void CheckStateChanges(World world)
        {
            switch (CurrentSubState)
            {
                case SubState.MOVEMENT_SELECTAGENT:
                    if (world.SelectedAgent != null) CurrentSubState = SubState.MOVEMENT_SELECTDESTINATION;
                    break;
                case SubState.MOVEMENT_SELECTDESTINATION:
                    break;
                default:
                    break;
            }
            switch (CurrentState)
            {
                case State.MOVEMENT:
					if(world.MovementComplete())
                    {
                        CurrentState = State.ACTION;
                        CurrentSubState = SubState.ACTION_SPENDPOINTS;
					}
                    break;
                case State.ACTION:
                    break;

            }
        }
    }
}
