using RoR2;

namespace Bastian
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, null)]
    public class BastianMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = MainPlugin.SURVIVORNAMEKEY + "masteryAchievement";
        public const string unlockableIdentifier = MainPlugin.SURVIVORNAMEKEY + "masteryUnlockable";

        public override string RequiredCharacterBody => MainPlugin.SURVIVORNAME + "Body";

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}