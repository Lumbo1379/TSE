using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        private GameObject _player;

        [SetUp]
        public void Setup()
        {
            _player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player/FPS Player"));
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_player);
        }

        [UnityTest]
        public IEnumerator OxygenDepletes()
        {
            var oxygenMeter = _player.GetComponent<OxygenMeter>();
            oxygenMeter.BeginDepletingOxygen();

            float initialOxygen = oxygenMeter.Oxygen;

            yield return new WaitForSeconds(oxygenMeter.OxygenTick);

            Assert.Less(oxygenMeter.Oxygen, initialOxygen);
        }

        [UnityTest]
        public IEnumerator OxygenIncreasesOnSuccessfulSuck()
        {
            var oxygenMeter = _player.GetComponent<OxygenMeter>();
            oxygenMeter.Oxygen = 50;

            float initialOxygen = oxygenMeter.Oxygen;

            oxygenMeter.UpdateMeterOnSuccessfulSuck(10);

            yield return new WaitForSeconds(0.1f);

            Assert.Greater(oxygenMeter.Oxygen, initialOxygen);
        }
    }
}
