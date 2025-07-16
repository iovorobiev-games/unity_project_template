# Unity Project Template 

This project is created for faster prototyping, when new project with the same setup needs to be created for yet another prototype.

## Content

The project sets up libraries:

* [UniTask](https://github.com/Cysharp/UniTask)
* [DOTween](https://dotween.demigiant.com/getstarted.php)
* [TMPEffects](https://github.com/Luca3317/TMPEffects)
* [Game Analytics](https://www.gameanalytics.com/)

The project also contains set of utilities, which for now includes:

* Bridge between unitask and clicks on game objects, which allows to await click if needed

The project also contains CI/CD pipeline which is configured to:

* Build web unity build for every commit in the repo
* Publish the build on itch.io
