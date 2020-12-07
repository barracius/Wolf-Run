namespace Helpers
{
    public enum WolfieState
    {
        Biting,
        Jumping,
        Sliding,
        Running,
        Stunned,
        OnFire
    }

    public enum Sounds
    {
        JumpSound,
        BiteSound,
        SlideSound,
        BittenTreeSound,
        ShieldSound,
        LossGameSound
    }

    public enum SkinType
    {
        Wolfie,
        Background,
        SecretStage
    }

    public enum GobalVariables
    {
        NumberOfStages = 3
    }

    public enum TypeOfAchievement
    {
        ScoreInStage,
        StageCompletion,
        TotalStars,
        SecretStage
    }

    public enum MuteSetting
    {
        Muted,
        Unmuted
    }

}