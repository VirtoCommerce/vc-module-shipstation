# Installation
Installing the module:
* Automatically: in VC Manager go to Configuration -> Modules -> ShipStation fulfillment -> Install
* Manually: download module zip package from https://github.com/VirtoCommerce/vc-module-Shipstation/releases. In VC Manager go to Configuration -> Modules -> Advanced -> upload module package -> Install.

Connecting to ShipStation:

Follow the steps provided in <a href="https://help.shipstation.com/hc/en-us/articles/360025856192-Custom-Store-Development-Guide" target="_blank">Connect to ShipStation</a> section of ShipStation's Custom Store Development Guide.

Be sure that "URL to custom XML page" parameter points to your public Commerce Manager site and ends with "/api/shipstation/{storeId}":
![image](https://cloud.githubusercontent.com/assets/5801549/17191416/87e432c8-5452-11e6-981a-0cde04183dec.png)

Ensure to type corresponding order statuses from VirtoCommerce like "New", "Processing", etc.

Check "Bringing Orders into ShipStation" and "Send Shipping Notifications from ShipStation" sections on the same guide onwards.

# Settings
This module has no settings defined as all integration actions are initiated from ShipStation system.