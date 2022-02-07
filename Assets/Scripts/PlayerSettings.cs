
public class PlayerSettings
{
    #region Public variables
    
    public float ForceMultiplier;

    public float MovespeedMax;
    
    public int BackpackSize;

    #endregion

    #region Constructors
        
    public PlayerSettings()
    {
        ResetToDefault();
    }

    #endregion

    #region Public methods
        
    public void ResetToDefault()
    {
        ForceMultiplier = 2000.0f;
        MovespeedMax = 10.0f;
        BackpackSize = 5;
    }

    #endregion
}