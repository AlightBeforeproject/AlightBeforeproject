using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log(operation.progress);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}

/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LOAD : MonoBehaviour {
 public string cenaACarregar;
 public float TempoFixoSeg = 5;
 public enum TipoCarreg {Carregamento, TempoFixo};
 public TipoCarreg TipoDeCarregamento;
 public Image barraDeCarregamento;
 public Text TextoProgresso;
 private int progresso = 0;
 private string textoOriginal;

 void Start () {
 switch (TipoDeCarregamento) {
 case TipoCarreg.Carregamento:
 StartCoroutine (CenaDeCarregamento (cenaACarregar));
 break;
 case TipoCarreg.TempoFixo:
 StartCoroutine (TempoFixo (cenaACarregar));
 break;
 }
 //
 if (TextoProgresso != null) {
 textoOriginal = TextoProgresso.text;
 }
 if (barraDeCarregamento != null) {
 barraDeCarregamento.type = Image.Type.Filled;
 barraDeCarregamento.fillMethod = Image.FillMethod.Horizontal;
 barraDeCarregamento.fillOrigin = (int)Image.OriginHorizontal.Left;
 }
 }

 IEnumerator CenaDeCarregamento(string cena){
 AsyncOperation carregamento = SceneManager.LoadSceneAsync (cena);
 while (!carregamento.isDone) {
 progresso = (int)(carregamento.progress * 100.0f);
 yield return null;
 }
 }

 IEnumerator TempoFixo(string cena){
 yield return new WaitForSeconds (TempoFixoSeg);
 SceneManager.LoadScene (cena);
 }

 void Update () {
 switch (TipoDeCarregamento) {
 case TipoCarreg.Carregamento:
 break;
 case TipoCarreg.TempoFixo:
 progresso = (int)(Mathf.Clamp((Time.time / TempoFixoSeg),0.0f,1.0f)* 100.0f);
 break;
 }
 if (TextoProgresso != null) {
 TextoProgresso.text = textoOriginal + " " + progresso + "%";
 }
 if (barraDeCarregamento != null) {
 barraDeCarregamento.fillAmount = (progresso / 100.0f);
 }
 }
}
*/
