using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

namespace HelixJump
{
    public class AdsManager : MonoBehaviour
    {
        void Start()
        {
            RequestInterstitial();
            //RequestRewardedAds();
            RequestBannerAds();
        }

        private static AdsManager mInstance;

        private void Awake()
        {
            if (mInstance == null)
            {
                mInstance = this;
            }
        }
        public static AdsManager GetInstance()
        {
            return mInstance;
        }

        void Update()
        {

        }

        #region InterstitialAd
        private InterstitialAd interstitial;

        public void RequestInterstitial()
        {
#if UNITY_ANDROID
            //Live..
            //string adUnitId = "ca-app-pub-6160291948538914/7427004931";

            //ca-app-pub-3940256099942544/1033173712
            //string adUnitId = "ca-app-pub-3940256099942544/8691691433";
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";

#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

            // Initialize an InterstitialAd.
            this.interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this.interstitial.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            this.interstitial.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            this.interstitial.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.interstitial.LoadAd(request);
        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "+ args.Message);
            HJGameEventHandler.Instance().TriggerOnAdFailedToShowEvent();
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
            RequestInterstitial();
            HJGameEventHandler.Instance().TriggerOnAdClosedEvent();
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }



        public void ShowInterstialAds()
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }
        }
        #endregion


        #region InterstitialAd
        private RewardedAd rewardedAd;


        public void RequestRewardedAds()
        {
            string adUnitId;
#if UNITY_ANDROID
            //adUnitId = "ca-app-pub-6160291948538914/7069225368";

            adUnitId = "";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

            this.rewardedAd = new RewardedAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(request);
        }

        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
            RequestRewardedAds();
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: "
                                 + args.Message);

            RequestRewardedAds();
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
            RequestRewardedAds();
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                                 + args.Message);

            RequestRewardedAds();
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdClosed event received");
            RequestRewardedAds();
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print(
                "HandleRewardedAdRewarded event received for "
                            + amount.ToString() + " " + type);


        }


        public void ShowRewardedAds()
        {
            if (this.rewardedAd.IsLoaded())
            {
                this.rewardedAd.Show();
            }
        }
        #endregion


        #region BannerAd
        private BannerView bannerView;

        private void RequestBannerAds()
        {

#if UNITY_ANDROID
            //string adUnitId = "ca-app-pub-6160291948538914/5798228494";

            //test
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

            this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

            // Called when an ad request has successfully loaded.
            this.bannerView.OnAdLoaded += this.HandleOnAdLoadedBanner;
            // Called when an ad request failed to load.
            this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoadBanner;
            // Called when an ad is clicked.
            this.bannerView.OnAdOpening += this.HandleOnAdOpenedBanner;
            // Called when the user returned from the app after an ad click.
            this.bannerView.OnAdClosed += this.HandleOnAdClosedBanner;
            // Called when the ad click caused the user to leave the application.
            this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplicationBanner;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            this.bannerView.LoadAd(request);
        }

        public void HandleOnAdLoadedBanner(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoadBanner(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.Message);
        }

        public void HandleOnAdOpenedBanner(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosedBanner(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        public void HandleOnAdLeavingApplicationBanner(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
        #endregion

    }
}