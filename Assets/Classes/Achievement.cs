namespace Classes
{
    public abstract class Achievement
    {
        public bool Done;
        
        public abstract bool CheckCondition();
        public abstract int ReturnProgress();
    }
}