using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HelixJump
{
	public class HJGameManager : MonoBehaviour
	{
		private static HJGameManager mInstance;
		private HJGameState mCurrentGameState;

		private void Awake()
		{
			mInstance = this;
		}
		//--------------------------------------------------------------------------------------------------------------------------------------------------------

		public static HJGameManager Instance()
		{
			return mInstance;
		}
		//--------------------------------------------------------------------------------------------------------------------------------------------------------

		public HJGameState CurrentGameState
		{
			get { return mCurrentGameState; }
			set { mCurrentGameState = value; }

		}
		//--------------------------------------------------------------------------------------------------------------------------------------------------------
		public void ShowInterstialAd(Action<bool> isAdShown)
		{
#if UNITY_EDITOR
			isAdShown?.Invoke(false);
#elif UNITY_ANDROID
			StartCoroutine(CheckForInternet(resp =>
			{
				if (resp)
				{
					AdsManager.GetInstance().ShowInterstialAds();
					isAdShown?.Invoke(true);
				}
				else
				{
					isAdShown?.Invoke(false);
				}
			}));
#endif
		}
		//--------------------------------------------------------------------------------------------------------------------------------------------------------
		public IEnumerator CheckForInternet(Action<bool> inCall)
		{
			WaitForSeconds wait = new WaitForSeconds(0.1f);

			bool internetPossiblyAvailable = false;

			switch (Application.internetReachability)
			{
				case NetworkReachability.ReachableViaLocalAreaNetwork:
					internetPossiblyAvailable = true;
					break;
				case NetworkReachability.ReachableViaCarrierDataNetwork:
					internetPossiblyAvailable = true;
					break;
				default:
					internetPossiblyAvailable = false;
					break;
			}
			if (internetPossiblyAvailable)
			{
				Ping pingServer = new Ping("8.8.8.8");

				float startTime = Time.time;

				while (!pingServer.isDone && Time.time < startTime)
				{
					yield return wait;
				}
				if (pingServer.isDone)
				{
					int pingTime = pingServer.time;

					if (inCall != null)
						inCall(true);

					pingServer.DestroyPing();
				}
				else
				{
					WWW checkInternet = new WWW("https://www.google.co.in");
					if (checkInternet.error == null)
					{
						if (inCall != null)
							inCall(true);
					}
					else
					{
						if (inCall != null)
							inCall(false);
					}
					checkInternet.Dispose();

					pingServer.DestroyPing();
				}
			}
			else
			{
				if (inCall != null)
					inCall(false);
			}
		}
		//--------------------------------------------------------------------------------------------------------------------------------------------------------
		public void OnRestartSameLevel()
		{
			CurrentGameState = HJGameState.eGamePlaying;
			HJPlayerScoreAndLevelManager.Instance().RestartCurrentLevel();
			HJGameEventHandler.Instance().TriggerRetrySameLevelEvent();
		}
	}
}