using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class HJGameManager
    {
        private static HJGameManager mInstance;
        private HJGameState mCurrentGameState;

        public static HJGameManager Instance()
        {
            return mInstance = mInstance == null ? new HJGameManager() : mInstance;
        }

        public HJGameState CurrentGameState
        {
            get { return mCurrentGameState; }
            set { mCurrentGameState = value; }

        }
    }
}