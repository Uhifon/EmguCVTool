//----------------------------------------------------------------------------
//  Copyright (C) 2004-2016 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;

namespace EmgucvDemo
{
    public static class DrawMatches
    {
        public static void FindMatch(Mat modelImage, Mat observedImage, out long matchTime, out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, out Mat mask, out Mat homography, out long score)
        {
            int k = 2;
            double uniquenessThreshold = 0.80;
            Stopwatch watch;
            homography = null;
            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();

            using (UMat uModelImage = modelImage.GetUMat(AccessType.Read))
            using (UMat uObservedImage = observedImage.GetUMat(AccessType.Read))
            {               

               //   KAZE featureDetector = new KAZE();
              //     SURF featureDetector = new SURF(100);
                //    SIFT featureDetector = new SIFT();
                ORBDetector featureDetector = new ORBDetector();
                Mat modelDescriptors = new Mat();
                //进行检测和计算，把opencv中的两部分和到一起了，分开用也可以
                featureDetector.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);
                watch = Stopwatch.StartNew();

                Mat observedDescriptors = new Mat();
                featureDetector.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);

                // KdTree for faster results / less accuracy
                using (var ip = new Emgu.CV.Flann.KdTreeIndexParams())
                using (var sp = new SearchParams())
                //  using (DescriptorMatcher matcher = new FlannBasedMatcher(ip, sp))//开始进行匹配
                using (BFMatcher matcher = new BFMatcher(DistanceType.L2))
                {
                    matcher.Add(modelDescriptors);
                    matcher.KnnMatch(observedDescriptors, matches, k, null);
                    mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                    mask.SetTo(new MCvScalar(255));
                    Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);//去除重复的匹配

                    // Calculate score based on matches size
                    // ---------------------------------------------->
                    score = 0;
                    for (int i = 0; i < matches.Size; i++)
                    {
                        if (mask.GetData(i)[0] == 0) continue;
                        foreach (var e in matches[i].ToArray())
                            ++score;
                    }
                    // <----------------------------------------------

                    int nonZeroCount = CvInvoke.CountNonZero(mask);//用于寻找模板在图中的位置
                    if (nonZeroCount >= 4)
                    {
                        nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);
                        if (nonZeroCount >= 4)
                            homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, matches, mask, 2);
                    }
                }
                watch.Stop();

            }
            matchTime = watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Draw the model image and observed image, the matched features and homography projection.
        /// </summary>
        /// <param name="modelImage">The model image</param>匹配特征模板
        /// <param name="observedImage">The observed image</param>被匹配图像
        /// <param name="matchTime">The output total time for computing the homography matrix.</param>
        /// <returns>The model image and observed image, the matched features and homography projection.</returns>
        public static Mat Draw(Mat modelImage, Mat observedImage, out long matchTime, out long score,out Point[] observedPoints)
        {
            Mat homography;
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;

            observedPoints = new Point[4];
            using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                Mat mask;
                FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                   out mask, out homography, out score);

                //Draw the matched keypoints
                Mat result = new Mat();
                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                   matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);

                #region draw the projected region on the image

                if (homography != null)   //如果在图中找到了模板，就把它画出来
                {
                    //draw a rectangle along the projected model
                    Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                    PointF[] pts = new PointF[]
                    {
                      new PointF(rect.Left, rect.Bottom),
                      new PointF(rect.Right, rect.Bottom),
                      new PointF(rect.Right, rect.Top),
                      new PointF(rect.Left, rect.Top)
                    };
                    pts = CvInvoke.PerspectiveTransform(pts, homography);
                    Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
                    using (VectorOfPoint vp = new VectorOfPoint(points))
                    {
                        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        observedPoints[i].X = points[i].X;
                        observedPoints[i].Y = points[i].Y;
                    }
                }
                #endregion
               
                //比较结果
                return result;

            }
        }

        
    }
}