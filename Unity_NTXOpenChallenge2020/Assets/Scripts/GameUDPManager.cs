using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class GameUDPManager : MonoBehaviour
{
    private TextMeshProUGUI m_Text;
    private int port;
    private Slider m_Slider;
    private UDPSocket socket;
    private Toggle toggle;
    private Slider sliderOverride;
    public Transform platform;

    public RotatorX[] rotator;

    private float myoValue;

    private void Awake()
    {
        // Set the culture to english, to force numbers to respect the english format.
        // e.g. "en-us" is formatted as 5.0 and "fr-ca" is formatted as 5,0.
        var culture = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 200; // target an average of 200 FPS

        m_Text = GameObject.Find("udp_values").GetComponent<TextMeshProUGUI>();
        m_Slider = GameObject.Find("Slider").GetComponent<Slider>();
        sliderOverride = GameObject.Find("SliderOverride").GetComponent<Slider>();
        toggle = GameObject.Find("Toggle").GetComponent<Toggle>();

        myoValue = 0;

        ChangePort();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn)
        {
            myoValue = sliderOverride.value;
            m_Text.text = string.Format("Override value: {0}", sliderOverride.value.ToString("0.00"));

            MoveRotator();
            return; // exit update()
        }

        if (socket != null)
        {
            string data = socket.readData;

            if (data == string.Empty) return;

            m_Text.text = string.Format("UDP Port 5020: {0}", data);

            float.TryParse(data, out float result);
            myoValue = result;
            m_Slider.value = result;
        }

        MoveRotator();
    }

    private void MoveRotator()
    {
        // Map Range, map the value, from [0,5] to [1,0]
        float speedRotator = Mathf.Lerp(1, 0, Mathf.InverseLerp(0, 5, myoValue));

        foreach (var item in rotator)
        {
            item.percentSpeed = speedRotator; // Normal speed is 1.0 or 100%
        }
    }

    public void ChangePort()
    {
        int.TryParse(GameObject.Find("InputField_Port").GetComponent<TMP_InputField>().text, out port);

        if (socket != null)
        {
            socket.AbortConnection();
        }

        try
        {
            socket = new UDPSocket(port);
        }
        catch (System.Exception e)
        {
            m_Text.text = string.Format("Error: {0}", e.Message);
            socket = null;
        }
    }

    public void OnApplicationQuit()
    {
        // Close open socket before the application quit
        socket.AbortConnection();
    }
}
