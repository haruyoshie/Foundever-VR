using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.Video;
using Defective.JSON;
using Unity.VisualScripting;
using System.Collections.Generic;
//using static UnityEditor.PlayerSettings;

public class tvscreen : MonoBehaviour
{
    public List<string> urlVideosBD;
    public GameObject camPrincipal, camTv,UIVer,instruccion;
    //public string[] urlVideos;
    //public Texture[] tvSlides;
    //public VideoClip[] tvVideos;
    public VideoPlayer _videoPlayer;
    public RawImage _image;
    public Texture _texture;
    public float lastFrame;
    public AudioSource source;
    public string urlTvApp = "http://us0145lapp01.lat.sitel-world.net/CO/RED/PROD/APIS/Api_TVApp/GetContent/Instance/";
    public string finalString;
    string finalURL = "https://US145K12col01//General/TVApp/Multimedia";
    //public GameObject[] PantallaVideos;
    public int numSlide, contadorImgDB;
    public bool t;
    public int total;
    public double durationVideo;

    void Start()
    {
        contadorImgDB = 1;
        t = true;
        //int random_img = Random.Range(0, url.Length);
        // StartCoroutine(RandomVid());
        StartCoroutine(DownloadVideos());
    }

    IEnumerator DownloadVideos()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlTvApp))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("error " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string consulta = webRequest.downloadHandler.text;
                    JSONObject json = new JSONObject(consulta);
                    Debug.Log("el json es " + json.ToString());

                    for (int i = 0; i < json.count; i++)
                    {
//                        Debug.Log(json.count);
                        string tipoArchivo = json[i]["TpoArchvo"].ToString().Replace("\"", "");
                        //Debug.Log(tipoArchivo);
                        if (tipoArchivo.Equals("video/mp4"))
                        {
                            finalString = json[i]["Rta"].ToString().Replace("\"", "");
                            urlVideosBD.Add(finalString);
                        }
                        if (tipoArchivo.Equals("image/jpg") || tipoArchivo.Equals("image/jpeg"))
                        {
                            finalString = json[i]["Rta"].ToString().Replace("\"", "");
                            urlVideosBD.Add(finalString);
                        }
                    }
                    StartCoroutine(DisplayMedia());
                    break;
            }
        }
    }
    IEnumerator DisplayMedia()
    {
        foreach (string url in urlVideosBD)
        {
            if (IsImage(url)) // Check if the URL points to an image
            {
                if(_videoPlayer.isPlaying) yield break;
                _image.enabled = true;
                //_videoPlayer.enabled = false;
                //source.enabled = false;
                yield return StartCoroutine(LoadAndDisplayImage(url));
                yield return new WaitForSeconds(3f); // Display image for 3 seconds
            }
            else // Assume it's a video URL
            {
                if (url.Contains("mp4"))
                {
                    yield return StartCoroutine(PlayVideo(url));
                    _image.enabled = false;
                    //source.enabled = true;
                    yield return new WaitUntil(() => !_videoPlayer.isPlaying); // Wait until video finishes
                }
                
            }
        }

        StartCoroutine(DisplayMedia());
    }

    bool IsImage(string url)
    {
        // Check if the file extension is PNG or JPG
        return url.EndsWith(".png") || url.EndsWith(".jpg") || url.EndsWith(".jpeg");
    }

     IEnumerator LoadAndDisplayImage(string imageUrl)
    {
        // Load and display the image
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                _image.texture = texture;
            }
            else
            {
                Debug.LogError("Failed to download image: " + webRequest.error);
            }
        }
    }

    IEnumerator PlayVideo(string videoUrl)
    {
        _videoPlayer.url = videoUrl;
        _videoPlayer.Prepare();

        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }

        _videoPlayer.Play();
    }

    IEnumerator RandomVid()
    {
        if (contadorImgDB < urlVideosBD.Count && !_videoPlayer.isPlaying)
        {
            _videoPlayer.url = urlVideosBD[contadorImgDB];
            _videoPlayer.Prepare();

            while (!_videoPlayer.isPrepared)
            {
                yield return null;
            }

            Debug.Log($"<color=#ff0f09>LA DURACIÓN DEL VIDEO ES: </color>" + _videoPlayer.length);
            durationVideo = _videoPlayer.length;
            double minutes = (_videoPlayer.length / 60);
            Mathf.Floor(((float)minutes));
            Debug.Log($"<color=#00FF00>LA DURACIÓN DEL VIDEO EN MINUTOS ES: </color>" + Mathf.Floor(((float)minutes)));

            _videoPlayer.Play();

            // Wait until the video has finished playing
            while (_videoPlayer.isPlaying)
            {
                yield return null;
            }

            // Video has finished playing, you can add any additional logic here

            contadorImgDB++; // Move to the next video
            StartCoroutine(RandomVid()); // Start playing the next video
        }
        else
        {
            Debug.Log("All videos have been played.");
        }
    }


    /*IEnumerator RandomVid()
    {
        if (contadorImgDB <= urlVideosBD.Count)
        {
            _videoPlayer.url = urlVideosBD[contadorImgDB];
        }
        
        //int random_vid = Random.Range(0, urlVideos.Length);
        Debug.Log(_videoPlayer.isPrepared);
        if(_videoPlayer.isPrepared ) 
        {
            Debug.Log($"<color=#ff0f09>LA DURACIÓN DEL VIDEO ES: </color>" + _videoPlayer.length);
            durationVideo = _videoPlayer.length;
            double minutes;
            minutes = (_videoPlayer.length / 60);
            Mathf.Floor(((float)minutes));
            Debug.Log($"<color=#00FF00>LA DURACIÓN DEL VIDEO EN MINUTOS ES: </color>" + Mathf.Floor(((float)minutes)));
            _videoPlayer.url = urlVideosBD[contadorImgDB];
            //_videoPlayer.Play();
            //yield return new WaitForSeconds(2f);
        }
        
        //_videoPlayer.enabled = true;
        ////_image.texture = tvSlides[random_vid];
        ////_videoPlayer.clip = tvVideos[random_vid];
        //_videoPlayer.GetTargetAudioSource(_videoPlayer.audioTrackCount);
        //// _videoPlayer.SetDirectAudioVolume(_videoPlayer.audioTrackCount, 0.01f);
        //_videoPlayer.SetTargetAudioSource(_videoPlayer.audioTrackCount, source);
        //lastFrame = _videoPlayer.frameCount;
        //_videoPlayer.Play();
        yield return null;
    }
    private IEnumerator loadVideosFromURL(string videoURL)
    {
        UnityWebRequest _videoRequest = UnityWebRequest.Get(videoURL);
        yield return _videoRequest.SendWebRequest();

        if (_videoRequest.isDone == false || _videoRequest.error != null)
        {
            Debug.Log(_videoRequest.error);
        }
        Debug.Log("video done - " + _videoRequest.isDone);
        byte[] _videoBytes = _videoRequest.downloadHandler.data;

        string _pathToFile = Path.Combine(Application.persistentDataPath, "movie.MP4");
        File.WriteAllBytes(_pathToFile, _videoBytes);
        StartCoroutine(playThisURLInVideo(_pathToFile));
        yield return null;
    }
    private IEnumerator playThisURLInVideo(string _url)
    {
        _videoPlayer.source = UnityEngine.Video.VideoSource.Url;
        _videoPlayer.url = _url;
        _videoPlayer.Prepare();

        while (_videoPlayer.isPrepared == false)
        { yield return null; }

        Debug.Log("Video should play");
        _videoPlayer.Play();
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            source.volume = 0.039f;
            UIVer.SetActive(true);
            InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("salio");
            source.volume = 0f;
            camPrincipal.SetActive(true);
            camTv.SetActive(false);
            UIVer.SetActive(false);
            instruccion.SetActive(false);
            InteractionManager.Instance.SetInteractState(InteractionState.Free);
        }
    }
    public void exitTV()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaa");
        camPrincipal.SetActive(true);
        camTv.SetActive(false);
       
        //UIVer.SetActive(false);
        //InteractionManager.Instance.SetInteractState(InteractionState.Free);
    }
   /* private void Update()
    {
        if(contadorImgDB >= 50)
        {
            t = false;
            contadorImgDB= 0;
        }if(contadorImgDB < 50) 
        {
            t = true;
        }
        if (_videoPlayer.clip != null && numSlide == 4)
        {
            //_videoPlayer.SetTargetAudioSource(_video)
            Debug.Log(_videoPlayer.frame);
            if (_videoPlayer.frame + 1 == lastFrame || _videoPlayer.frame + 10 >= lastFrame)
            {
                //_videoPlayer.clip = null;
                _videoPlayer.enabled = false;
                //StartCoroutine(RandomImg());
            }
        }

    }*/
}