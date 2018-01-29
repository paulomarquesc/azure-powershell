---
external help file: AzureRmStorageTableCoreHelper-help.xml
Module Name: azurermstoragetable
online version:
schema: 2.0.0
---

# Remove-AzureRmCosmosDbTableRow

## SYNOPSIS
Remove-AzureRmCosmosDbTableRow - Removes a specified table row

## SYNTAX

### byEntityPSObjectObject
```
Remove-AzureRmCosmosDbTableRow -table <Object> -entity <Object> [<CommonParameters>]
```

### byPartitionandRowKeys
```
Remove-AzureRmCosmosDbTableRow -table <Object> -partitionKey <String> -rowKey <String> [<CommonParameters>]
```

## DESCRIPTION
Remove-AzureRmCosmosDbTableRow - Removes a specified table row.
It accepts multiple deletions through the Pipeline when passing entities returned from the Get-AzureRmCosmosDbTableRow
available cmdlets.
It also can delete a row/entity using Partition and Row Key properties directly.

## EXAMPLES

### EXAMPLE 1
```
# Deleting an entry by entity PS Object
```

$saContext = (Get-AzureRmStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount).Context
$table = Get-AzureRmCosmosDbTable -Name $tableName -Context $saContext	
\[string\]$filter1 = \[Microsoft.WindowsAzure.Storage.Table.TableQuery\]::GenerateFilterCondition("firstName",\[Microsoft.WindowsAzure.Storage.Table.QueryComparisons\]::Equal,"Paulo")
\[string\]$filter2 = \[Microsoft.WindowsAzure.Storage.Table.TableQuery\]::GenerateFilterCondition("lastName",\[Microsoft.WindowsAzure.Storage.Table.QueryComparisons\]::Equal,"Marques")
\[string\]$finalFilter = \[Microsoft.WindowsAzure.Storage.Table.TableQuery\]::CombineFilters($filter1,"and",$filter2)
$personToDelete = Get-AzureRmCosmosDbTableRowByCustomFilter -table $table -customFilter $finalFilter
$personToDelete | Remove-AzureRmCosmosDbTableRow -table $table

### EXAMPLE 2
```
# Deleting an entry by using partitionkey and row key directly
```

$saContext = (Get-AzureRmStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount).Context
$table = Get-AzureRmCosmosDbTable -Name $tableName -Context $saContext	
Remove-AzureRmCosmosDbTableRow -table $table -partitionKey "TableEntityDemoFullList" -rowKey "399b58af-4f26-48b4-9b40-e28a8b03e867"

### EXAMPLE 3
```
# Deleting everything
```

$saContext = (Get-AzureRmStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount).Context
$table = Get-AzureRmCosmosDbTable -Name $tableName -Context $saContext	
Get-AzureRmCosmosDbTableRowAll -table $table | Remove-AzureRmCosmosDbTableRow -table $table

## PARAMETERS

### -table
Table object of type Microsoft.WindowsAzure.Commands.Common.Storage.ResourceModel.AzureStorageTable where the entity exists

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -entity
{{Fill entity Description}}

```yaml
Type: Object
Parameter Sets: byEntityPSObjectObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -partitionKey
{{Fill partitionKey Description}}

```yaml
Type: String
Parameter Sets: byPartitionandRowKeys
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -rowKey
{{Fill rowKey Description}}

```yaml
Type: String
Parameter Sets: byPartitionandRowKeys
Aliases:

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
