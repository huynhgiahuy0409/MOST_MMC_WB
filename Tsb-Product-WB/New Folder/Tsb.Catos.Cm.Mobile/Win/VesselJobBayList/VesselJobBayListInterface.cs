using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.VesselJobBayList
{
    public interface VesselJobBayListInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************

        string FormName { get; }
        bool UseUserVoyage { get; }
        bool UseEnableVesselChangeAfterLogin { get; }
        bool UseActivatedInAllBay { get; set; }//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        bool UseVesselName { get; } // added by JH.Tak (2020.09.23) 0109492: QC driver- login screen - select vessel display vessel name instead of vsl code
        bool UseBayInsteadHatch { get; } // added by JH.Tak (2021.07.07) KPCT Driverless YT
        bool UsePortVoy { get; } // added by BG.Kim (2022.11.29) [WHL_UP] 0134776: Show VRN(PORT_VOY) in Location
        #endregion PROPERTY AREA **********************************************
    }
}
