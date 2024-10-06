using System.Collections.Generic;

[System.Serializable]
public class Zone
{
    public string zoneName;
    public List<Clean> blood;
    public int bloodCurrentAmmount = 0;
    public List<DisposableObject> corpses;
    public int corpsesCurrentAmmount = 0;
    public List<Clean> uvCleanables;
    public int uvCleanablesCurrentAmmount = 0;
    public List<Clean> bloodyObjects;
    public int bloodyObjectsCurrentAmmount = 0;
    public List<SnappableObject> arrabgables;
    public int arrabgablesCurrentAmmount = 0;
    public List<RetrievableObject> weapons;
    public int weaponsCurrentAmmount = 0;
    public List<RetrievableObject> documents;
    public int documentsCurrentAmmount = 0;
    public List<RetrievableObject> clothes;
    public int clothesCurrentAmmount = 0;
    public List<RetrievableObject> miscellaneous;
    public int miscellaneousCurrentAmmount = 0;
}
