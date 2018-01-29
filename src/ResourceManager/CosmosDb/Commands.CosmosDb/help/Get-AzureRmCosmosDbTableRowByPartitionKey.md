---
external help file: AzureRmStorageTableCoreHelper-help.xml
Module Name: azurermstoragetable
online version:
schema: 2.0.0
---

# Get-AzureRmCosmosDbTableRowByPartitionKey

## SYNOPSIS
Returns one or more rows/entities based on Partition Key

## SYNTAX

```
Get-AzureRmCosmosDbTableRowByPartitionKey [-table] <Object> [-partitionKey] <String> [<CommonParameters>]
```

## DESCRIPTION
Returns one or more rows/entities based on Partition Key

## EXAMPLES

### EXAMPLE 1
```
# Getting rows by partition Key
```

$saContext = (Get-AzureRmStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount).Context
$table = Get-AzureRmCosmosDbTable -Name $tableName -Context $saContext
Get-AzureRmCosmosDbTableRowByPartitionKey -table $table -partitionKey $newPartitionKey

## PARAMETERS

### -table
Table object of type Microsoft.WindowsAzure.Commands.Common.Storage.ResourceModel.AzureStorageTable to retrieve entities

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -partitionKey
Identifies the table partition

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
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
