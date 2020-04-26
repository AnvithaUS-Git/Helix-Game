﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HJGameConstants
{

    public const string kConfigFilePath = @"Config/helix_jump_config";
    public const string kBestScoreText = "Best : {0}";
   
    public const int kTotalSlicesInPlatform = 12;
    public const float kFirstPlatformYPos = 0.9f;


    //HJPlatformDetails
    public const string kBaseSliceCountKey = "bs_cnt";
    public const string kDeathSliceCountKey = "dth_cnt";

    //HJLevelDetails
    public const string kLevelKey = "lvl_id";
    public const string kDistanceKey = "dis_plt";
    public const string kBackgroundColorKey = "bg_clr";
    public const string kPoleColorKey = "pl_clr";
    public const string kBasePlatformColorKey = "bs_clr";
    public const string kBallColorKey = "ball_clr";
    public const string kScoreUnitKey = "scr_unt";
    public const string kPlatformDetailKey = "plt_dtl";
}
