using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    string UIPath = "Prefabs/UI/Settings/";
    public void OpenVolSettings()
    {
        Instantiate(Resources.Load<GameObject>(UIPath + "VolumenSettings"), this.transform);
    }
    public void OpenProfileSettings()
    {
        Instantiate(Resources.Load<GameObject>(UIPath + "ProfileSettings"), this.transform);
    }
    public void OpenGraphicSettings() 
    {
        Instantiate(Resources.Load<GameObject>(UIPath + "GraphicSettings"), this.transform);
    }
    public void CloseSettings()
    {
        Destroy(this.gameObject);
    }
}
