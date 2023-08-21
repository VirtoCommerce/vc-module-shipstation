# Overview

[![CI status](https://github.com/VirtoCommerce/vc-module-shipstation/workflows/Module%20CI/badge.svg?branch=dev)](https://github.com/VirtoCommerce/vc-module-shipstation/actions?query=workflow%3A"Module+CI") [![Quality gate](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=alert_status&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation) [![Reliability rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=reliability_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation) [![Security rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=security_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation) [![Sqale rating](https://sonarcloud.io/api/project_badges/measure?project=VirtoCommerce_vc-module-shipstation&metric=sqale_rating&branch=dev)](https://sonarcloud.io/dashboard?id=VirtoCommerce_vc-module-shipstation)

ShipStation fulfillment module enables synchronizing customer orders with <a href="http://www.shipstation.com/" target="_blank">ShipStation</a>. That way new orders with shipments placed in VirtoCommerce will become available in ShipStation admin and changes of shipment status will be synced with VirtoCommerce orders.

## Key features
* Export orders specified by store
* Updating order's and shipping's statuses when shipstation make an action

## Current constraints
* On order updating, all data is ignoring, only order's status will setted as 'Completed' and shipment status become 'Sent'. This can be cusomized by custom module).


## Connecting to ShipStation

!!! note
    All operations are accessible via Rest API only. You will need to create [an API Key and grant permission before the call](https://virtocommerce.com/docs/latest/user-guide/security/#generate-api-key).
    You need to grant `ShipStation:read` and `ShipStation:update` permissions.


Follow the steps provided in [Connect to ShipStation](https://help.shipstation.com/hc/en-us/articles/360025856192-Custom-Store-Development-Guide) section of ShipStation's Custom Store Development Guide.


Be sure that "URL to custom XML page" parameter points to your public Commerce Manager site and ends with `/api/shipstation/{storeId}?api_key={ApiKey}`:

![image](https://cloud.githubusercontent.com/assets/5801549/17191416/87e432c8-5452-11e6-981a-0cde04183dec.png)

Ensure to type corresponding order statuses from VirtoCommerce like "New", "Processing", etc.

Check "Bringing Orders into ShipStation" and "Send Shipping Notifications from ShipStation" sections on the same guide onwards.

## API Specifications

### Get Orders

ShipStation will use the following API for requesting order information:

```json
    GET /api/shipstation/{storeId}?api_key={ApiKey}&action=export&start_date=[Start Date]&end_date=[End Date]&page=1 

```

### Update Order

The POST call allows ShipStation to post shipment information back to your order.

```json
    POST /api/shipstation/{storeId}?api_key={ApiKey}

```


## Settings
This module has no settings defined as all integration actions are initiated from ShipStation system.
