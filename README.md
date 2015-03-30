# Page Rank Scraper
Grabs the Google Page Rank of a url, and some alexa statistics if it can.

#### Usage
Input must have 1 argument:

1.  Url (www sensitive)

#### Example Result
Output is a json with the page rank and statistics:
```
{
"googlePageSuccess": True,
"googlePageRank": 9,
"alexaSuccess": True,
"claimedDate": "8/1/2014 6:32:28 PM",
"linksIn": 3804847,
"rankDelta": 0,
"reachRank": 1,
"trafficRank": 1
}
```
