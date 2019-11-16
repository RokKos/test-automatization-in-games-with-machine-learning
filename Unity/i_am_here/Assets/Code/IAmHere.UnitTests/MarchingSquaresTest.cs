using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using IAmHere.WorldGeneration;

namespace IAmHere.UnitTests
{
    
    [TestFixture]
    public class MarchingSquaresTest
    {
        private MarchingSquares marchingSquares = null;

        [SetUp]
        public void SetupMarchingSquaresTest()
        {
            marchingSquares = new MarchingSquares();
        }

        [Test]
        public void LineLookUp00()
        {
            
            Assert.AreEqual(0, marchingSquares.LineLookUp(new bool[,]
            {
                {false, false},
                {false, false}
            }));

        }
        
        [Test]
        public void LineLookUp01()
        {
            
            Assert.AreEqual(1, marchingSquares.LineLookUp(new bool[,]
            {
                {false, false},
                {true, false}
            }));

        }
        
        [Test]
        public void LineLookUp02()
        {
            
            Assert.AreEqual(2, marchingSquares.LineLookUp(new bool[,]
            {
                {false, false},
                {false, true}
            }));

        }
        
        [Test]
        public void LineLookUp03()
        {
            
            Assert.AreEqual(3, marchingSquares.LineLookUp(new bool[,]
            {
                {false, false},
                {true, true}
            }));

        }
        
        [Test]
        public void LineLookUp04()
        {
            
            Assert.AreEqual(4, marchingSquares.LineLookUp(new bool[,]
            {
                {false, true},
                {false, false}
            }));

        }
        
        [Test]
        public void LineLookUp05()
        {
            
            Assert.AreEqual(5, marchingSquares.LineLookUp(new bool[,]
            {
                {false, true},
                {true, false}
            }));

        }
        
        [Test]
        public void LineLookUp06()
        {
            
            Assert.AreEqual(6, marchingSquares.LineLookUp(new bool[,]
            {
                {false, true},
                {false, true}
            }));

        }
        
        [Test]
        public void LineLookUp07()
        {
            
            Assert.AreEqual(7, marchingSquares.LineLookUp(new bool[,]
            {
                {false, true},
                {true, true}
            }));

        }
        
        [Test]
        public void LineLookUp08()
        {
            
            Assert.AreEqual(8, marchingSquares.LineLookUp(new bool[,]
            {
                {true, false},
                {false, false}
            }));

        }
        
        [Test]
        public void LineLookUp09()
        {
            
            Assert.AreEqual(9, marchingSquares.LineLookUp(new bool[,]
            {
                {true, false},
                {true, false}
            }));

        }
        
        [Test]
        public void LineLookUp10()
        {
            
            Assert.AreEqual(10, marchingSquares.LineLookUp(new bool[,]
            {
                {true, false},
                {false, true}
            }));

        }
        
        [Test]
        public void LineLookUp11()
        {
            
            Assert.AreEqual(11, marchingSquares.LineLookUp(new bool[,]
            {
                {true, false},
                {true, true}
            }));

        }
        
        [Test]
        public void LineLookUp12()
        {
            
            Assert.AreEqual(12, marchingSquares.LineLookUp(new bool[,]
            {
                {true, true},
                {false, false}
            }));

        }
        
        [Test]
        public void LineLookUp13()
        {
            
            Assert.AreEqual(13, marchingSquares.LineLookUp(new bool[,]
            {
                {true, true},
                {true, false}
            }));

        }
        
        [Test]
        public void LineLookUp14()
        {
            
            Assert.AreEqual(14, marchingSquares.LineLookUp(new bool[,]
            {
                {true, true},
                {false, true}
            }));

        }
        
        [Test]
        public void LineLookUp15()
        {
            
            Assert.AreEqual(15, marchingSquares.LineLookUp(new bool[,]
            {
                {true, true},
                {true, true}
            }));

        }
        
        [Test]
        public void GridMedium()
        {
            
            bool[,] gridMedium = new bool[,]
            {
                {true, true, true, true, true,true, true, true, true, true },
                {true, false, false, false, true, true, true, false, false, true },
                {true, false, true, false, true, false, false, false, false, true },
                {true, false, true, true, true, false, false, false, false, true },
                {true, true, true, true, true, true, true, true, true, true },
            };
            
            byte[,] result = new byte[,]
            {
                {13, 12, 12, 14, 15, 15, 13, 12, 14},
                {9, 2, 1, 6, 13, 12, 8, 0, 6},
                {9, 6, 11, 7, 9, 0, 0, 0, 6},
                {11, 7, 15, 15, 11, 3, 3, 3, 7}
            };
            
            Assert.AreEqual(result, marchingSquares.ParseGrid(gridMedium));

        }
        
        
    }
}
