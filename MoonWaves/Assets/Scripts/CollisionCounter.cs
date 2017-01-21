using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour {
    public TextMesh Text;
	
    private void Update() {
		Text.text = WaterParticle.waveCollisionCountAvg.ToString();
    }
}
