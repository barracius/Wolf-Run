using System;
using Classes;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace AchievementsScripts
{
    public class AchievementScript : MonoBehaviour
    {
       public TypeOfAchievement typeOfAchievement;
        public SkinType typeOfReward;
        public int numberOfReward;
        public int numberOfStage;
        public int[] stages;
        private int _target;
        private Achievement _achievement;
        private bool _readyToClaim = false;
        //private bool _claimed = false;
        private GameObject _progressBar;
        private Slider _progress;
        private Text _progressBarText;
        private GameObject _claimText;
        private GameObject _claimedText;

        private void Start()
        {
            Assignations();
            AssignClass();
            ShowProgress();
            CheckIfClaimable();
        }
        
        private void CheckIfClaimable()
        {
            if (!_achievement.Done) return;
            int lockedStatus;
            switch (typeOfReward)
            {
                case SkinType.Wolfie:
                    lockedStatus = PlayerPrefs.GetInt("WolfieSkin" + numberOfReward + "LockedStatus", 0);
                    break;
                case SkinType.Background:
                    lockedStatus = PlayerPrefs.GetInt("BackgroundSkin" + numberOfReward + "LockedStatus", 0);
                    break;
                case SkinType.SecretStage:
                    lockedStatus = PlayerPrefs.GetInt("SecretStageLockedStatus", 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (lockedStatus == 0)
            {
                _readyToClaim = true;
                _claimText.SetActive(true);
            }
            else
            {
                //_claimed = true;
                _claimedText.SetActive(true);
            }
        }

        private void ShowProgress()
        {
            var currentValue = _achievement.ReturnProgress();
            _progress.value = currentValue;
            _progressBarText.text = currentValue + "/" + _progress.maxValue;
        }

        private void Assignations()
        {
            _progressBar = transform.Find("ProgressBar").gameObject;
            _progress = _progressBar.GetComponent<Slider>();
            _target = (int) _progress.maxValue;
            _progressBarText = _progressBar.transform.Find("Text").GetComponent<Text>();
            _claimText = transform.Find("RewardImage").Find("ClaimText").gameObject;
            _claimedText = transform.Find("RewardImage").Find("ClaimedText").gameObject;
        }

        private void AssignClass()
        {
            switch (typeOfAchievement)
            {
                case TypeOfAchievement.StageCompletion:
                    _achievement = new StageCompletionAchievement(numberOfStage);
                    break;
                case TypeOfAchievement.TotalStars:
                    _achievement = new TotalStarsAchievement(_target);
                    break;
                case TypeOfAchievement.ScoreInStage:
                    _achievement = new ScoreInStageAchievement(numberOfStage, _target);
                    break;
                case TypeOfAchievement.SecretStage:
                    _achievement = new SecretStageAchievement(_target, stages);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnClaimClick()
        {
            if (!_readyToClaim) return;
            switch (typeOfReward)
            {
                case SkinType.Wolfie:
                    PlayerPrefs.SetInt("WolfieSkin" + numberOfReward + "LockedStatus", 1);
                    break;
                case SkinType.Background:
                    PlayerPrefs.SetInt("BackgroundSkin" + numberOfReward + "LockedStatus", 1);
                    break;
                case SkinType.SecretStage:
                    PlayerPrefs.SetInt("SecretStageLockedStatus", 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                
            }

            _readyToClaim = false;
            //_claimed = true;
            _claimText.SetActive(false);
            _claimedText.SetActive(true);
        }
    }
}
