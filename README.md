<a name="readme-top"></a>
<!-- ABOUT THE PROJECT -->
# CS2 Automated Messages Plugin

This plugin allows sending timed messages to everyone on the server or players that just joined.

Currently these message types are supported:
* Colored chat message
* Center popup message
* Center alert popup message


If you like the project don't forget to give it a star ★! If you have any issues or suggestions feel free to add them [here](https://github.com/Fisers/cs2-automated-messages/issues "here").

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

To install the plugin you'll need [Metamod:Source 2.0](https://www.metamodsource.net/downloads.php/?branch=master "Metamod:Source 2.0") and [CounterStrikeSharp](https://docs.cssharp.dev/docs/guides/getting-started.html "CounterStrikeSharp")

### Installation

1. Head to the [releases page](https://github.com/Fisers/cs2-automated-messages/releases "releases page") and download the latest release
2. Extract the .zip file into `game/csgo/addons`
3. Enjoy!


<!-- USAGE EXAMPLES -->
## Usage

To change automated server messages, you'll need to edit `/addons/counterstrikesharp/configs/plugins/AutomatedMessages/config.json`

Here is an example config:
```
{
  // Prefix that gets applied to all Chat messages
  "ChatMessagePrefix": "{green}Automated Messages | {default}",
  "MessageGroups": [
    {
      // Interval 0 means it's a welcome message that gets sent when a player joins
      "Interval": 0,
      "Messages": [
        {
          "Delay": 0,
          "MessageType": 0,
          "Message": "Message that gets sent as soon as the player joins"
        },
        {
          // Delay the sending of the message (useful to send the message after other plugins)
          "Delay": 10,
          // Message type: 0 - CHAT | 1 - CENTER | 2 - CENTER RED ALERT
          "MessageType": 0,
          "Message": "{red}Message that gets sent after 10 seconds"
        }
      ]
    },
    {
      // Rotate over the list of messages every 5 seconds and send them
      "Interval": 5,
      "Messages": [
        {
          "Delay": 0,
          "MessageType": 0,
          "Message": "1st message that gets sent every 5 seconds"
        },
        {
          "Delay": 0,
          "MessageType": 0,
          "Message": "{lightpurple}2nd message that gets sent every 5 seconds"
        }
      ]
    },
    {
      "Interval": 10,
      "Messages": [
        {
          "MessageType": 1,
          "Message": "1st center message that gets sent every 30 seconds"
        },
        {
          "MessageType": 2,
          "Message": "2nd center alert message that gets sent every 30 seconds"
        }
      ]
    }
  ]
}
```
Colored Chat messages support the following colors:
`{default} {white} {darkred} {lightpurple} {green} {lightgreen} {slimegreen} {red} {grey} {yellow} {invisible} {lightblue} {blue} {purple} {pink} {fadedred} {gold}`


<!-- ROADMAP -->
## Roadmap

- [x] Add Chat Message sending
	- [x] Implement timers for sending
	- [x] Add some color to the messages
- [x] Implement JSON configuration
- [x] Add Message Delay
- [x] Add Center Message Popup
	- [x] Implement Default Center Popup
	- [x] Implement Alert Type of the Popup
- [x] Add Chat Message prefix for every message
- [ ] Add dynamic tags like current time/map/player_count
- [ ] Multi-language Support
    - [ ] Russian
    - [ ] Latvian


<!-- CONTACT -->
## Contact

Kwasiks - [steam](https://steamcommunity.com/id/pushka_/) - kwasiks@riftrium.com


<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

I would like to thank the following repositories for helping with solutions:

* [cm-cs2-colorsay ](https://github.com/Challengermode/cm-cs2-colorsay)
* [cs2-advertisement ](https://github.com/partiusfabaa/cs2-advertisement)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
