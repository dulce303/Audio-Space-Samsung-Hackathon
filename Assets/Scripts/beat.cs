using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioAnalyzer : MonoBehaviour
{
   public float bounceTreshold;
public Color startGradient;
public Color endGradient;
private Transform[] analyzerSegments;
public bool hasEqualizer;
   public AudioSource audioSource;
private const float EXTRUSION = 50f;

// An FFT returns a spectrum including frequencies that are out of human hearing range -
// this restricts the number of bins used from the spectrum to the lower @fftBounds.
//	[Range (8, 128)]
//	public
private int fftBounds = 32;

// Optionally weights the frequency amplitude when calculating extrude distance.
public AnimationCurve frequencyCurve;


private const float TWOPI = 6.283185f;
private const int FFT_SAMPLES = 4096 / 2;

private float[] fft = new float[FFT_SAMPLES];

//	public AudioSource audioSource;

private const int NUM_SEGMENTS = 20;
void Start ()
{
if (hasEqualizer) {
analyzerSegments = new Transform [NUM_SEGMENTS];
for (int i = 0; i < transform.childCount; i++) {
analyzerSegments [i] = transform.GetChild (i);
var coloramount = (float)i / (float)transform.childCount;
//transform.GetChild (i).GetComponent<Image> ().color = Color.Lerp (startGradient, endGradient, coloramount);
}
}
}

public interface BounceListner
{
void bounce (bool bounce);

void flow (float flow);
}

private List<BounceListner> bounceListeners = new List<BounceListner> ();

public void addBounceListener (BounceListner listener)
{
bounceListeners.Add (listener);
}

public void removeBounceListener (BounceListner listener)
{
bounceListeners.Remove (listener);
}

float getDisplacement (int index)
{
float normalizedIndex = (((float)index + 0.000001f) / (float)NUM_SEGMENTS);
int n = (int)(normalizedIndex * fftBounds);
float displacement = (fft [n] * (frequencyCurve.Evaluate (normalizedIndex) * .5f + .5f)) * EXTRUSION;
return displacement;
}

void Update ()
{
if (hasEqualizer) {
it cut it off
 void Update ()
{
if (hasEqualizer) {
//AudioSingleton.Instance.getCurrentMusicAudiSource().GetSpectrumData (fft, 0, FFTWindow.BlackmanHarris);



           audioSource.GetSpectrumData(fft, 0, FFTWindow.BlackmanHarris);


for (int i = 0; i < analyzerSegments.Length; i++) {

float Edisplacement = getDisplacement (i);

Vector3 vec = analyzerSegments [i].localScale;
vec.Set (1f, 1f + Edisplacement, 1f);
analyzerSegments [i].localScale = vec;
}
}
if (bounceListeners.Count != 0) {


               audioSource.GetSpectrumData(fft, 0, FFTWindow.BlackmanHarris);

//AudioSingleton.Instance.getCurrentMusicAudiSource().GetSpectrumData (fft, 0, FFTWindow.BlackmanHarris);

float displacement = getDisplacement (1);
float displacementFlow = getDisplacement (2);

for (int i = 0; i < analyzerSegments.Length; i++) {

               displacementFlow += getDisplacement(i);
}
for (int i = 0; i < bounceListeners.Count; i++) {

bounceListeners [i].bounce (displacement > bounceTreshold);
bounceListeners [i].flow (displacementFlow);
}
}
}
}