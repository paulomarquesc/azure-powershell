---
external help file: AzureRmStorageTableCoreHelper-help.xml
Module Name: azurermstoragetable
online version:
schema: 2.0.0
---

# Update-AzureRmCosmosDbTableRow

## SYNOPSIS
Updates a table entity

## SYNTAX

```
Update-AzureRmCosmosDbTableRow [-table] <Object> [-entity] <Object> [<CommonParameters>]
```

## DESCRIPTION
Updates a table entity.
To work with this cmdlet, you need first retrieve an entity with one of the Get-AzureRmCosmosDbTableRow cmdlets available
and store in an object, change the necessary properties and then perform the update passing this modified entity back, through Pipeline or as argument.
Notice that this cmdlet accepts only one entity per execution.

## EXAMPLES

### EXAMPLE 1
```
# Updating an entity
```

$saContext = (Get-AzureRmStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount).Context
$table = Get-AzureRmCosmosDbTable -Name $tableName -Context $saContext	
\[string\]$filter = \[Microsoft.WindowsAzure.Storage.Table.TableQuery\]::GenerateFilterCondition("firstName",\[Microsoft.WindowsAzure.Storage.Table.QueryComparisons\]::Equal,"User1")
$person = Get-AzureRmCosmosDbTableRowByCustomFilter -table $table -customFilter $filter
$person.lastName = "New Last Name"
$person | Update-AzureRmCosmosDbTableRow -table $table

## PARAMETERS

### -table
Table object of type Microsoft.WindowsAzure.Commands.Common.Storage.ResourceModel.AzureStorageTable where the entity exists

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

### -entity
The entity/row with new values to perform the update.

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
