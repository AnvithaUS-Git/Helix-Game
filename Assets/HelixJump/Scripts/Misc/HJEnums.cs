using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public enum HJPlatformSliceType
    {
        eRegularSlice,
        eDisabledSlice,
        eDeathSlice
    }

    public enum HJGameState
    {
        eGameInitializing,
        eGamePlaying,
        eGamePlayerDeath,
        eGameEnd
    }
}