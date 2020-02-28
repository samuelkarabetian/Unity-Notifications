# Unity-Message-System

## Instructions

1. Create a NotificationDisplayerSettings asset in the project panel.
2. Create a GameObject in your scene and give it the NotificationDisplayer component.
2. Drag in the NotificationDisplayerSettings asset to the settings field of the NotificationDisplayer component.
3. Create a NotificationStyle asset (or use DefaultNotificationStyle) in the project panel.
4. Create a Notification asset in the project panel.
5. Drag in the NotificationStyle asset into the style field of the Notification Asset.
6. Call <b>NotificationDisplayer.DisplayNotification()</b> and pass in your notification to display it to the screen.