using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.readonlyants;
using System.Drawing;
using Tsb.Catos.Cm.Mobile.Common.Item.Gps;
using Tsb.Catos.Cm.Core.YardDefine.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Gps
{
    public class CoordinatesManager
    {
        #region CONST & FIELD AREA ********************************************
        public static readonly float SLOT_WIDTH = 6.45f;
        public static readonly float SLOT_HEIGHT = 2.83f;
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

        #region METHOD AREA ********************************************
        public LocationItem GetLocationByPoint(float x, float y)
        {
            LocationItem location = null;
            YSlotItem slot = null;
            try
            {
                if (BlockList != null)
                {
                    for (int i = 1; i <= BlockList.Count(); i++)
                    {
                        BlockItem block = BlockList.GetBlock(i);
                        if (block != null)
                        {

                            PointF[] points = GetVertex(block.X, block.Y, block.L, block.W);
                            float degree = block.Deg;
                            PointF rotationCenter = points[0];
                            PointF[] rotatedPoints = TransformPoints(points, degree, rotationCenter);

                            if (IsContainPoint(rotatedPoints, x, y))
                            {
                                PointF[] slotPoints = new PointF[4];
                                if (block.Facility.Equals(YDefineConstants.FAC_SC))
                                {
                                    for (short j = 1; j <= block.MaxRow; j++)
                                    {
                                        if (block.BayDir.Equals(YDefineConstants.ROWDIR_LEFT2RIGHT))
                                        {
                                            slot = block.GetYSlot(1, j);
                                        }
                                        else
                                        {
                                            slot = block.GetYSlot(block.MaxBay, j);
                                        }

                                        slotPoints = GetVertex(slot.X, slot.Y, block.L, SLOT_HEIGHT);
                                        PointF slotRotationCenter = slotPoints[0];
                                        PointF[] rotatedSlotPoints = TransformPoints(slotPoints, degree, slotRotationCenter);

                                        if (IsContainPoint(rotatedSlotPoints, x, y))
                                        {
                                            location = new LocationItem();
                                            location.BlockName = block.Name;
                                            location.BayRowIndex = j;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (short j = 1; j <= block.MaxBay; j++)
                                    {
                                        if (block.RowDir.Equals(YDefineConstants.ROWDIR_LEFT2RIGHT))
                                        {
                                            slot = block.GetYSlot(j, 1);
                                        }
                                        else
                                        {
                                            slot = block.GetYSlot(j, block.MaxRow);
                                        }

                                        slotPoints = GetVertex(slot.X, slot.Y, SLOT_WIDTH, block.W);
                                        PointF slotRotationCenter = slotPoints[0];
                                        PointF[] rotatedSlotPoints = TransformPoints(slotPoints, degree, slotRotationCenter);

                                        if (IsContainPoint(rotatedSlotPoints, x, y))
                                        {
                                            location = new LocationItem();
                                            location.BlockName = block.Name;
                                            location.BayRowIndex = j;
                                            break;
                                        }
                                    }
                                }

                                if (location != null)
                                {
                                    break;
                                }
                            }
                        }
                        block = null;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessageHandler.ErrorLog(e);
            }
            return location;
        }

        public bool IsContainPoint(PointF[] polygonPoint, float x, float y)
        {
            int hitCount = 0;
            float maxX = 0;
            float maxY = 0;
            float minX = int.MaxValue;
            float minY = int.MaxValue;
            try
            {
                for (int idx = 0; idx < polygonPoint.Length; idx++)
                {
                    PointF point = polygonPoint[idx];
                    if (point != null)
                    {
                        maxX = Math.Max(maxX, point.X);
                        maxY = Math.Max(maxY, point.Y);

                        minX = Math.Min(minX, point.X);
                        minY = Math.Min(minY, point.Y);
                    }
                }

                if (maxX < x || x < minX)
                {
                    return false;
                }

                if (maxY < y || y < minY)
                {
                    return false;
                }

                PointF[] tempPoint = new PointF[polygonPoint.Length];
                for (int idx = 0; idx < polygonPoint.Length; idx++)
                {
                    if (idx != polygonPoint.Length - 1)
                    {
                        tempPoint[idx] = polygonPoint[idx + 1];
                    }
                    else
                    {
                        tempPoint[idx] = polygonPoint[0];
                    }
                }

                for (int idx = 0; idx < polygonPoint.Length; idx++)
                {
                    if (Math.Min(polygonPoint[idx].Y, tempPoint[idx].Y) > y)
                    {
                        continue;
                    }
                    if (Math.Max(polygonPoint[idx].Y, tempPoint[idx].Y) < y)
                    {
                        continue;
                    }
                    if (CheckIntersection(polygonPoint[idx], tempPoint[idx], new PointF(x, y), new PointF(x + maxX, y)))
                    {
                        hitCount++;
                    }
                }

                if (hitCount % 2 == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                ErrorMessageHandler.ErrorLog(e);
            }

            return false;
        }

        private bool CheckIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            if ((GetSignedArea(p1, p2, p3) * GetSignedArea(p1, p2, p4) <= 0) &&
                (GetSignedArea(p3, p4, p1) * GetSignedArea(p3, p4, p2) <= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private PointF[] TransformPoints(PointF[] pointArray, float degree, PointF rotationCenter)
        {
            PointF[] points = new PointF[pointArray.Length];
            for (int i = 0; i < pointArray.Length; i++)
            {
                points[i] = RotatePoint(pointArray[i], degree, rotationCenter);
            }

            return points;
        }

        private PointF[] GetVertex(float x, float y, float width, float height)
        {
            PointF[] points = new PointF[4];
            points[0] = new PointF(x, y);
            points[1] = new PointF((x + width), y);
            points[2] = new PointF((x + width), (y + height));
            points[3] = new PointF(x, (y + height));
            return points;
        }

        private PointF RotatePoint(PointF point, float degree, PointF rotationCenter)
        {
            double x, y;
            PointF rotatedPoint = new PointF(float.NaN, float.NaN);
            try
            {
                double rad = DegreeToRadian(degree);
                x = ((point.X - rotationCenter.X) * Math.Cos(rad) - (point.Y - rotationCenter.Y) * Math.Sin(rad)) + rotationCenter.X;
                y = ((point.X - rotationCenter.X) * Math.Sin(rad) + (point.Y - rotationCenter.Y) * Math.Cos(rad)) + rotationCenter.Y;
                rotatedPoint = new PointF((float)x, (float)y);
            }
            catch (Exception e)
            {
                ErrorMessageHandler.ErrorLog(e);
            }
            return rotatedPoint;
        }

        private int GetSignedArea(PointF p1, PointF p2, PointF p3)
        {
            float area = ((p1.X * p2.Y - p1.Y * p2.X) +
                         (p2.X * p3.Y - p2.Y * p3.X) +
                         (p3.X * p1.Y - p3.Y * p1.X));

            if (area >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        private double DegreeToRadian(double angle) { return PI * angle / 180.0; }
        #endregion METHOD AREA ********************************************
    }
}
