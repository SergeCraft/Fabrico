# Fabrico
This is a small mockup of hypercasual game, inspired by some test task specifications and Factorio game.
<img src="https://drive.google.com/uc?export=view&id=1kkMuawCyF8wQtAQPsECx2hAetoS_TUwu" alt="screenshot1" width="300"/>
<img src="https://drive.google.com/uc?export=view&id=1EbU4HRbKWtHzsozT2GtCb54cg0-Jlpza" alt="screenshot2" width="300"/>
## Decription
A player (simple capsule with backpack) is a loader on works.
His target is to produce some resources with factories. 
Factory can require some resources to produce another resource for specified time.
Red factory produces red resource without required any resource;
Yellow factory produces yellow resource and requires 1 red resource;
Blue factory requires 1 red and 1 yellow resources  to produce 1 blue resource;
It also not producing resource if required resouces on input storage isn't enough or
output storage is full.
Player can collect resources from output storage to his backpack (limited size) and 
drop them to input storage of factory if he is inside storage interaction area.

Key features:
* There is three types of factories, but may be extended via config class
* Factories spawns at randomized location
* Player controled via virtual joystick (mobile app blank)
* Factories sending messages to scroll area with reason of its idle
* All visuals made without external editors

## Download build
Actual build for Windows x86_64 is available 
<a href="https://drive.google.com/uc?export=download&id=10TO1GibE-IrIhjyQbROmuVeWhFR7QlOq">here</a>
