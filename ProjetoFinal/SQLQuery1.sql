DECLARE @sql NVARCHAR(MAX) = '';

SELECT @sql += 
'
PRINT ''===== ' + t.name + ' ====='';

SELECT * 
FROM [' + t.name + '];

'
FROM sys.tables t;

EXEC sp_executesql @sql;