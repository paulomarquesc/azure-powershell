---
external help file: AzureRmStorageTableCoreHelper-help.xml
Module Name: azurermstoragetable
online version:
schema: 2.0.0
---

# Get-AzureRmCosmosDbTable

## SYNOPSIS
Gets a Cosmos DB Table object.

## SYNTAX

### AzureCosmosDb
```
Get-AzureRmCosmosDbTable -resourceGroup <String> -tableName <String> -cosmosDbAccount <String>
 [<CommonParameters>]
```

## DESCRIPTION
Gets a Cosmos DB Table object.

## EXAMPLES

### EXAMPLE 1
```
# Getting Cosmos DB table object
```

$resourceGroup = "myResourceGroup"
$databaseName = "myCosmosDbName"
$tableName = "table01"
$table01 = Get-AzureRmCosmosDbTable -resourceGroup $resourceGroup -tableName $tableName -cosmosDbAccount $databaseName

## PARAMETERS

### -resourceGroup
Resource Group where the Azure Storage Account or Cosmos DB are located

```yaml
Type: String
Parameter Sets: AzureCosmosDb, AzureRmTableStorage
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -tableName
Name of the table to retrieve

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -cosmosDbAccount
CosmosDB account name where the table lives (this parameter was previously called databaseName, which is now an alias of this parameter)

```yaml
Type: String
Parameter Sets: AzureCosmosDb
Aliases: databaseName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
