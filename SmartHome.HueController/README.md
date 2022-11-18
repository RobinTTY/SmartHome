# HueController

HueController is a small WorkerService that allows for controlling my room lighting via keyboard hotkeys.

## Installation

## Registering the Service

sc.exe create HueController binpath= "<...>\SmartHome.HueController\bin\Release\net7.0-windows\publish\SmartHome.HueController.exe" start= auto

### Removing the Service

sc.exe delete HueController
