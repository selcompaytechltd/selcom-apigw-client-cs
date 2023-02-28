 
# SELCOM API CLIENT
<p align='center'>
<img src="https://img.shields.io/badge/C%23-asp.net-blue">
<img src="https://img.shields.io/badge/net%20-v6.0-blue">

</p >

## Homepage
https://developers.selcommobile.com/

## Description
This is a library containing functions that aid in the accessing of selcom api. IT is made up pf 4 functions.


## Installation 
dotnet add package  selcom-apigw-client

### Use

```cs
//use package
using selcom_apgw_client;

//// initalize a new apiAccess instace with values of the base url, api key and api secret

apigwClient client = new apigwClient(baseUrl, apiKey, apiSecret);

// computeHeader a dictionary containing data to bes submitted
// computeHeader returns an array with values for the following header fields: 
// Authorization, Timestamp, Digest, Signed-Fields
client.computeHeader( dictData):

// postFuct takes relative path to base url. dictionary containing data to be submitted 
// It performs a POST request of the submitted data to the destniation url generatingg the header internally
// IT returns a String containing the response data to the request
client.postFunc(path, dictData)

// getFuct takestakes relative path to base url. dictionary containing data to be submitted  
// It performs a GET request adding the query to the  url and generatingg the header internally
// IT returns a Stringcontaining the response data to the request
client.getFunc(path, dictData)

// deletetFuct takes relative path to base url.dictionary containing data to be submitted 
// It performs a DELETE request adding the query to the  url and generatingg the header internally
// IT returns a String containing the response data to the request
client.deleteFunc(path, dictData)
```
### Examples
```cs
//import package
using selcom_apgw_client;

String apiKey = '202cb962ac59075b964b07152d234b70';
String apiSecret = '81dc9bdb52d04dc20036dbd8313ed055';
String baseUrl = "http://example.com";



// initalize a new Client instace with values of the base url, api key and api secret
var client = new apigwClient(baseUrl, apiKey, apiSecret);

//order data
Dictionary<String, Object> orderDict = new Dictionary<String,Object>() ;
orderDict.Add("vendor","VENDORTILL");
orderDict.Add("order_id","1218d5Qb");
orderDict.Add("buyer_email", "john@example.com");
orderDict.Add("buyer_name", "John Joh");
orderDict.Add("buyer_phone","255682555555");
orderDict.Add("amount",  8000);
orderDict.Add("currency","TZS");
orderDict.Add("buyer_remarks","None");
orderDict.Add("merchant_remarks","None");
orderDict.Add("no_of_items",  1);


// path relatiive to base url
String orderPath = "/v1/checkout/create-order-minimal";
//crate new order

var orderRespose = client.postFunc(orderPath, orderDict);
//print response
Console.Write(orderResponse);
```