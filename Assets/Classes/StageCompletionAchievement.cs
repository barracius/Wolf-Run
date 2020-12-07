using UnityEngine;

namespace Classes
{
    public class StageCompletionAchievement : Achievement
    {
        private readonly int _numberOfStage;
        private int _currentScore;

        public StageCompletionAchievement(int numberOfStage)
        {
            _numberOfStage = numberOfStage;
        }

        public override bool CheckCondition()
        {
            _currentScore = Helpers.Methods.GetScoreInStage(_numberOfStage);
            if (_currentScore > 0)
            {
                Done = true;
                return true;
            }
            Done = false;
            return false;
            
        }

        public override int ReturnProgress()
        {
            return CheckCondition() ? 1 : 0;
        }
    }
}