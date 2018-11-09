using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Death : MonoBehaviour {
    public PostProcessingProfile ppProfile;
    public bool death;
    public int count = 0;
    GrainModel.Settings grainSettings;
	// Use this for initialization
	void Start () {
        grainSettings = ppProfile.grain.settings;
        grainSettings.intensity = 0;
        ppProfile.grain.settings = grainSettings;

    }
	
	// Update is called once per frame
	void Update () {
		if (death) {
            if(grainSettings.intensity < 50) {
                grainSettings.intensity = (count * count) / 10000.0f;
            }
            grainSettings.size = 0.2f * Mathf.Sin(count) + 1.3f ;
            count++;
            ppProfile.grain.settings = grainSettings;
        }
	}
}
