using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public enum HJPlatformSliceType
    {
        eNone,
        eRegularSlice,
        eDisabledSlice,
        eDeathSlice,
        eGoalSlice
    }
    //----------------------------------------------------------------------------------------------------

    public enum HJGameState
    {
        eGameInitializing,
        eGamePlaying,
        eGameTryMode,
        eGamePlayerDeath,
        eGameLevelCompleteMode,
        eGameEnd
    }
    //----------------------------------------------------------------------------------------------------

}