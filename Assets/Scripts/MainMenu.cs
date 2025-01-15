using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuBack;
    public GameObject Story;
    public GameObject Setting;

    public GameObject CheckMusic;
    public GameObject CheckSound;

    public Slider SliderMusic;
    public Slider SliderSound;

    public int isMusic = 0;
    public int isSound = 0;

    public float musicVolume = 1;
    public float soundVolume = 1;

    void Start()
    {
        //CheckMusic.SetActive(true);
        //CheckSound.SetActive(true);

        //SliderMusic.onValueChanged.AddListener(delegate { MusicValueChange(); });
        //SliderSound.onValueChanged.AddListener(delegate { SoundValueChange(); });
    }

    void MusicValueChange()
    {
        Debug.Log(SliderMusic.value);
    }

    void SoundValueChange()
    {
        Debug.Log(SliderSound.value);
    }

    void OpenStory()
    {
        Story.SetActive(true);
        Story.GetComponent<Animator>().SetTrigger("Open");
    }

    void OpenSetting()
    {
        Setting.SetActive(true);
        Setting.GetComponent<Animator>().SetTrigger("Open");
    }

     void OpenMenuBack()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Open");
        Story.SetActive(false);
        Setting.SetActive(false);
    }

    public void BtnStory()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenStory", 1.5f);
    }

    public void BtnSetting()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenSetting", 1.5f);
    }

    public void BtnCloseStory()
    {
        Story.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenMenuBack", 1.5f);
    }

    public void BtnCloseSetting()
    {
        Setting.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenMenuBack", 1.5f);
    }

    public void BtnMusic()
    {
        CheckMusic.SetActive(!CheckMusic.activeInHierarchy);
    }

    public void BtnSound()
    {
        CheckMusic.SetActive(!CheckSound.activeInHierarchy);
    }

}
