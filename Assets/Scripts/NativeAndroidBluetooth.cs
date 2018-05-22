using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NativeAndroid : MonoBehaviour
{
		private const string BLUETOOTH_CLASS = "com.smartpalm.nativebluetoothplugin.BluetoothService";

		[SerializeField]
    private Text _searchingLabel;

		[SerializeField]
    private Text _deviceList;

		private List<string> namesOfNearbyDevices = new List<string>();
		private string jsonOfNearbyDevices;
    private string[] deviceNames;

		// Update is called once per frame
		void Update () {
			using (AndroidJavaClass btServiceClass = new AndroidJavaClass (BLUETOOTH_CLASS)) {
          string oldJson = jsonOfNearbyDevices;
					jsonOfNearbyDevices = btServiceClass.CallStatic<string>("getNearbyDeviceNamesAsJson");
          if (!oldJson.Equals(jsonOfNearbyDevices)) {
            updateDeviceList();
          }
			}
		}

		void updateDeviceList() {
      jsonOfNearbyDevices = jsonOfNearbyDevices.Replace("[", "");
      jsonOfNearbyDevices = jsonOfNearbyDevices.Replace("]", "");
      jsonOfNearbyDevices = jsonOfNearbyDevices.Replace("\"", "");
      deviceNames = jsonOfNearbyDevices.Split(',');
      _deviceList.text = "";
      foreach (string device in deviceNames) {
        _deviceList.text = _deviceList.text + device + "\n";
      }
		}

 		public void askForBluetoothPermission() {
      using (AndroidJavaClass btServiceClass = new AndroidJavaClass (BLUETOOTH_CLASS)){
					AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject> ("getInstance");
					AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

					btService.Call ("setMainContext", context);
					btService.Call ("connectToBluetooth");
			}
 		}

		public void startGetPairedDevices() {
			using (AndroidJavaClass btServiceClass = new AndroidJavaClass (BLUETOOTH_CLASS)){
					AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject> ("getInstance");
					AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

					btService.Call ("setMainContext", context);
					btService.Call ("connectToBluetooth");
					btService.Call ("startGettingPairedDevices");
					_searchingLabel.text = "Searching for devices...";
          jsonOfNearbyDevices = "";
					_deviceList.text = "";
			}
		}

		public void stopGetPairedDevices() {
			using (AndroidJavaClass btServiceClass = new AndroidJavaClass (BLUETOOTH_CLASS)){
					AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject> ("getInstance");
					AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

					btService.Call ("stopGettingPairedDevices");
					_searchingLabel.text = "";
			}
		}

    public void connectToNearestPairedDevice() {
			using (AndroidJavaClass btServiceClass = new AndroidJavaClass (BLUETOOTH_CLASS)){
					AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject> ("getInstance");
					AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

					btService.Call ("connectToNearestPairedDevice");
			}
		}
}
