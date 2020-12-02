using System;
using UnityEngine;

namespace Classes
{
    public class TotalStarsAchievement : Achievement
    {
        private readonly int _targetStars;
        private int _currentStars;
        
        public TotalStarsAchievement(int targetStars)
        {
            _targetStars = targetStars;
        }

        public override bool CheckCondition()
        {
            _currentStars = Helpers.Methods.CalculateTotalStars();
            if (_currentStars >= _targetStars)
            {
                Done = true;
                return true;
            }
            Done = false;
            return false;
        }

        public override int ReturnProgress()
        {
            return CheckCondition() ? _targetStars : _currentStars;
        }
    }
}