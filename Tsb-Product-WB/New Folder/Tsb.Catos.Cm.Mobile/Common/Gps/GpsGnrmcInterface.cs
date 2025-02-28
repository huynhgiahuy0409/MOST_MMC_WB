using System;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item.Gps;

namespace Tsb.Catos.Cm.Mobile.Common.Gps
{
    public class GpsGnrmcInterface
    {
        #region CONST & FIELD AREA ********************************************
        private static GpsGnrmcInterface instance_ = null;
        private static GpsGnrmcManager gpsManager = null;
        private const double PI = 3.14159265358979323846;
        private BlockItemList _blockList = null;
        #endregion CONST & FIELD AREA ********************************************

        #region PROPERTY AREA ********************************************
        public BlockItemList BlockList
        {
            get { return _blockList; }
            set { _blockList = value; }
        }
        #endregion PROPERTY AREA ********************************************

        #region INITIALIZATION AREA ********************************************
        private GpsGnrmcInterface() { }

        public static GpsGnrmcInterface GetInstance()
        {
            if (instance_ == null)
            {
                instance_ = CreateInstance();
            }
            return instance_;
        }

        private static GpsGnrmcInterface CreateInstance()
        {
            if (instance_ == null)
            {
                instance_ = new GpsGnrmcInterface();
                instance_.Init();
            }
            return instance_;
        }

        private void Init()
        {

            Coordinates startTransCoord;
            Double leftTopX = 0.0;
            Double leftTopY = 0.0;
            Double rightTopX = 0.0;
            Double rightTopY = 0.0;
            double angle = 0.0;

            // 1. 기울기 및 원점위치 계산을 위한 입력값 (선석을 Top으로 돌렸을때 기준으로 구글맵에서의 정확한 위경도 값)
            String longitude4LeftTop = "127.663083"; 	// 경도(EW)
            String latitude4LeftTop = "34.898333"; 		// 위도(NS)
            String longitude4RightTop = "127.650751";	// 경도(EW)
            String latitude4RightTop = "34.890950";		// 위도(NS)

            gpsManager = new GpsGnrmcManager();

            // 2. 각도 돌리지 않은 상태에서의 위경도를 좌표값으로 변환
            gpsManager.GpsTerminalRotateDatumPointX = 0;
            gpsManager.GpsTerminalRotateDatumPointY = 0;
            gpsManager.GpsRotateAngle = 0;
            gpsManager.GpsDistanceGapX = 0;
            gpsManager.GpsDistanceGapY = 0;
            gpsManager.GpsDataFormat = CodeConstants.GPS_DATA_FORMAT_DDD;

            startTransCoord = gpsManager.ConvertCoordinates(longitude4LeftTop, latitude4LeftTop);
            leftTopX = startTransCoord.positionX;
            leftTopY = startTransCoord.positionY;
            ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.init() : leftTop - X : " + leftTopX + ", Y : " + leftTopY);

            startTransCoord = gpsManager.ConvertCoordinates(longitude4RightTop, latitude4RightTop);
            rightTopX = startTransCoord.positionX;
            rightTopY = startTransCoord.positionY;
            ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.init() : rightTop - X : " + rightTopX + ", Y : " + rightTopY);

            // 3. 화면상에서 선석을 위쪽으로 옮기기 위한 기울기 계산
            double dx = rightTopX - leftTopX;
            double dy = rightTopY - leftTopY;
            angle = Math.Abs(ConvertRadiansToDegrees(Math.Atan2(dy, dx)));
            ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.init() : angle = " + angle);

            // 아래 항목들은 설정값이므로 최초 계산 및 튜닝 후 프로그램 시작시점에 변수에 설정해놓고 고정으로 사용하면 됨.
            gpsManager.GpsTerminalRotateDatumPointX = leftTopX;
            gpsManager.GpsTerminalRotateDatumPointY = leftTopY;
            gpsManager.GpsRotateAngle = angle;
            gpsManager.GpsDistanceGapX = 0;	// offset 조정
            gpsManager.GpsDistanceGapY = 0;	// offset 조정
            gpsManager.GpsScaleErrorRangeX = 0;
            gpsManager.GpsScaleErrorRangeY = 0;
            gpsManager.GpsDataFormat = CodeConstants.GPS_DATA_FORMAT_DMM;
        }
        #endregion INITIALIZATION AREA ********************************************

        #region METHOD AREA ********************************************
        public LocationItem GetLocation(String longitude, String latitude)
        {
            LocationItem locationItem = null;
            Coordinates coordinates = null;
            CoordinatesManager coordinatesManager = null;

            try
            {
                coordinates = gpsManager.ConvertCoordinates(longitude, latitude);
                if (coordinates != null)
                {
                    double catosX = coordinates.positionX;
                    double catosY = coordinates.positionY;
                    ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.getLocation() : CATOS X = " + catosX + ", CATOS Y = " + catosY);

                    coordinatesManager = new CoordinatesManager();
                    coordinatesManager.BlockList = BlockList;
                    locationItem = coordinatesManager.GetLocationByPoint((float)catosX, (float)catosY);
                    if (locationItem != null)
                    {
                        ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.getLocation() : Block = " + locationItem.BlockName + ", BayRowIndex = " + locationItem.BayRowIndex);
                    }
                    else
                    {
                        double newCatosX = catosX + 3;
                        double newCatosY = catosY + 3;
                        ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.getLocation() : Location Item is null. +3m. CATOS X = " + newCatosX + ", CATOS Y = " + newCatosY);

                        locationItem = coordinatesManager.GetLocationByPoint((float)newCatosX, (float)newCatosY);
                        if (locationItem != null)
                        {
                            ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.getLocation() : Block = " + locationItem.BlockName + ", BayRowIndex = " + locationItem.BayRowIndex);
                        }
                        else
                        {
                            newCatosX = catosX - 3;
                            newCatosY = catosY - 3;
                            ErrorMessageHandler.WriteLog4Debug("GpsGnrmcInterface.getLocation() : Location Item is null. -3m. CATOS X = " + newCatosX + ", CATOS Y = " + newCatosY);

                            locationItem = coordinatesManager.GetLocationByPoint((float)newCatosX, (float)newCatosY);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessageHandler.ErrorLog(e);
            }
            finally
            {
                coordinates = null;
                coordinatesManager = null;
            }

            return locationItem;
        }

        public double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / PI) * radians;
            return (degrees);
        }
        #endregion METHOD AREA ********************************************
    }
}
