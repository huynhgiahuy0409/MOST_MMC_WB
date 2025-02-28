using System;
using Tsb.Catos.Cm.Mobile.Common.Constants;

namespace Tsb.Catos.Cm.Mobile.Common.Gps
{
    public class GpsGnrmcManager
    {
        #region CONST & FIELD AREA ********************************************
        public const int NUMBER_UNDEFINED = -1;
        private const double PI = 3.14159265358979323846;

        private static int _gpsUtcTimeSlot;
        private static double _gpsRotateAngle;
        private static double _gpsTerminalRotateDatumPointX;
        private static double _gpsTerminalRotateDatumPointY;
        private static double _gpsScaleErrorRangeX;	// X axis error range per meter
        private static double _gpsScaleErrorRangeY;	// Y axis error range per meter
        private static double _gpsDistanceGapX;		// X axis distance gap on the assumption that actual distance and CHESS's distance are almost similar
        private static double _gpsDistanceGapY;		// Y axis distance gap on the assumption that actual distance and CHESS's distance are almost similar.
        private static int _gpsMaximumAllowableErrorRange;	// If GPS signal is not correct because of some matter, system allows to accept this amount of error range while calculating
        private static int _maximumSpeedLimitInTerminal;	// Max Speed Limit in Terminal
        private static int _gpsDataFormat = 0;
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA ********************************************
        public int GpsUtcTimeSlot 
        {
            get {return _gpsUtcTimeSlot;}  
            set {_gpsUtcTimeSlot = value;} 
        }
        public double GpsRotateAngle 
        {
            get { return _gpsRotateAngle; } 
            set { _gpsRotateAngle = value; } 
        }
        public double GpsTerminalRotateDatumPointX
        {
            get { return _gpsTerminalRotateDatumPointX; }
            set { _gpsTerminalRotateDatumPointX = value; }
        }
        public double GpsTerminalRotateDatumPointY
        {
            get { return _gpsTerminalRotateDatumPointY; }
            set { _gpsTerminalRotateDatumPointY = value; }
        }
        public double GpsScaleErrorRangeX
        {
            get { return _gpsScaleErrorRangeX; }
            set { _gpsScaleErrorRangeX = value; }
        }
        public double GpsScaleErrorRangeY
        {
            get { return _gpsScaleErrorRangeY; }
            set { _gpsScaleErrorRangeY = value; }
        }
        public double GpsDistanceGapX
        {
            get { return _gpsDistanceGapX; }
            set { _gpsDistanceGapX = value; }
        }
        public double GpsDistanceGapY
        {
            get { return _gpsDistanceGapY; }
            set { _gpsDistanceGapY = value; }
        }
        public int GpsMaximumAllowableErrorRange
        {
            get { return _gpsMaximumAllowableErrorRange; }
            set { _gpsMaximumAllowableErrorRange = value; }
        }
        public int MaximumSpeedLimitInTerminal
        {
            get { return _maximumSpeedLimitInTerminal; }
            set { _maximumSpeedLimitInTerminal = value; }
        }
        public int GpsDataFormat
        {
            get { return _gpsDataFormat; }
            set { _gpsDataFormat = value; }
        }
        #endregion PROPERTY AREA ********************************************

        #region INITIALIZATION AREA ********************************************
        #endregion INITIALIZATION AREA ********************************************

        #region METHOD AREA ********************************************
        public Coordinates ConvertCoordinates(String longitude, String latitude)
        {
            // 소수점뒤에 4자리가 넘어가면 십진변환된것임. ex) 35.101072 129.094908
            // 3735.0079,N,12701.6446,E

            Coordinates coordinates = new Coordinates();
            double pLongitude;
            double pLatitude;

            try
            {
                if (longitude == null || latitude == null || longitude.Length < 3 || latitude.Length < 3)
                {
                    coordinates.positionX = NUMBER_UNDEFINED;
                    coordinates.positionY = NUMBER_UNDEFINED;
                    return coordinates;
                }

                if (GpsDataFormat == CodeConstants.GPS_DATA_FORMAT_DDD)
                {
                    // in case already converted to decimal value
                    pLongitude = Double.Parse(longitude);
                    pLatitude = Double.Parse(latitude);
                }
                else
                {
                    // DM(Degrees/Minutes) -> DD(Decimal Degree)
                    pLongitude = ConvertDmToDd(longitude, false);
                    pLatitude = ConvertDmToDd(latitude, true);
                }

                // WGS84(World Geodetic System 1984) -> UTM(Universal Transverse Mercator)
                double[] wgs84ToUtm = GeoConverter.ToUtm(pLongitude, pLatitude);

                Coordinates tmpCoord = new Coordinates();
                // GPS angle is rotated as CHESS system

                tmpCoord = RotateMap(wgs84ToUtm[0], wgs84ToUtm[1]);	// [0]: X, [1]: Y

                double tmpPositionX = tmpCoord.positionX - GpsTerminalRotateDatumPointX;
                double tmpPositionY = tmpCoord.positionY - GpsTerminalRotateDatumPointY;

//				// Control distance gap
//				double fixedGapPositionX = tmpPositionX + getGpsDistanceGapX();
//				double fixedGapPositionY = tmpPositionY - getGpsDistanceGapY();

                // Control scale error range
                double scaleErrorRangeCalculateX = tmpPositionX + (tmpPositionX * GpsScaleErrorRangeX);
                double scaleErrorRangeCalculateY = tmpPositionY + (tmpPositionY * GpsScaleErrorRangeY);

                coordinates.positionX = Math.Round(scaleErrorRangeCalculateX * 10) / 10.0;

                // Y-axis is rotated 180 degrees(Negative <-> Positive)
                if (scaleErrorRangeCalculateY <= 0)
                {
                    coordinates.positionY = Math.Abs(Math.Round(scaleErrorRangeCalculateY * 10) / 10.0);
                }
                else
                {
                    coordinates.positionY = Math.Round(scaleErrorRangeCalculateY * 10) / 10.0;
                }

                // Control distance gap
                coordinates.positionX = coordinates.positionX + GpsDistanceGapX;
                coordinates.positionY = coordinates.positionY + GpsDistanceGapY;

            }
            catch (Exception e)
            {
                coordinates.positionX = NUMBER_UNDEFINED;
                coordinates.positionY = NUMBER_UNDEFINED;
//				chePdsLogger.error("", e);
            }

            return coordinates;

        }

