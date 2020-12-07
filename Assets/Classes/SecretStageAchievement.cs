using System;
using UnityEditor;

namespace Classes
{
    public class SecretStageAchievement : Achievement
    {
        private int _counter;
        private readonly int _targetScore;
        private int[] _stages;

        public SecretStageAchievement(int targetScore, int[] stages)
        {
            _targetScore = targetScore;
            _stages = stages;
        }
        
        public override bool CheckCondition()
        {
            foreach (var stage in _stages)
            {
                CheckConditionOnStage(stage);
            }

            if (_counter == _stages.Length)
            {
                Done = true;
                return true;
            }
            Done = false;
            return false;
        }

        public override int ReturnProgress()
        {
            return CheckCondition() ? 3 : _counter;
        }

        private void CheckConditionOnStage(int numberOfStage)
        {
            if (Helpers.Methods.GetScoreInStage(numberOfStage) >= _targetScore)
            {
                _counter++;
            }
        }
    }
}