using DT_pihmed_pozicio.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DT_pihmed_pozicio.Tests.Data
{
    [TestClass]
    public class CanvasCoordinatesTests
    {
        [TestMethod]
        public void ValidateCanvasCoordinates_100_100_100_100()
        {
            string filePath_100_100 = Path.Combine(Environment.CurrentDirectory, "TestData", "Square_100_100.csv");
            CanvasCoordinates.Correction = 1;

            CanvasCoordinates canvasCoordinates = new CanvasCoordinates(filePath_100_100, 100, 100);

            var data = new List<Point>(canvasCoordinates.Data);

            Assert.IsTrue(data[0].X == 100            && data[0].Y == 100);
            Assert.IsTrue(data[1].X - 0 < 0.000000001 && data[1].Y == 100);
            Assert.IsTrue(data[2].X - 0 < 0.000000001 && data[2].Y - 0 < 0.000000001);
            Assert.IsTrue(data[3].X == 100            && data[3].Y - 0 < 0.000000001);
        }

        [TestMethod]
        public void ValidateCanvasCoordinates_100_100_50_50()
        {
            string filePath_100_100 = Path.Combine(Environment.CurrentDirectory, "TestData", "Square_100_100.csv");
            CanvasCoordinates.Correction = 1;

            CanvasCoordinates canvasCoordinates = new CanvasCoordinates(filePath_100_100, 50, 50);

            var data = new List<Point>(canvasCoordinates.Data);

            Assert.IsTrue(data[0].X == 50 && data[0].Y == 50);
            Assert.IsTrue(data[1].X - 0 < 0.000000001 && data[1].Y == 50);
            Assert.IsTrue(data[2].X - 0 < 0.000000001 && data[2].Y - 0 < 0.000000001);
            Assert.IsTrue(data[3].X == 50 && data[3].Y - 0 < 0.000000001);
        }

        [TestMethod]
        public void ValidateCanvasCoordinates_100_100_200_200()
        {
            string filePath_100_100 = Path.Combine(Environment.CurrentDirectory, "TestData", "Square_100_100.csv");
            CanvasCoordinates.Correction = 1;

            CanvasCoordinates canvasCoordinates = new CanvasCoordinates(filePath_100_100, 200, 200);

            var data = new List<Point>(canvasCoordinates.Data);

            Assert.IsTrue(data[0].X == 200 && data[0].Y == 200);
            Assert.IsTrue(data[1].X - 0 < 0.000000001 && data[1].Y == 200);
            Assert.IsTrue(data[2].X - 0 < 0.000000001 && data[2].Y - 0 < 0.000000001);
            Assert.IsTrue(data[3].X == 200 && data[3].Y - 0 < 0.000000001);
        }

        [TestMethod]
        public void ValidateCanvasCoordinates_100_100_50_200()
        {
            string filePath_100_100 = Path.Combine(Environment.CurrentDirectory, "TestData", "Square_100_100.csv");
            CanvasCoordinates.Correction = 1;

            CanvasCoordinates canvasCoordinates = new CanvasCoordinates(filePath_100_100, 50, 200);

            var data = new List<Point>(canvasCoordinates.Data);

            Assert.IsTrue(data[0].X == 50 && data[0].Y == 125);
            Assert.IsTrue(data[1].X - 0 < 0.000000001 && data[1].Y == 125);
            Assert.IsTrue(data[2].X - 0 < 0.000000001 && data[2].Y - 75 < 0.000000001);
            Assert.IsTrue(data[3].X == 50 && data[3].Y == 75);
        }

        [TestMethod]
        public void ValidateCanvasCoordinates_100_100_200_50()
        {
            string filePath_100_100 = Path.Combine(Environment.CurrentDirectory, "TestData", "Square_100_100.csv");
            CanvasCoordinates.Correction = 1;

            CanvasCoordinates canvasCoordinates = new CanvasCoordinates(filePath_100_100, 200, 50);

            var data = new List<Point>(canvasCoordinates.Data);

            Assert.IsTrue(data[0].X == 125 && data[0].Y == 50);
            Assert.IsTrue(data[1].X - 75 < 0.000000001 && data[1].Y == 50);
            Assert.IsTrue(data[2].X - 75 < 0.000000001 && data[2].Y - 0 < 0.000000001);
            Assert.IsTrue(data[3].X == 125 && data[3].Y == 0);
        }
    }
}
