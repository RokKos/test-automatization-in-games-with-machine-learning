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
        private GameObject go = null;
        private MarchingSquares marchingSquares = null;

        [SetUp]
        public void SetupMarchingSquaresTest()
        {
            go = new GameObject();
            marchingSquares = go.AddComponent<MarchingSquares>();
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
    }
}
