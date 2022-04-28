# ShipStation Fulfillment Module
ShipStation fulfillment module enables synchronizing customer orders with <a href="http://www.shipstation.com/" target="_blank">ShipStation</a>. That way new orders with shipments placed in VirtoCommerce will become available in ShipStation admin and changes of shipment status will be synced with VirtoCommerce orders.

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

# License
Copyright (c) Virtosoftware Ltd.  All rights reserved.

Licensed under the Virto Commerce Open Software License (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at

http://virtocommerce.com/opensourcelicense

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied.