        public double ConvertDmToDd(String originalCoorinate, bool coordinateType)
        {	// true: latitude, false: longitude

            if (coordinateType)
            {	// latitude
                double convertedLatitude = 0;

                Dm latitudeDm = new Dm();
                latitudeDm = ParseLatitude(originalCoorinate);
                convertedLatitude = GeoConverter.DmToDEC(latitudeDm.degrees, latitudeDm.minutes);

                return convertedLatitude;
            }
            else if (!coordinateType)
            {	// longitude
                double convertedLongitude = 0;

                Dm latitudeDm = new Dm();
                latitudeDm = ParseLongitude(originalCoorinate);
                convertedLongitude = GeoConverter.DmToDEC(latitudeDm.degrees, latitudeDm.minutes);

                return convertedLongitude;
            }
            return 0;
        }

        public Dm ParseLatitude(String latitude)
        {

            Dm dm = new Dm();
            int pos4Dot = latitude.IndexOf(".");

            // modified by Lee Sang Wook(2016.06.24) : Latitude format is different between PCT and GOCT
            // GOCT=2238.9061, PCT=03757.421
            if (pos4Dot == 5)
            {
                // PCT
                dm.degrees = Double.Parse(latitude.Substring(1, 3));
                dm.minutes = Double.Parse(latitude.Substring(3, latitude.Length - 3));
            }
            else
            {
                // GOCT
                dm.degrees = Double.Parse(latitude.Substring(0, 2));
                dm.minutes = Double.Parse(latitude.Substring(2, latitude.Length - 2));
            }

            return dm;
        }

        public Dm ParseLongitude(String longitude)
        {

            Dm dm = new Dm();

            dm.degrees = Double.Parse(longitude.Substring(0, 3));
            dm.minutes = Double.Parse(longitude.Substring(3, longitude.Length - 3));
            
            return dm;
        }

        private Coordinates RotateMap(double fromX, double fromY)
        {

            Coordinates coordinates = new Coordinates();

            double rad = DegreeToRadian(GpsRotateAngle);
            double toX = 0;
            double toY = 0;

            if (GpsRotateAngle > 0)
            {
                toX = ((fromX - GpsTerminalRotateDatumPointX) * Math.Cos(rad) - (fromY - GpsTerminalRotateDatumPointY) * Math.Sin(rad)) + GpsTerminalRotateDatumPointX;
                toY = ((fromX - GpsTerminalRotateDatumPointX) * Math.Sin(rad) + (fromY - GpsTerminalRotateDatumPointY) * Math.Cos(rad)) + GpsTerminalRotateDatumPointY;
            }
            else
            {
                toX = ((fromX - GpsTerminalRotateDatumPointX) * Math.Cos(rad) + (fromY - GpsTerminalRotateDatumPointY) * Math.Sin(rad)) + GpsTerminalRotateDatumPointX;
                toY = ((fromX - GpsTerminalRotateDatumPointX) * Math.Sin(rad) - (fromY - GpsTerminalRotateDatumPointY) * Math.Cos(rad)) + GpsTerminalRotateDatumPointY;
            }

            coordinates.positionX = toX;
            coordinates.positionY = toY;

            return coordinates;

        }

        private double DegreeToRadian(double angle) { return PI * angle / 180.0; }

        #endregion METHOD AREA ********************************************
    }
}
