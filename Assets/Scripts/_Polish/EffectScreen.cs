using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EffectScreen : MonoBehaviour {

    public Image flashImage;
    public ParticleSystem finishParticle;

	void Start () {

        WordHunt.Finish += Finish;
		
	}

    public void Finish(){
        finishParticle.Play();
        flashImage.DOFade(1,.1f).OnComplete(()=>flashImage.DOFade(0,.2f));
    }


    private void OnDestroy()
    {
        WordHunt.Finish -= Finish;
    }
}
