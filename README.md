# UdonNotifier v2
A VRChat Notification System for your world. Use this prefab for sending notification messages to players. You can use it for sending error messages, hints, tooltips, visual feedback after interacting with objects or use some of the built in features like:
* Player Join/Leave notification
* Outdated instance warning
* Custom welcome message

Additionally supports:
* Overriding notification sound and display time
* Supports showing multiple notifications at the same time

A preview VRChat world is available [here](https://vrchat.com/home/world/wrld_b0cb0277-4e43-45e5-89be-ba671fa25562). 

![image](https://github.com/user-attachments/assets/a5ae01a2-f3e9-49af-b2f5-c7e97f8be93b)

## Installation
Download the prefab from releases, double click the `.unitypackage` and in Unity click on `Import`. Make sure to also import the required TextMeshPro Assets if you haven't already!

![image](https://github.com/danielfvm/UdonNotifier/assets/23420640/57282709-bd87-4bf2-8345-422ff77d4c95)

Finally, Drag and Drop the `Assets/UdonNotifier/NotificationManager` Prefab into your Scene.

## Configuration
The `NotificationManager` contains following scripts that can individually be configured:
* NotificationManager
  Here you can configure the position, size and default fade in/out sound of the notifications.
  Use a reference to the script to send your own custom Notifications. 
* Join Leave Notifier
  Shows notifications when a player joins or leaves your world. You can change the message format, duration and optionally you can disable seeing your own join/leave notifications. If you don't want join/leave notifications in your world, disable the Component.
  
![image](https://github.com/user-attachments/assets/371cdb40-1826-4295-9b53-443b98b6d3a2)
  
* Welcome Notifier
  Displays a custom welcome message to players joining your world. You could use it to give players a hint how to access menus, general information, plug your patreon or simply to greet them :). Disable the Component if you don't need this.
  
![image](https://github.com/user-attachments/assets/e69c59ca-8543-464b-b2b5-bf2f7a24a6f9)

* Update Notifier
  Will automatically warn the player if they joined an outdated instance of your world. This can be useful if you have many active players in your world and frequently update your world, as things might get broken when players with different versions of your world join the same instance. You can change the message and display duration in the settings. Disable the Component if you don't need this. Additionally it will display a debug log message that shows the version and upload date of both the instance and your local version.
  
  ![image](https://github.com/danielfvm/UdonNotifier/assets/23420640/af8b40af-605e-4f3b-aadb-67a5940e9865)

## Custom Messages
Sending a custom notification from UdonSharp is as easy as:
```cs
using DeanCode;

// Reference to the NotificationManager GameObject
[SerializeField] NotificationManager manager;

void Start()
{
    // Send a `Hello World!` Notification with a warning symbol for 10 seconds
    manager.SendNotification("Hello World!", NotificationType.Warning, 10);
}
```

## Credits
Crediting this prefab is not required, but if you do it is very appreciated <3 
