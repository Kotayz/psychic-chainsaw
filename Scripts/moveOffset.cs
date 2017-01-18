using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {

    private Material currentMaterial;
    public float velocity;
    private float offset;

	// Use this for initialization
	void Start () {
        currentMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        offset += 0.01f;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * velocity, 0));
    }
}
