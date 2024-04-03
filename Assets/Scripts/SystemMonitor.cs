using UnityEngine;
using System.Diagnostics;
using TMPro;
using UnityEngine.Profiling;

public class SystemMonitor : MonoBehaviour
{
    #region Variables

    [SerializeField] private TextMeshProUGUI _cpuText;
    [SerializeField] private TextMeshProUGUI _gpuText;
    [SerializeField] private TextMeshProUGUI _ramText;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _stopButton;
    
    private bool _isTracking;
    
    #endregion

    #region UnityCallbacks
    
    // Update method to calculate and display CPU, GPU, and RAM usage on the UI.
    private void Update()
    {
        if(!_isTracking) return;
        
        // CPU Usage
        var cpuUsage = Mathf.Round(Profiler.GetTotalReservedMemoryLong() / 1024f / 1024f);

        // GPU Usage
        var gpuUsage = Mathf.Round(Profiler.GetTotalAllocatedMemoryLong() / 1024f / 1024f);

        // RAM Usage
        var ramUsage = Mathf.Round(Process.GetCurrentProcess().WorkingSet64 / 1024f / 1024f);

        // Update UI
        _cpuText.text = "CPU Usage: " + cpuUsage + " MB";
        _gpuText.text = "GPU Usage: " + gpuUsage + " MB";
        _ramText.text = "RAM Usage: " + ramUsage + " MB";
    }
    
    #endregion

    #region PrivateMethods

    // SetTracking sets the tracking value and updates the start and stop buttons accordingly. 
    private void SetTracking(bool value)
    {
        _isTracking = value;
        _startButton.SetActive(!value);
        _stopButton.SetActive(value);
    }
    
    #endregion

    #region UICallbacks
    
    // Method to handle the event when the "Start Tracking" button is clicked.
    public void OnStartTrackingClicked()
    {
        SetTracking(true);
    }

    // Method to handle the event when the "Stop Tracking" button is clicked.
    public void OnStopTrackingClicked()
    {
        SetTracking(false);
    }
    
    #endregion
}
