using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabletUIManager : MonoBehaviour
{
    #region SCRIPTS
    [Header("Config")]
    [SerializeField] private GameStateManager gameStateManager = null;
    #endregion
    #region CONTENT
    [SerializeField] private ScrollRect scrollRect;
    #endregion
    #region AUDIO
    [Header("Audio")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;
    [SerializeField] private string crossOutEvent = null;
    #endregion
    #region BAR VARIABLES
    [Header("Bar")]
    [SerializeField] private const string barName = "Bar";
    [SerializeField] private Button barButton = null;
    [SerializeField] private GameObject barPanel = null;
    [Header("Bar Blood")]
    [SerializeField] private Image barBloodTickImage = null;
    [SerializeField] private Image barBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barBloodProgressText = null;

    [Header("Bar Corpses")]
    [SerializeField] private Image barCorpsesTickImage = null;
    [SerializeField] private Image barCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barCorpsesProgressText = null;

    [Header("Bar UV Cleanables")]
    [SerializeField] private Image barUVCleanablesTickImage = null;
    [SerializeField] private Image barUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barUVCleanablesProgressText = null;

    [Header("Bar Bloody Objects")]
    [SerializeField] private Image barBloodyObjectsTickImage = null;
    [SerializeField] private Image barBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barBloodyObjectsProgressText = null;   

    [Header("Bar Arrangables")]
    [SerializeField] private Image barArrangablesTickImage = null;
    [SerializeField] private Image barArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barArrangablesProgressText = null;

    [Header("Bar Weapons")]
    [SerializeField] private Image barWeaponsTickImage = null;
    [SerializeField] private Image barWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barWeaponsProgressText = null;

    [Header("Bar Documents")]
    [SerializeField] private Image barDocumentsTickImage = null;
    [SerializeField] private Image barDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barDocumentsProgressText = null;

    [Header("Bar Clothes")]
    [SerializeField] private Image barClothesTickImage = null;
    [SerializeField] private Image barClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barClothesProgressText = null;

    [Header("Bar Miscellaneous")]
    [SerializeField] private Image barMiscellaneousTickImage = null;
    [SerializeField] private Image barMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barMiscellaneousProgressText = null;

    #endregion
    #region MEN'S BATHROOM VARIABLES
    [Header("Men's Bathroom")]
    [SerializeField] private const string menBathroomName = "Men's Bathroom";
    [SerializeField] private Button menBathroomButton = null;
    [SerializeField] private GameObject menBathroomPanel = null;
    [Header("Men's Bathroom Blood")]
    [SerializeField] private Image menBathroomBloodTickImage = null;
    [SerializeField] private Image menBathroomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomBloodProgressText = null;

    [Header("Men's Bathroom Corpses")]
    [SerializeField] private Image menBathroomCorpsesTickImage = null;
    [SerializeField] private Image menBathroomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomCorpsesProgressText = null;

    [Header("Men's Bathroom UV Cleanables")]
    [SerializeField] private Image menBathroomUVCleanablesTickImage = null;
    [SerializeField] private Image menBathroomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomUVCleanablesProgressText = null;

    [Header("Men's Bathroom Bloody Objects")]
    [SerializeField] private Image menBathroomBloodyObjectsTickImage = null;
    [SerializeField] private Image menBathroomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomBloodyObjectsProgressText = null;

    [Header("Men's Bathroom Arrangables")]
    [SerializeField] private Image menBathroomArrangablesTickImage = null;
    [SerializeField] private Image menBathroomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomArrangablesProgressText = null;

    [Header("Men's Bathroom Weapons")]
    [SerializeField] private Image menBathroomWeaponsTickImage = null;
    [SerializeField] private Image menBathroomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomWeaponsProgressText = null;

    [Header("Men's Bathroom Documents")]
    [SerializeField] private Image menBathroomDocumentsTickImage = null;
    [SerializeField] private Image menBathroomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomDocumentsProgressText = null;

    [Header("Men's Bathroom Clothes")]
    [SerializeField] private Image menBathroomClothesTickImage = null;
    [SerializeField] private Image menBathroomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomClothesProgressText = null;

    [Header("Men's Bathroom Miscellaneous")]
    [SerializeField] private Image menBathroomMiscellaneousTickImage = null;
    [SerializeField] private Image menBathroomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomMiscellaneousProgressText = null;

    #endregion
    #region WOMEN'S BATHROOM VARIABLES
    [Header("Women's Bathroom")]
    [SerializeField] private const string womenBathroomName = "Women's Bathroom";
    [SerializeField] private Button womenBathroomButton = null;
    [SerializeField] private GameObject womenBathroomPanel = null;
    [Header("Women's Bathroom Blood")]
    [SerializeField] private Image womenBathroomBloodTickImage = null;
    [SerializeField] private Image womenBathroomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomBloodProgressText = null;

    [Header("Women's Bathroom Corpses")]
    [SerializeField] private Image womenBathroomCorpsesTickImage = null;
    [SerializeField] private Image womenBathroomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomCorpsesProgressText = null;

    [Header("Women's Bathroom UV Cleanables")]
    [SerializeField] private Image womenBathroomUVCleanablesTickImage = null;
    [SerializeField] private Image womenBathroomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomUVCleanablesProgressText = null;

    [Header("Women's Bathroom Bloody Objects")]
    [SerializeField] private Image womenBathroomBloodyObjectsTickImage = null;
    [SerializeField] private Image womenBathroomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomBloodyObjectsProgressText = null;

    [Header("Women's Bathroom Arrangables")]
    [SerializeField] private Image womenBathroomArrangablesTickImage = null;
    [SerializeField] private Image womenBathroomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomArrangablesProgressText = null;

    [Header("Women's Bathroom Weapons")]
    [SerializeField] private Image womenBathroomWeaponsTickImage = null;
    [SerializeField] private Image womenBathroomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomWeaponsProgressText = null;

    [Header("Women's Bathroom Documents")]
    [SerializeField] private Image womenBathroomDocumentsTickImage = null;
    [SerializeField] private Image womenBathroomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomDocumentsProgressText = null;

    [Header("Women's Bathroom Clothes")]
    [SerializeField] private Image womenBathroomClothesTickImage = null;
    [SerializeField] private Image womenBathroomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomClothesProgressText = null;

    [Header("Women's Bathroom Miscellaneous")]
    [SerializeField] private Image womenBathroomMiscellaneousTickImage = null;
    [SerializeField] private Image womenBathroomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomMiscellaneousProgressText = null;

    #endregion
    #region KITCHEN VARIABLES
    [Header("Kitchen")]
    [SerializeField] private const string kitchenName = "Kitchen";
    [SerializeField] private Button kitchenButton = null;
    [SerializeField] private GameObject kitchenPanel = null;
    [Header("Kitchen Blood")]
    [SerializeField] private Image kitchenBloodTickImage = null;
    [SerializeField] private Image kitchenBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenBloodProgressText = null;

    [Header("Kitchen Corpses")]
    [SerializeField] private Image kitchenCorpsesTickImage = null;
    [SerializeField] private Image kitchenCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenCorpsesProgressText = null;

    [Header("Kitchen UV Cleanables")]
    [SerializeField] private Image kitchenUVCleanablesTickImage = null;
    [SerializeField] private Image kitchenUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenUVCleanablesProgressText = null;

    [Header("Kitchen Bloody Objects")]
    [SerializeField] private Image kitchenBloodyObjectsTickImage = null;
    [SerializeField] private Image kitchenBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenBloodyObjectsProgressText = null;

    [Header("Kitchen Arrangables")]
    [SerializeField] private Image kitchenArrangablesTickImage = null;
    [SerializeField] private Image kitchenArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenArrangablesProgressText = null;

    [Header("Kitchen Weapons")]
    [SerializeField] private Image kitchenWeaponsTickImage = null;
    [SerializeField] private Image kitchenWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenWeaponsProgressText = null;

    [Header("Kitchen Documents")]
    [SerializeField] private Image kitchenDocumentsTickImage = null;
    [SerializeField] private Image kitchenDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenDocumentsProgressText = null;

    [Header("Kitchen Clothes")]
    [SerializeField] private Image kitchenClothesTickImage = null;
    [SerializeField] private Image kitchenClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenClothesProgressText = null;

    [Header("Kitchen Miscellaneous")]
    [SerializeField] private Image kitchenMiscellaneousTickImage = null;
    [SerializeField] private Image kitchenMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenMiscellaneousProgressText = null;

    #endregion
    #region STORAGE ROOM VARIABLES
    [Header("Storage Room")]
    [SerializeField] private const string storageRoomName = "Storage Room";
    [SerializeField] private Button storageButton = null;
    [SerializeField] private GameObject storagePanel = null;
    [Header("Storage Room Blood")]
    [SerializeField] private Image storageBloodTickImage = null;
    [SerializeField] private Image storageBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageBloodProgressText = null;

    [Header("Storage Room Corpses")]
    [SerializeField] private Image storageCorpsesTickImage = null;
    [SerializeField] private Image storageCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageCorpsesProgressText = null;

    [Header("Storage Room UV Cleanables")]
    [SerializeField] private Image storageUVCleanablesTickImage = null;
    [SerializeField] private Image storageUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageUVCleanablesProgressText = null;

    [Header("Storage Room Bloody Objects")]
    [SerializeField] private Image storageBloodyObjectsTickImage = null;
    [SerializeField] private Image storageBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageBloodyObjectsProgressText = null;

    [Header("Storage Room Arrangables")]
    [SerializeField] private Image storageArrangablesTickImage = null;
    [SerializeField] private Image storageArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageArrangablesProgressText = null;

    [Header("Storage Room Weapons")]
    [SerializeField] private Image storageWeaponsTickImage = null;
    [SerializeField] private Image storageWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageWeaponsProgressText = null;

    [Header("Storage Room Documents")]
    [SerializeField] private Image storageDocumentsTickImage = null;
    [SerializeField] private Image storageDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageDocumentsProgressText = null;

    [Header("Storage Room Clothes")]
    [SerializeField] private Image storageClothesTickImage = null;
    [SerializeField] private Image storageClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageClothesProgressText = null;

    [Header("Storage Room Miscellaneous")]
    [SerializeField] private Image storageMiscellaneousTickImage = null;
    [SerializeField] private Image storageMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageMiscellaneousProgressText = null;

    #endregion
    #region VIP ROOM VARIABLES
    [Header("VIP Room")]
    [SerializeField] private const string vipRoomName = "VIP Room";
    [SerializeField] private Button vipRoomButton = null;
    [SerializeField] private GameObject vipRoomPanel = null;
    [Header("VIP Room Blood")]
    [SerializeField] private Image vipRoomBloodTickImage = null;
    [SerializeField] private Image vipRoomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomBloodProgressText = null;

    [Header("VIP Room Corpses")]
    [SerializeField] private Image vipRoomCorpsesTickImage = null;
    [SerializeField] private Image vipRoomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomCorpsesProgressText = null;

    [Header("VIP Room UV Cleanables")]
    [SerializeField] private Image vipRoomUVCleanablesTickImage = null;
    [SerializeField] private Image vipRoomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomUVCleanablesProgressText = null;

    [Header("VIP Room Bloody Objects")]
    [SerializeField] private Image vipRoomBloodyObjectsTickImage = null;
    [SerializeField] private Image vipRoomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomBloodyObjectsProgressText = null;

    [Header("VIP Room Arrangables")]
    [SerializeField] private Image vipRoomArrangablesTickImage = null;
    [SerializeField] private Image vipRoomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomArrangablesProgressText = null;

    [Header("VIP Room Weapons")]
    [SerializeField] private Image vipRoomWeaponsTickImage = null;
    [SerializeField] private Image vipRoomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomWeaponsProgressText = null;

    [Header("VIP Room Documents")]
    [SerializeField] private Image vipRoomDocumentsTickImage = null;
    [SerializeField] private Image vipRoomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomDocumentsProgressText = null;

    [Header("VIP Room Clothes")]
    [SerializeField] private Image vipRoomClothesTickImage = null;
    [SerializeField] private Image vipRoomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomClothesProgressText = null;

    [Header("VIP Room Miscellaneous")]
    [SerializeField] private Image vipRoomMiscellaneousTickImage = null;
    [SerializeField] private Image vipRoomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomMiscellaneousProgressText = null;

    #endregion
    #region BETTING ROOM VARIABLES
    [Header("Betting Room")]
    [SerializeField] private const string bettingRoomName = "Betting Room";
    [SerializeField] private Button bettingRoomButton = null;
    [SerializeField] private GameObject bettingRoomPanel = null;
    [Header("Betting Room Blood")]
    [SerializeField] private Image bettingRoomBloodTickImage = null;
    [SerializeField] private Image bettingRoomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomBloodProgressText = null;

    [Header("Betting Room Corpses")]
    [SerializeField] private Image bettingRoomCorpsesTickImage = null;
    [SerializeField] private Image bettingRoomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomCorpsesProgressText = null;

    [Header("Betting Room UV Cleanables")]
    [SerializeField] private Image bettingRoomUVCleanablesTickImage = null;
    [SerializeField] private Image bettingRoomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomUVCleanablesProgressText = null;

    [Header("Betting Room Bloody Objects")]
    [SerializeField] private Image bettingRoomBloodyObjectsTickImage = null;
    [SerializeField] private Image bettingRoomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomBloodyObjectsProgressText = null;

    [Header("Betting Room Arrangables")]
    [SerializeField] private Image bettingRoomArrangablesTickImage = null;
    [SerializeField] private Image bettingRoomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomArrangablesProgressText = null;

    [Header("Betting Room Weapons")]
    [SerializeField] private Image bettingRoomWeaponsTickImage = null;
    [SerializeField] private Image bettingRoomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomWeaponsProgressText = null;

    [Header("Betting Room Documents")]
    [SerializeField] private Image bettingRoomDocumentsTickImage = null;
    [SerializeField] private Image bettingRoomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomDocumentsProgressText = null;

    [Header("Betting Room Clothes")]
    [SerializeField] private Image bettingRoomClothesTickImage = null;
    [SerializeField] private Image bettingRoomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomClothesProgressText = null;

    [Header("Betting Room Miscellaneous")]
    [SerializeField] private Image bettingRoomMiscellaneousTickImage = null;
    [SerializeField] private Image bettingRoomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomMiscellaneousProgressText = null;

    #endregion

    private List<GameObject> tabs = null;
    private float timeElapsed = 0f;
    private bool isFilling = false;

    private void Awake()
    {
        tabs = new List<GameObject>()
        {
            barPanel,
            menBathroomPanel,
            womenBathroomPanel,
            kitchenPanel,
            storagePanel,
            vipRoomPanel,
            bettingRoomPanel
        };

        #region BUTTONS
        barButton.onClick.AddListener(() => { OpenTab(barPanel); });
        menBathroomButton.onClick.AddListener(() => { OpenTab(menBathroomPanel); });
        womenBathroomButton.onClick.AddListener(() => { OpenTab(womenBathroomPanel); });
        kitchenButton.onClick.AddListener(() => { OpenTab(kitchenPanel); });
        storageButton.onClick.AddListener(() => { OpenTab(storagePanel); });
        vipRoomButton.onClick.AddListener(() => { OpenTab(vipRoomPanel); });
        bettingRoomButton.onClick.AddListener(() => { OpenTab(bettingRoomPanel); });
        #endregion
        #region BAR TICK IMAGE
        SetImageState(barBloodTickImage);
        SetImageState(barArrangablesTickImage);
        SetImageState(barBloodyObjectsTickImage);
        SetImageState(barClothesTickImage);
        SetImageState(barCorpsesTickImage);
        SetImageState(barDocumentsTickImage);
        SetImageState(barMiscellaneousTickImage);
        SetImageState(barUVCleanablesTickImage);
        SetImageState(barWeaponsTickImage);
        #endregion
        #region MEN'S BATHROOM TICK IMAGE
        SetImageState(menBathroomMiscellaneousTickImage);
        SetImageState(menBathroomArrangablesTickImage);
        SetImageState(menBathroomBloodTickImage);
        SetImageState(menBathroomBloodyObjectsTickImage);
        SetImageState(menBathroomClothesTickImage);
        SetImageState(menBathroomCorpsesTickImage);
        SetImageState(menBathroomDocumentsTickImage);
        SetImageState(menBathroomUVCleanablesTickImage);
        SetImageState(menBathroomWeaponsTickImage);
        #endregion
        #region WOMEN'S BATHROOM TICK IMAGE
        SetImageState(womenBathroomArrangablesTickImage);
        SetImageState(womenBathroomBloodTickImage);
        SetImageState(womenBathroomBloodyObjectsTickImage);
        SetImageState(womenBathroomClothesTickImage);
        SetImageState(womenBathroomDocumentsTickImage);
        SetImageState(womenBathroomMiscellaneousTickImage);
        SetImageState(womenBathroomWeaponsTickImage);
        SetImageState(womenBathroomUVCleanablesTickImage);
        SetImageState(womenBathroomCorpsesTickImage);
        #endregion
        #region KITCHEN TICK IMAGE
        SetImageState(kitchenArrangablesTickImage);
        SetImageState(kitchenBloodTickImage);
        SetImageState(kitchenBloodyObjectsTickImage);
        SetImageState(kitchenClothesTickImage);
        SetImageState(kitchenCorpsesTickImage);
        SetImageState(kitchenDocumentsTickImage);
        SetImageState(kitchenMiscellaneousTickImage);
        SetImageState(kitchenWeaponsTickImage);
        SetImageState(kitchenUVCleanablesTickImage);
        #endregion
        #region STORAGE ROOM TICK IMAGE
        SetImageState(storageArrangablesTickImage);
        SetImageState(storageBloodTickImage);
        SetImageState(storageBloodyObjectsTickImage);
        SetImageState(storageClothesTickImage);
        SetImageState(storageCorpsesTickImage);
        SetImageState(storageDocumentsTickImage);
        SetImageState(storageMiscellaneousTickImage);
        SetImageState(storageUVCleanablesTickImage);
        SetImageState(storageWeaponsTickImage);
        #endregion
        #region VIP ROOM TICK IMAGE
        SetImageState(vipRoomArrangablesTickImage);
        SetImageState(vipRoomBloodTickImage);
        SetImageState(vipRoomBloodyObjectsTickImage);
        SetImageState(vipRoomClothesTickImage);
        SetImageState(vipRoomCorpsesTickImage);
        SetImageState(vipRoomDocumentsTickImage);
        SetImageState(vipRoomMiscellaneousTickImage);
        SetImageState(vipRoomUVCleanablesTickImage);
        SetImageState(vipRoomWeaponsTickImage);
        #endregion
        #region BETTING ROOM TICK IMAGE
        SetImageState(bettingRoomClothesTickImage);
        SetImageState(bettingRoomMiscellaneousTickImage);
        SetImageState(bettingRoomDocumentsTickImage);
        SetImageState(bettingRoomArrangablesTickImage);
        SetImageState(bettingRoomBloodTickImage);
        SetImageState(bettingRoomBloodyObjectsTickImage);
        SetImageState(bettingRoomCorpsesTickImage);
        SetImageState(bettingRoomUVCleanablesTickImage);
        SetImageState(bettingRoomWeaponsTickImage);
        #endregion
        #region BAR CROSSED OUT IMAGE
        SetImageFillValue(barArrangablesCrossedOutImage);
        SetImageFillValue(barBloodCrossedOutImage);
        SetImageFillValue(barBloodyObjectsCrossedOutImage);
        SetImageFillValue(barClothesCrossedOutImage);
        SetImageFillValue(barCorpsesCrossedOutImage);
        SetImageFillValue(barDocumentsCrossedOutImage);
        SetImageFillValue(barMiscellaneousCrossedOutImage);
        SetImageFillValue(barUVCleanablesCrossedOutImage);
        SetImageFillValue(barWeaponsCrossedOutImage);
        #endregion
        #region MEN'S BATHROOM CROSSED OUT IMAGE
        SetImageFillValue(menBathroomArrangablesCrossedOutImage);
        SetImageFillValue(menBathroomBloodCrossedOutImage);
        SetImageFillValue(menBathroomBloodyObjectsCrossedOutImage);
        SetImageFillValue(menBathroomClothesCrossedOutImage);
        SetImageFillValue(menBathroomCorpsesCrossedOutImage);
        SetImageFillValue(menBathroomDocumentsCrossedOutImage);
        SetImageFillValue(menBathroomMiscellaneousCrossedOutImage);
        SetImageFillValue(menBathroomUVCleanablesCrossedOutImage);
        SetImageFillValue(menBathroomWeaponsCrossedOutImage);
        #endregion
        #region WOMEN'S BATHROOM CROSSED OUT IMAGE
        SetImageFillValue(womenBathroomArrangablesCrossedOutImage);
        SetImageFillValue(womenBathroomBloodCrossedOutImage);
        SetImageFillValue(womenBathroomBloodyObjectsCrossedOutImage);
        SetImageFillValue(womenBathroomClothesCrossedOutImage);
        SetImageFillValue(womenBathroomCorpsesCrossedOutImage);
        SetImageFillValue(womenBathroomDocumentsCrossedOutImage);
        SetImageFillValue(womenBathroomMiscellaneousCrossedOutImage);
        SetImageFillValue(womenBathroomUVCleanablesCrossedOutImage);
        SetImageFillValue(womenBathroomWeaponsCrossedOutImage);
        #endregion
        #region KITCHEN CROSSED OUT IMAGE
        SetImageFillValue(kitchenArrangablesCrossedOutImage);
        SetImageFillValue(kitchenBloodCrossedOutImage);
        SetImageFillValue(kitchenBloodyObjectsCrossedOutImage);
        SetImageFillValue(kitchenClothesCrossedOutImage);
        SetImageFillValue(kitchenCorpsesCrossedOutImage);
        SetImageFillValue(kitchenDocumentsCrossedOutImage);
        SetImageFillValue(kitchenMiscellaneousCrossedOutImage);
        SetImageFillValue(kitchenUVCleanablesCrossedOutImage);
        SetImageFillValue(kitchenWeaponsCrossedOutImage);
        #endregion
        #region STORAGE ROOM CROSSED OUT IMAGE
        SetImageFillValue(storageArrangablesCrossedOutImage);
        SetImageFillValue(storageBloodCrossedOutImage);
        SetImageFillValue(storageBloodyObjectsCrossedOutImage);
        SetImageFillValue(storageClothesCrossedOutImage);
        SetImageFillValue(storageCorpsesCrossedOutImage);
        SetImageFillValue(storageDocumentsCrossedOutImage);
        SetImageFillValue(storageMiscellaneousCrossedOutImage);
        SetImageFillValue(storageUVCleanablesCrossedOutImage);
        SetImageFillValue(storageWeaponsCrossedOutImage);
        #endregion
        #region VIP ROOM CROSSED OUT IMAGE
        SetImageFillValue(vipRoomArrangablesCrossedOutImage);
        SetImageFillValue(vipRoomBloodCrossedOutImage);
        SetImageFillValue(vipRoomBloodyObjectsCrossedOutImage);
        SetImageFillValue(vipRoomClothesCrossedOutImage);
        SetImageFillValue(vipRoomCorpsesCrossedOutImage);
        SetImageFillValue(vipRoomDocumentsCrossedOutImage);
        SetImageFillValue(vipRoomMiscellaneousCrossedOutImage);
        SetImageFillValue(vipRoomUVCleanablesCrossedOutImage);
        SetImageFillValue(vipRoomWeaponsCrossedOutImage);
        #endregion
        #region BETTING ROOM CROSSED OUT IMAGE
        SetImageFillValue(bettingRoomArrangablesCrossedOutImage);
        SetImageFillValue(bettingRoomBloodCrossedOutImage);
        SetImageFillValue(bettingRoomBloodyObjectsCrossedOutImage);
        SetImageFillValue(bettingRoomClothesCrossedOutImage);
        SetImageFillValue(bettingRoomCorpsesCrossedOutImage);
        SetImageFillValue(bettingRoomDocumentsCrossedOutImage);
        SetImageFillValue(bettingRoomMiscellaneousCrossedOutImage);
        SetImageFillValue(bettingRoomUVCleanablesCrossedOutImage);
        SetImageFillValue(bettingRoomWeaponsCrossedOutImage);
        #endregion
        #region TEXTS

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barClothesProgressText, zone.clothesCurrentAmmount, zone.clothes.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomClothesProgressText, zone.clothesCurrentAmmount, zone.clothes.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomClothesProgressText, zone.clothesCurrentAmmount, zone.clothes.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenClothesProgressText, zone.clothesCurrentAmmount, zone.clothes.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageClothesProgressText, zone.clothesCurrentAmmount, zone.clothes.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomClothesProgressText, zone.clothesCurrentAmmount, zone.clothes.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomClothesProgressText, zone.clothesCurrentAmmount, zone.clothes.Count);
        }

        foreach (var zone in gameStateManager.Zones)
        {
            if (zone.zoneName == barName) UpdateText(barMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(menBathroomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
            else if (zone.zoneName == womenBathroomName) UpdateText(womenBathroomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(kitchenMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(storageMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(vipRoomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
            else if (zone.zoneName == menBathroomName) UpdateText(bettingRoomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
        }
        #endregion 
    }

    public void UpdateBloodText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
                if (zone.bloodCurrentAmmount == zone.blood.Count)
                {
                    SetImageState(barBloodTickImage, true);
                    StartFillingImage(barBloodCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
                if (zone.bloodCurrentAmmount == zone.blood.Count)
                {
                    SetImageState(menBathroomBloodTickImage, true);
                    StartFillingImage(menBathroomBloodCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
                if (zone.bloodCurrentAmmount == zone.blood.Count)
                {
                    SetImageState(womenBathroomBloodTickImage, true);
                    StartFillingImage(womenBathroomBloodCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
                if (zone.bloodCurrentAmmount == zone.blood.Count)
                {
                    SetImageState(kitchenBloodTickImage, true);
                    StartFillingImage(kitchenBloodCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
                if (zone.bloodCurrentAmmount == zone.blood.Count)
                {
                    SetImageState(storageBloodTickImage, true);
                    StartFillingImage(storageBloodCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
                if (zone.bloodCurrentAmmount == zone.blood.Count)
                {
                    SetImageState(vipRoomBloodTickImage, true);
                    StartFillingImage(vipRoomBloodCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomBloodProgressText, zone.bloodCurrentAmmount, zone.blood.Count);
                if (zone.bloodCurrentAmmount == zone.blood.Count)
                {
                    SetImageState(bettingRoomBloodTickImage, true);
                    StartFillingImage(bettingRoomBloodCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateCorpsesText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
                if (zone.corpsesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(barCorpsesTickImage, true);
                    StartFillingImage(barCorpsesCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
                if (zone.corpsesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(menBathroomCorpsesTickImage, true);
                    StartFillingImage(menBathroomCorpsesCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
                if (zone.corpsesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(womenBathroomCorpsesTickImage, true);
                    StartFillingImage(womenBathroomCorpsesCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
                if (zone.corpsesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(kitchenCorpsesTickImage, true);
                    StartFillingImage(kitchenCorpsesCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
                if (zone.corpsesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(storageCorpsesTickImage, true);
                    StartFillingImage(storageCorpsesCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
                if (zone.corpsesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(vipRoomCorpsesTickImage, true);
                    StartFillingImage(vipRoomCorpsesCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomCorpsesProgressText, zone.corpsesCurrentAmmount, zone.corpses.Count);
                if (zone.corpsesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(bettingRoomCorpsesTickImage, true);
                    StartFillingImage(bettingRoomCorpsesCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateUVCleanablesText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
                if (zone.uvCleanablesCurrentAmmount == zone.uvCleanables.Count)
                {
                    SetImageState(barUVCleanablesTickImage, true);
                    StartFillingImage(barUVCleanablesCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
                if (zone.uvCleanablesCurrentAmmount == zone.uvCleanables.Count)
                {
                    SetImageState(menBathroomUVCleanablesTickImage, true);
                    StartFillingImage(menBathroomUVCleanablesCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
                if (zone.uvCleanablesCurrentAmmount == zone.uvCleanables.Count)
                {
                    SetImageState(womenBathroomUVCleanablesTickImage, true);
                    StartFillingImage(womenBathroomUVCleanablesCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
                if (zone.uvCleanablesCurrentAmmount == zone.uvCleanables.Count)
                {
                    SetImageState(kitchenUVCleanablesTickImage, true);
                    StartFillingImage(kitchenUVCleanablesCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
                if (zone.uvCleanablesCurrentAmmount == zone.uvCleanables.Count)
                {
                    SetImageState(storageUVCleanablesTickImage, true);
                    StartFillingImage(storageUVCleanablesCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
                if (zone.uvCleanablesCurrentAmmount == zone.uvCleanables.Count)
                {
                    SetImageState(vipRoomUVCleanablesTickImage, true);
                    StartFillingImage(vipRoomUVCleanablesCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomUVCleanablesProgressText, zone.uvCleanablesCurrentAmmount, zone.uvCleanables.Count);
                if (zone.uvCleanablesCurrentAmmount == zone.uvCleanables.Count)
                {
                    SetImageState(bettingRoomUVCleanablesTickImage, true);
                    StartFillingImage(bettingRoomUVCleanablesCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateBloodyObjectsText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
                if (zone.bloodyObjectsCurrentAmmount == zone.bloodyObjects.Count)
                {
                    SetImageState(barBloodyObjectsTickImage, true);
                    StartFillingImage(barBloodyObjectsCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
                if (zone.bloodyObjectsCurrentAmmount == zone.bloodyObjects.Count)
                {
                    SetImageState(menBathroomBloodyObjectsTickImage, true);
                    StartFillingImage(menBathroomBloodyObjectsCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
                if (zone.bloodyObjectsCurrentAmmount == zone.bloodyObjects.Count)
                {
                    SetImageState(womenBathroomBloodyObjectsTickImage, true);
                    StartFillingImage(womenBathroomBloodyObjectsCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
                if (zone.bloodyObjectsCurrentAmmount == zone.bloodyObjects.Count)
                {
                    SetImageState(kitchenBloodyObjectsTickImage, true);
                    StartFillingImage(barBloodyObjectsCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
                if (zone.bloodyObjectsCurrentAmmount == zone.bloodyObjects.Count)
                {
                    SetImageState(storageBloodyObjectsTickImage, true);
                    StartFillingImage(storageBloodyObjectsCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
                if (zone.bloodyObjectsCurrentAmmount == zone.bloodyObjects.Count)
                {
                    SetImageState(vipRoomBloodyObjectsTickImage, true);
                    StartFillingImage(vipRoomBloodyObjectsCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomBloodyObjectsProgressText, zone.bloodyObjectsCurrentAmmount, zone.bloodyObjects.Count);
                if (zone.bloodyObjectsCurrentAmmount == zone.bloodyObjects.Count)
                {
                    SetImageState(bettingRoomBloodyObjectsTickImage, true);
                    StartFillingImage(bettingRoomBloodyObjectsCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateArrangablesText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
                if (zone.arrabgablesCurrentAmmount == zone.arrabgables.Count)
                {
                    SetImageState(barArrangablesTickImage, true);
                    StartFillingImage(barArrangablesCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
                if (zone.arrabgablesCurrentAmmount == zone.arrabgables.Count)
                {
                    SetImageState(menBathroomArrangablesTickImage, true);
                    StartFillingImage(menBathroomArrangablesCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
                if (zone.arrabgablesCurrentAmmount == zone.arrabgables.Count)
                {
                    SetImageState(womenBathroomArrangablesTickImage, true);
                    StartFillingImage(womenBathroomArrangablesCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
                if (zone.arrabgablesCurrentAmmount == zone.arrabgables.Count)
                {
                    SetImageState(kitchenArrangablesTickImage, true);
                    StartFillingImage(kitchenArrangablesCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
                if (zone.arrabgablesCurrentAmmount == zone.arrabgables.Count)
                {
                    SetImageState(storageArrangablesTickImage, true);
                    StartFillingImage(storageArrangablesCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
                if (zone.arrabgablesCurrentAmmount == zone.arrabgables.Count)
                {
                    SetImageState(vipRoomArrangablesTickImage, true);
                    StartFillingImage(vipRoomArrangablesCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomArrangablesProgressText, zone.arrabgablesCurrentAmmount, zone.arrabgables.Count);
                if (zone.arrabgablesCurrentAmmount == zone.arrabgables.Count)
                {
                    SetImageState(bettingRoomArrangablesTickImage, true);
                    StartFillingImage(bettingRoomArrangablesCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateWeaponsText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
                if (zone.weaponsCurrentAmmount == zone.weapons.Count)
                {
                    SetImageState(barWeaponsTickImage, true);
                    StartFillingImage(barWeaponsCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
                if (zone.weaponsCurrentAmmount == zone.weapons.Count)
                {
                    SetImageState(menBathroomWeaponsTickImage, true);
                    StartFillingImage(menBathroomWeaponsCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
                if (zone.weaponsCurrentAmmount == zone.weapons.Count)
                {
                    SetImageState(womenBathroomWeaponsTickImage, true);
                    StartFillingImage(womenBathroomWeaponsCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
                if (zone.weaponsCurrentAmmount == zone.weapons.Count)
                {
                    SetImageState(kitchenWeaponsTickImage, true);
                    StartFillingImage(kitchenWeaponsCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
                if (zone.weaponsCurrentAmmount == zone.weapons.Count)
                {
                    SetImageState(storageWeaponsTickImage, true);
                    StartFillingImage(storageWeaponsCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
                if (zone.weaponsCurrentAmmount == zone.weapons.Count)
                {
                    SetImageState(vipRoomWeaponsTickImage, true);
                    StartFillingImage(vipRoomWeaponsCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomWeaponsProgressText, zone.weaponsCurrentAmmount, zone.weapons.Count);
                if (zone.weaponsCurrentAmmount == zone.weapons.Count)
                {
                    SetImageState(bettingRoomWeaponsTickImage, true);
                    StartFillingImage(bettingRoomWeaponsCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateDocumentsText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
                if (zone.documentsCurrentAmmount == zone.documents.Count)
                {
                    SetImageState(barDocumentsTickImage, true);
                    StartFillingImage(barDocumentsCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
                if (zone.documentsCurrentAmmount == zone.documents.Count)
                {
                    SetImageState(menBathroomDocumentsTickImage, true);
                    StartFillingImage(menBathroomDocumentsCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
                if (zone.documentsCurrentAmmount == zone.documents.Count)
                {
                    SetImageState(womenBathroomDocumentsTickImage, true);
                    StartFillingImage(womenBathroomDocumentsCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
                if (zone.documentsCurrentAmmount == zone.documents.Count)
                {
                    SetImageState(kitchenDocumentsTickImage, true);
                    StartFillingImage(kitchenDocumentsCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
                if (zone.documentsCurrentAmmount == zone.documents.Count)
                {
                    SetImageState(storageDocumentsTickImage, true);
                    StartFillingImage(storageDocumentsCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
                if (zone.documentsCurrentAmmount == zone.documents.Count)
                {
                    SetImageState(vipRoomDocumentsTickImage, true);
                    StartFillingImage(vipRoomDocumentsCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomDocumentsProgressText, zone.documentsCurrentAmmount, zone.documents.Count);
                if (zone.documentsCurrentAmmount == zone.documents.Count)
                {
                    SetImageState(bettingRoomDocumentsTickImage, true);
                    StartFillingImage(bettingRoomDocumentsCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateClothesText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barClothesProgressText, zone.clothesCurrentAmmount, zone.corpses.Count);
                if (zone.clothesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(barClothesTickImage, true);
                    StartFillingImage(barClothesCrossedOutImage);
                }
                break;
            case menBathroomName:
                UpdateText(menBathroomClothesProgressText, zone.clothesCurrentAmmount, zone.corpses.Count);
                if (zone.clothesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(menBathroomClothesTickImage, true);
                    StartFillingImage(menBathroomClothesCrossedOutImage);
                }
                break;
            case womenBathroomName:
                UpdateText(womenBathroomClothesProgressText, zone.clothesCurrentAmmount, zone.corpses.Count);
                if (zone.clothesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(womenBathroomClothesTickImage, true);
                    StartFillingImage(womenBathroomClothesCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenClothesProgressText, zone.clothesCurrentAmmount, zone.corpses.Count);
                if (zone.clothesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(kitchenClothesTickImage, true);
                    StartFillingImage(kitchenClothesCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageClothesProgressText, zone.clothesCurrentAmmount, zone.corpses.Count);
                if (zone.clothesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(storageClothesTickImage, true);
                    StartFillingImage(storageClothesCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomClothesProgressText, zone.clothesCurrentAmmount, zone.corpses.Count);
                if (zone.clothesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(vipRoomClothesTickImage, true);
                    StartFillingImage(vipRoomClothesCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomClothesProgressText, zone.clothesCurrentAmmount, zone.corpses.Count);
                if (zone.clothesCurrentAmmount == zone.corpses.Count)
                {
                    SetImageState(bettingRoomClothesTickImage, true);
                    StartFillingImage(bettingRoomClothesCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    public void UpdateMiscellaneousText(Zone zone)
    {
        switch (zone.zoneName)
        {
            case barName:
                UpdateText(barMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
                if (zone.miscellaneousCurrentAmmount == zone.miscellaneous.Count)
                {
                    SetImageState(barMiscellaneousTickImage, true);
                    StartFillingImage(barMiscellaneousCrossedOutImage);
                }
                break;
            case menBathroomName:
                if (zone.miscellaneousCurrentAmmount == zone.miscellaneous.Count)
                {
                    SetImageState(menBathroomMiscellaneousTickImage, true);
                    StartFillingImage(menBathroomMiscellaneousCrossedOutImage);
                }
                UpdateText(menBathroomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
                break;
            case womenBathroomName:
                UpdateText(womenBathroomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
                if (zone.miscellaneousCurrentAmmount == zone.miscellaneous.Count)
                {
                    SetImageState(womenBathroomMiscellaneousTickImage, true);
                    StartFillingImage(womenBathroomMiscellaneousCrossedOutImage);
                }
                break;
            case kitchenName:
                UpdateText(kitchenMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
                if (zone.miscellaneousCurrentAmmount == zone.miscellaneous.Count)
                {
                    SetImageState(kitchenMiscellaneousTickImage, true);
                    StartFillingImage(kitchenMiscellaneousCrossedOutImage);
                }
                break;
            case storageRoomName:
                UpdateText(storageMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
                if (zone.miscellaneousCurrentAmmount == zone.miscellaneous.Count)
                {
                    SetImageState(storageMiscellaneousTickImage, true);
                    StartFillingImage(storageMiscellaneousCrossedOutImage);
                }
                break;
            case vipRoomName:
                UpdateText(vipRoomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
                if (zone.miscellaneousCurrentAmmount == zone.miscellaneous.Count)
                {
                    SetImageState(vipRoomMiscellaneousTickImage, true);
                    StartFillingImage(vipRoomMiscellaneousCrossedOutImage);
                }
                break;
            case bettingRoomName:
                UpdateText(bettingRoomMiscellaneousProgressText, zone.miscellaneousCurrentAmmount, zone.miscellaneous.Count);
                if (zone.miscellaneousCurrentAmmount == zone.miscellaneous.Count)
                {
                    SetImageState(bettingRoomMiscellaneousTickImage, true);
                    StartFillingImage(bettingRoomMiscellaneousCrossedOutImage);
                }
                break;
            default:
                break;
        }
    }

    private void SetImageState(Image image, bool state = false)
    {
        image.enabled = state;
    }

    private void SetImageFillValue(Image image, float amount = 0)
    {
        image.fillAmount = amount;
    }

    public void StartFillingImage(Image image, float targetFillAmount = 1f, float duration = 1f)
    {
        isFilling = true;
        timeElapsed = 0f;

        while (isFilling)
        {
            FillImageOverTime(image, targetFillAmount, duration);
        }
    }

    private bool FillImageOverTime(Image image, float targetFillAmount = 1, float duration = 1)
    {
        if (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(image.fillAmount, targetFillAmount, timeElapsed / duration);
            Debug.Log("Fill amount: " + image.fillAmount);
            return false; 
        }
        else
        {
            image.fillAmount = targetFillAmount;
            isFilling = false; 
            return true;
        }
    }

    private void OpenTab(GameObject selectedTab)
    {
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(tab == selectedTab);
        }

        scrollRect.content = selectedTab.GetComponent<RectTransform>();
    }

    private void UpdateText(TextMeshProUGUI text, int currentAmount, int maxAmount)
    {
        text.text = $"{currentAmount:D2}/{maxAmount:D2}";
    }
}