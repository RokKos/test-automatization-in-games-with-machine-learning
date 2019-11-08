using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


    public class MarchingSquaresTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void MarchingSquaresTestSimplePasses()
        {
            // Use the Assert class to test conditions
            //MarchingSquares marchingSquares = new MarchingSquares();
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MarchingSquaresTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void  BasicLookUp00()
        {
            //MarchingSquares marchingSquares = new MarchingSquares();

            //BlaBla bla = new BlaBla();
            //Assert.Equals(0, marchingSquares.LookUpLine());
            Assert.Fail();
        }
    }

