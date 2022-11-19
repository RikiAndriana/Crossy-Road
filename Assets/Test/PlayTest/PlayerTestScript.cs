using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTestScript
{
    // // A Test behaves as an ordinary method
    // [Test]
    // public void PlayerTestScriptSimplePasses()
    // {
    //     // Use the Assert class to test conditions
    // }

    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator PlayerTestScriptWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }

    [UnityTest]
    public IEnumerator Player_Jump_Test()
    {
        GameObject playerObject = new GameObject("Player");
        playerObject.GetComponent<Player>();
        yield return null;
    }
}
