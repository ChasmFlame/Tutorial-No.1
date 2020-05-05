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
            MOVEMENT_SELECTAGENT, MOVEMENT_SELECTDESTINATION
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

        public SubState Advance()
        {
            CurrentSubState = CurrentSubState++;
            return CurrentSubState;
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
                    if (world.SelectedAgent1 != null) Advance();
                    break;
                case SubState.MOVEMENT_SELECTDESTINATION:
                    break;
                default:
                    break;
            }
        }
    }
}
