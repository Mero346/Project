using GoogleMobileAds.Api;
using System;
using UnityEngine;
using TMPro;

public class Admob : MonoBehaviour
{
    private InterstitialAd _interstitial;
    private RewardedAd _rewardedAd;

    private float _coins;
    private int _adTypeNumber;

    [SerializeField] private TextMeshProUGUI _coinsText;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-6695891168184997/9653310032";
        string testAdUnitId = "ca-app-pub-3940256099942544/1033173712";

        _interstitial = new InterstitialAd(testAdUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }

    private void RequestRewarded()
    {
        string adUnitId = "ca-app-pub-6695891168184997/6041368083";
        string testAdUnitId = "ca-app-pub-3940256099942544/5224354917";

        _rewardedAd = new RewardedAd(testAdUnitId);

        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        Debug.Log("Дать награду");
        switch (_adTypeNumber)
        {
            case 0:
                _coins += 1000;
                break;
            case 1:
                _coins += 5000;
                break;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        _coinsText.text = _coins.ToString();
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
    }
    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
    }
    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {
    }

    public void ShowAd()
    {
        RequestInterstitial();

        if (_interstitial.IsLoaded())
        {
            _interstitial.Show();
        }
    }

    public void ShowRewardedAd(int typeNumber)
    {
        RequestRewarded();

        _adTypeNumber = typeNumber;
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }
}
