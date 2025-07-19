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
* Trivial Service Locator framework 

The project also contains CI/CD pipeline which is configured to:

* Build web unity build for every commit in the repo
* Publish the build on itch.io

## Setting up CI/CD

For successfully building and publishing the game to itch following needs to be set up:

### Secrets

For building the projects unity secrets need to be set up. Read more on the [Builder docs](https://game.ci/docs/github/builder/)

* UNITY\_EMAIL - email used to authorize in the unity hub
* UNITY\_PASSWORD - corresponding password
* UNITY\_LICENSE - contents of .ulf file. In case license is already activated, it should be located in C:/Program Data/Unity folder by default

For publishing to itch following secrets should be set up:

* BUTLER\_CREDENTIALS - the key, used by butler to authenticate. Read more on the [Butler docs](https://itch.io/docs/butler/login.html)

And the following variables:

* Itch\_User - username of the itch account where the game should be published
* Itch\_Game - the name of the game as stated on the itch.io page

NOTE: the game page must be setup beforehand, otherwise nothing will be published.

