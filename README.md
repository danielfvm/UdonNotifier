# UdonNotifier
A VRChat Notification System for your world. Use this prefab for sending notification messages to players. You can use it for sending error messages, hints, tooltips, visual feedback after interacting with objects or use some of the built in features like:
* Player Join/Leave notification
* Outdated instance warning
* Custom welcome message

Additionally supports:
* Overriding notification sound and display time
* Supports showing multiple notifications at the same time

![image](https://github.com/danielfvm/UdonNotifier/assets/23420640/37afeafc-1162-4cc9-a6ab-c74f3aea948e)

## Installation
Download the prefab from releases, click on the `.unitypackage` and in Unity click on `Import`
![image](https://github.com/danielfvm/UdonNotifier/assets/23420640/57282709-bd87-4bf2-8345-422ff77d4c95)

Finally, Drag and Drop the `Assets/UdonNotifier/NotificationManager` Prefab into your Scene.

## Configuration
The `NotificationManager` contains following scripts that can individually be configured:
* NotificationManager
  Here you can configure the position, size and default fade in/out sound of the notifications.
  Use a reference to the script to send your own custom Notifications. 
* Join Leave Notifier
  Shows notifications when a player joins or leaves your world. You can change the message format, duration and optionally you can disable seeing your own join/leave notifications. If you don't want join/leave notifications in your world, disable the Component.
  
  ![image](https://github.com/danielfvm/UdonNotifier/assets/23420640/d2b435f5-83d4-4eaa-8d0b-c0255331f568)
  
* Welcome Notifier
  Displays a custom welcome message to players joining your world. You could use it to give players a hint how to access menus, general information, plug your patreon or simply to greet them :). Disable the Component if you don't need this.
  
  ![image](https://github.com/danielfvm/UdonNotifier/assets/23420640/8b15c50f-2d5d-4326-b038-efc4c1ee3e19)

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
    // Send a `Hello World!` Notification with a warning symbol
    manager.SendNotification("Hello World!", NotificationType.Warning);
}
```

## Credits
Crediting this prefab is not required, but if you do it is very appreciated <3 
