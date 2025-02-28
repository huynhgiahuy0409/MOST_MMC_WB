#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2011 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
* 
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2016.11.09    Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Log;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tsb.Fontos.Core.Util
{
    /// <summary>
    /// 좌표들에 대한 기하학적 변환에 대한 유용한 기능을 정의합니다.
    /// </summary>
    public class MatrixUtil : BaseUtil
    {
        new public const string ObjectID = "GNR-FTCO-UTL-MatrixUtil";

        /// <summary>
        /// 지정된 영역의 회전 영역을 계산합니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="rectangle">영역</param>
        /// <returns>회전된 영역을 반환합니다.</returns>
        public static Rectangle RotateRectangle(float degree, PointF rotationCenter, Rectangle rectangle)
        {
            if (degree == 0) return rectangle;

            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(degree, rotationCenter);

            Point[] points = new Point[4];

            try
            {
                points[0] = new Point(rectangle.X, rectangle.Y);
                points[1] = new Point(rectangle.X, rectangle.Y + rectangle.Height);
                points[2] = new Point(rectangle.X + rectangle.Width, rectangle.Y);
                points[3] = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
                myMatrix.TransformPoints(points);

                myMatrix.Dispose();
                myMatrix = null;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return MatrixUtil.GetMBR(points);
        }
        /// <summary>
        /// 지정된 영역의 회전 영역을 계산합니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="rectangle">영역</param>
        /// <returns>회전된 영역을 반환합니다.</returns>
        public static RectangleF RotateRectangleF(float degree, PointF rotationCenter, RectangleF rectangle)
        {
            if (degree == 0) return rectangle;

            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(degree, rotationCenter);

            PointF[] points = new PointF[4];

            try
            {
                points[0] = new PointF(rectangle.X, rectangle.Y);
                points[1] = new PointF(rectangle.X, rectangle.Y + rectangle.Height);
                points[2] = new PointF(rectangle.X + rectangle.Width, rectangle.Y);
                points[3] = new PointF(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
                myMatrix.TransformPoints(points);

                myMatrix.Dispose();
                myMatrix = null;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return MatrixUtil.GetRectangleF(points);
        }
        /// <summary>
        /// 지정된 영역의 회전 영역을 계산합니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="rectangle">영역</param>
        /// <returns>회전된 영역의 꼭지점을 반환합니다.</returns>
        public static List<Point> RotatePoints(float degree, PointF rotationCenter, Rectangle rectangle)
        {
            List<Point> pointList = new List<Point>();
            Point[] points = MatrixUtil.RotatePointArray(degree, rotationCenter, rectangle);
            for (int i = 0; i < 4; i++)
            {
                pointList.Add(points[i]);
            }

            return pointList;
        }
        /// <summary>
        /// 지정된 영역의 회전 영역을 계산합니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="rectangle">영역</param>
        /// <returns>회전된 영역의 꼭지점을 반환합니다.</returns>
        public static Point[] RotatePointArray(float degree, PointF rotationCenter, Rectangle rectangle)
        {
            Point[] points = new Point[4];

            try
            {
                points[0] = new Point(rectangle.X, rectangle.Y);
                points[1] = new Point(rectangle.X, rectangle.Y + rectangle.Height);
                points[2] = new Point(rectangle.X + rectangle.Width, rectangle.Y);
                points[3] = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);

                if (degree != 0)
                {
                    Matrix myMatrix = new Matrix();
                    myMatrix.RotateAt(degree, rotationCenter);
                    myMatrix.TransformPoints(points);
                    myMatrix.Dispose();
                    myMatrix = null;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return points;

        }
        /// <summary>
        /// 지정된 지점의 회전 지점을 계산합니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="point">지점</param>
        /// <returns>회전된 지점을 반환합니다.</returns>
        public static Point RotatePoint(float degree, PointF rotationCenter, Point point)
        {
            if (degree == 0) return point;

            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(degree, rotationCenter);

            Point[] points = new Point[1];

            points[0] = point;
            myMatrix.TransformPoints(points);

            myMatrix.Dispose();
            myMatrix = null;

            return points[0];
        }
        /// <summary>
        /// 지정된 지점의 회전 지점을 계산합니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="point">지점</param>
        /// <returns>회전된 지점을 반환합니다.</returns>
        public static PointF RotatePointF(float degree, PointF rotationCenter, PointF point)
        {
            if (degree == 0) return point;

            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(degree, rotationCenter);

            PointF[] points = new PointF[1];

            points[0] = point;
            myMatrix.TransformPoints(points);

            myMatrix.Dispose();
            myMatrix = null;

            return points[0];
        }
        /// <summary>
        /// 지정된 영역의 회전 영역 중 좌측 최상단 위치값을 가져옵니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="rectangle">영역</param>
        /// <returns>회전된 영역중 좌측 최상단의 위치값을 반환합니다.</returns>
        public static Point GetLeftPoint(float degree, PointF rotationCenter, Rectangle rectangle)
        {
            if (degree == 0) return new Point(rectangle.X, rectangle.Y);

            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(degree, rotationCenter);

            Point[] points = new Point[4];

            points[0] = new Point(rectangle.X, rectangle.Y);
            points[1] = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            points[2] = new Point(rectangle.X + rectangle.Width, rectangle.Y);
            points[3] = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            myMatrix.TransformPoints(points);


            int tempIndex = 0;
            int minX = 999999;
            int minY = 999999;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].X <= minX)
                {
                    if (points[i].X == minX && minY < points[i].Y)
                    {
                        continue;
                    }
                    minX = points[i].X;
                    minY = points[i].Y;
                    tempIndex = i;
                }
            }

            myMatrix.Dispose();
            myMatrix = null;

            return points[tempIndex];
        }
        /// <summary>
        /// 지정된 영역의 회전 영역 중 우측 최상단 위치값을 가져옵니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="rectangle">영역</param>
        /// <returns>회전된 영역중 우측 최상단의 위치값을 반환합니다.</returns>
        public static Point GetRightPoint(float degree, PointF rotationCenter, Rectangle rectangle)
        {
            if (degree == 0)
            {
                return new Point(rectangle.X + rectangle.Width, rectangle.Y);
            }

            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(degree, rotationCenter);

            Point[] points = new Point[4];

            points[0] = new Point(rectangle.X, rectangle.Y);
            points[1] = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            points[2] = new Point(rectangle.X + rectangle.Width, rectangle.Y);
            points[3] = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            myMatrix.TransformPoints(points);

            int tempIndex = 0;
            int maxX = -9999;
            int minY = 999999;

            try
            {
                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].X >= maxX)
                    {
                        if (points[i].X == maxX && minY < points[i].Y)
                        {
                            continue;
                        }
                        maxX = points[i].X;
                        minY = points[i].Y;
                        tempIndex = i;
                    }
                }

                myMatrix.Dispose();
                myMatrix = null;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return points[tempIndex];
        }

        /// <summary>
        /// 지정된 영역의 회전 영역 중 중앙 위치값을 가져옵니다.
        /// </summary>
        /// <param name="degree">회전각</param>
        /// <param name="rotationCenter">회전축</param>
        /// <param name="rectangle">영역</param>
        /// <returns>회전된 영역 중 중앙 위치값을 반환합니다.</returns>
        public static Point GetCenterPoint(float degree, PointF rotationCenter, Rectangle rectangle)
        {
            if (degree == 0)
            {
                return new Point(rectangle.X + (rectangle.Width / 2), rectangle.Y + (rectangle.Height / 2));
            }

            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(degree, rotationCenter);

            Point[] points = new Point[4];

            points[0] = new Point(rectangle.X, rectangle.Y);
            points[1] = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            points[2] = new Point(rectangle.X + rectangle.Width, rectangle.Y);
            points[3] = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            myMatrix.TransformPoints(points);

            Point minPoint = MatrixUtil.GetMinPoint(points.ToList<Point>());
            Point maxPoint = MatrixUtil.GetMaxPoint(points.ToList<Point>());

            return new Point(minPoint.X + ((maxPoint.X - minPoint.X) / 2), minPoint.Y + ((maxPoint.Y - minPoint.Y) / 2));
        }

        /// <summary>
        /// 지정된 점들의 속한 최소 영역값을 가져옵니다.
        /// </summary>
        /// <param name="p1"></param>
        /// <returns></returns>
        private static RectangleF GetRectangleF(PointF[] p1)
        {
            float minX = 999999;
            float minY = 999999;
            float maxX = 0;
            float maxY = 0;

            for (int index = 0; p1.Length > index; index++)
            {
                if (p1[index].X < minX) minX = p1[index].X;
                if (p1[index].Y < minY) minY = p1[index].Y;
                if (p1[index].X > maxX) maxX = p1[index].X;
                if (p1[index].Y > maxY) maxY = p1[index].Y;
            }
            return new RectangleF(new PointF(minX, minY)
                , new SizeF(maxX - minX, maxY - minY));
        }

        /// <summary>
        /// 지정된 점들의 속한 최소 영역값을 가져옵니다.
        /// </summary>
        /// <param name="p1"></param>
        /// <returns></returns>
        private static Rectangle GetMBR(Point[] p1)
        {
            int minX = 999999;
            int minY = 999999;
            int maxX = 0;
            int maxY = 0;

            for (int index = 0; p1.Length > index; index++)
            {
                if (p1[index].X < minX) minX = p1[index].X;
                if (p1[index].Y < minY) minY = p1[index].Y;
                if (p1[index].X > maxX) maxX = p1[index].X;
                if (p1[index].Y > maxY) maxY = p1[index].Y;
            }
            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
        }

        /// <summary>
        /// 입력된 위치값 중 x, Y 최소값을 가져옵니다.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static Point GetMinPoint(List<Point> list)
        {
            int minX = 999999;
            int minY = 999999;
            for (int index = 0; index < list.Count; index++)
            {
                if (list[index].X < minX) minX = list[index].X;
                if (list[index].Y < minY) minY = list[index].Y;
            }

            return new Point(minX, minY);
        }
        /// <summary>
        /// 입력된 위치값 중 X, Y 최대값을 가져옵니다.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Point GetMaxPoint(List<Point> list)
        {
            int maxX = -999999;
            int maxY = -999999;
            for (int index = 0; index < list.Count; index++)
            {
                if (list[index].X > maxX) maxX = list[index].X;
                if (list[index].Y > maxY) maxY = list[index].Y;
            }

            return new Point(maxX, maxY);
        }
    }
}
