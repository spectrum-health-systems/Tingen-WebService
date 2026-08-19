<div align="center">

  <h1>Tingen Web Service: Scratchpad</h1>

</div>

***

```csharp
foreach (var path in Catalog.RequiredFolders(frameworkSetting))
{
    try
    {
        DuDirectory.ForceExist(path);
    }
    catch (Exception ex)
    {
        LogEvent.Primeval("ERR1000-FrameworkValidationFailed", ErrorMessage.ERR1000(ex.Message));
        //TODO - Should probably send an email notification.
    }
}

LogEvent.Primeval("ERR1000-FailedToLoadFrameworkSettings", ErrorMessage.ERR1000(ex.Message));
```

***

## Netsmart Query web service URL

The `TingenWebService_NtstWsvcQueryUat_Query` setting should be set to the URL of the Netsmart Query web service for your organization, which will look like this:

`https://{YourOrganization}.netsmartcloud.com/csp/{YourOrganization}uat/avpm/WEBSVC.Query.cls`
`https://shs-azu-nsws-01.spectrumhealthsystems.org/WebService/UAT/TingenWebService.asmx?WSDL`

***

## Error codes

1124
3242
4930
8857
1494
5876
7244
1605
6919
9034
1460
3424
4815
3177
4865
5288
5137
1752
6008
1548
3705
2707
2306
3205
8747
8050
7904
5362
3302
7889
9103
6463
3907
8574
7334
2265
1284
5952
9761
4303
4375
2182
8745
7337
8307
6474
7675
1358
8399
3785
8309
9608
6487
8348
3287
6910
9330
8721
3357
7816
3651
6589
9271
7436
1079
1399
3358
5073
1022
4429
4465
2565
4167
5502
3919
9822
6677
6558
8852
6052
2279
9097
1154
8052
7585
7586
2905
1493
8567
3565
6595

***
