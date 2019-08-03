VCtrlsJoystick is a high performance on screen joy stick that runs on the 
new unity 4.6 UI system that takes advantage of the new event system in 4.6.


Features:
-Full support for multi-touch and multiple sticks.
-Ready to use Prefabs
-Manager to easily get data from joysticks in the scene (works similar to the default input class.)
-Vertical and horizontal constraints.
-Floating joystick support
-First Person Exmeple.


Usage:
Place JoyStickPrefab in scene under your canvas, and rename its root, to what you want your joystick to be named.
than under the root move the "Joystick" to where you would like it, and adjust the "TriggerArea" to fill the area,
on screen where you want to be able to activate the joystick.

If "Appear On Press" is enabled on the joy stick the stick will appear anyone in the "TriggerArea".
Constraints are used when you want a stick to work on only 1 axis.

Add the "VCtrlsManager" script to its own gameobject in the scene or to the EventSystem object made by Canvas. 

Code:
To get data drom the joysticks in your code you can use the VCtrlsManager class in any of these ways.

```
Vector2 stickPos = VCtrlsManager.Stick["StickName"].GetVector();

or

float stickPosX = VCtrlsManager.Stick["StickName"].GetAxis(0);
float stickPosY = VCtrlsManager.Stick["StickName"].GetAxis(1);

or

float stickPosX = VCtrlsManager.Stick["StickName"].GetAxis("horizontal");
float stickPosY = VCtrlsManager.Stick["StickName"].GetAxis("vertical");
```


Video Walthrough
https://www.youtube.com/watch?v=1bgeUtMZcso
