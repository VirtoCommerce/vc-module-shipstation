# Overview

[![CI status](https://github.com/VirtoCommerce/vc-module-shipstation/workflows/Module%20CI/badge.svg?branch=dev)](https://github.com/VirtoCommerce/vc-module-shipstation/actions?query=workflow%3A"Module+CI") [![Quality gate](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=alert_status&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation) [![Reliability rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=reliability_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation) [![Security rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=security_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation) [![Sqale rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=sqale_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation)

The Shipstation integration module allows to export orders to the shipstation account and update it's shipping status.

## Key features
* Export orders specified by store
* Updating order's and shipping's statuses when shipstation make an action

## Current constraints
* There is no any authentication for the API requests
* On order updating, all data is ignoring, only order's status will setted as 'Completed' and shipment status become 'Sent' (This behavior might be overrided)

# Installation
Installing the module:
* Automatically: in VC Manager go to Configuration -> Modules -> ShipStation fulfillment -> Install
* Manually: download module zip package from https://github.com/VirtoCommerce/vc-module-Shipstation/releases. In VC Manager go to Configuration -> Modules -> Advanced -> upload module package -> Install.

Connecting to ShipStation:

Follow the steps provided in [Connect to ShipStation](https://help.shipstation.com/hc/en-us/articles/360025856192-Custom-Store-Development-Guide) section of ShipStation's Custom Store Development Guide.

Be sure that "URL to custom XML page" parameter points to your public Commerce Manager site and ends with "/api/shipstation/{storeId}":
![image](https://cloud.githubusercontent.com/assets/5801549/17191416/87e432c8-5452-11e6-981a-0cde04183dec.png)

Ensure to type corresponding order statuses from VirtoCommerce like "New", "Processing", etc.

Check "Bringing Orders into ShipStation" and "Send Shipping Notifications from ShipStation" sections on the same guide onwards.

# Settings
This module has no settings defined as all integration actions are initiated from ShipStation system.