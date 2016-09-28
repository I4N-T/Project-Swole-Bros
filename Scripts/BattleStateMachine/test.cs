using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class test : MonoBehaviour {

	public int theTest;
	public float testvar1;

	public void TestMethod() {

		theTest = GetRandomValue ();
		testvar1 = GetFloattest ();

		Debug.Log (testvar1);
		Debug.Log (theTest);

	}

	int GetRandomValue() {
		float rand = Random.value;
		if (rand <= .5f)
			return Random.Range(0, 6);
		if (rand <= .8f)
			return Random.Range(6, 9);
		
		return Random.Range(9, 11);

	}
	float GetFloattest() {
		float testvar = Random.value;

		
		return testvar;
		
	}
}
