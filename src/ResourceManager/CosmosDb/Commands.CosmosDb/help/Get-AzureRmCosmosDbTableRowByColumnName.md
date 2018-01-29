---
external help file: AzureRmStorageTableCoreHelper-help.xml
Module Name: azurermstoragetable
online version:
schema: 2.0.0
---

# Get-AzureRmCosmosDbTableRowByColumnName

## SYNOPSIS
Returns one or more rows/entities based on a specified column and its value

## SYNTAX

### byString
```
Get-AzureRmCosmosDbTableRowByColumnName -table <Object> -columnName <String> -value <String> -operator <String>
 [<CommonParameters>]
```

### byGuid
```
Get-AzureRmCosmosDbTableRowByColumnName -table <Object> -columnName <String> -guidValue <Guid> -operator <String>
 [<CommonParameters>]
```

## DESCRIPTION
Returns one or more rows/entities based on a specified column and its value

## EXAMPLES

### EXAMPLE 1
```
# Getting row by firstname
```

$saContext = (Get-AzureRmStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount).Context
$table = Get-AzureRmCosmosDbTable -Name $tableName -Context $saContext
Get-AzureRmCosmosDbTableRowByColumnName -table $table -columnName "firstName" -value "Paulo" -operator Equal

## PARAMETERS

### -table
Table object of type Microsoft.WindowsAzure.Commands.Common.Storage.ResourceModel.AzureStorageTable to retrieve entities

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

### -columnName
Column name to compare the value to

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

### -value
Value that will be looked for in the defined column

```yaml
Type: String
Parameter Sets: byString
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -guidValue
{{Fill guidValue Description}}

```yaml
Type: Guid
Parameter Sets: byGuid
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -operator
Supported comparison operator.
Valid values are "Equal","GreaterThan","GreaterThanOrEqual","LessThan" ,"LessThanOrEqual" ,"NotEqual"

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
