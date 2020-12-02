using UnityEngine;

namespace Classes
{
    public class ScoreInStageAchievement : Achievement
    {
        private readonly int _numberOfStage;
        private int _currentScore;
        private readonly int _targetScore;

        public ScoreInStageAchievement(int numberOfStage, int targetScore)
        {
            _numberOfStage = numberOfStage;
            _targetScore = targetScore;
        }

        public override bool CheckCondition()
        {
            _currentScore = PlayerPrefs.GetInt("level" + _numberOfStage + "Score",0);
            if (_currentScore >= _targetScore)
            {
                Done = true;
                return true;
            }
            Done = false;
            return false;
        }

        public override int ReturnProgress()
        {
            return CheckCondition() ? _targetScore : _currentScore;
        }
    }
}