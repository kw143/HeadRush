using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    public bool triggerDeath;
    public PostEffectMaskRenderer mask1;
    public PostEffectMaskRenderer mask2;
    public Material fadeMat;
    private Camera mainCam1;
    private Camera mainCam2;
    private PostEffectMask camMask1;
    private PostEffectMask camMask2;
    bool dying = false;
    float blur = 0;
    float alpha = 1;

    // Use this for initialization
    void Start() {
        mask1.enabled = false;
        mask2.enabled = false;
        //GetComponent<Renderer>().material.CopyPropertiesFromMaterial(mat);
        mainCam1 = GameObject.Find("PlayerOneCamera").GetComponent<Camera>();
        mainCam2 = GameObject.Find("PlayerTwoCamera").GetComponent<Camera>();
        camMask1 = mainCam1.GetComponent<PostEffectMask>();
        camMask2 = mainCam2.GetComponent<PostEffectMask>();
    }

    // Update is called once per frame
    void Update() {
        if (triggerDeath) {
            mask1.enabled = true;
            mask2.enabled = true;
            dying = true;
            triggerDeath = false;
            GetComponent<Renderer>().material = fadeMat;
        }
        if (dying) {
            blur = blur + 0.08f;
            camMask1.blur = blur;
            camMask2.blur = blur;
            if (blur > 1) {
                Color currCol = GetComponent<Renderer>().material.color;
                currCol.a = alpha;
                alpha = alpha - 0.02f;
                GetComponent<Renderer>().material.SetColor("_Color", currCol);
            }
            if (alpha <= 0) {
                gameObject.SetActive(false);
            }
        }
    }


}
