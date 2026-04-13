using NUnit.Framework;
using System.Collections;
using Test;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class CoinTest
    {
        [UnityTest]
        public IEnumerator CoinTestWithEnumeratorPasses()
        {
            SceneManager.LoadScene("Test");
            yield return null;

            var test = new MonoBehaviourTest<CoinTestBehaviour>();
            yield return test;

            var coin = GameObject.FindWithTag("Coin");
            Assert.IsTrue(coin == null,"Coin in scene and not pick up yet");
        }
    }
}
