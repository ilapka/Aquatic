using System;
using Components.Events;
using Data;
using GoogleMobileAds.Api;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.SDK
{
    public class AddMobSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly AddMobData _addMobData = null;

        private readonly EcsFilter<ShowInterstitialEvent> _showInterstitialFilter = null;

        private BannerView _bannerAd;
        private InterstitialAd _interstitialAd;
        
        public void Init()
        {
            MobileAds.Initialize(initStatus => {});
            LoadBanner();
            LoadInterstitial();
            ShowBanner();
        }

        public void Run()
        {
            foreach (var i in _showInterstitialFilter)
            {
                ShowInterstitial();
            }
        }
        
        private void LoadBanner()
        {
            _bannerAd = new BannerView(_addMobData.bannerId, AdSize.Leaderboard, AdPosition.Bottom);
            var adRequest = new AdRequest.Builder().Build();
            _bannerAd.LoadAd(adRequest);
        }
        
        private void LoadInterstitial()
        {
            _interstitialAd = new InterstitialAd(_addMobData.interstitialId);
            var adRequest = new AdRequest.Builder().Build();
            _interstitialAd.LoadAd(adRequest);
        }
        
        private void ShowBanner()
        {
            if(_bannerAd == null)
                throw new Exception("Banner not loaded");
            _bannerAd.Show();
        }

        private void ShowInterstitial()
        {
            if(_bannerAd == null)
                throw new Exception("Interstitial not loaded");
            _interstitialAd.Show();
        }
    }
}
