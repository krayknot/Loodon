namespace LoodonDAL
{
    public class ClsQueries
    {
        public static readonly string SqlGetcolumnproperties = "SELECT COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','AllowsNull')AS 'AllowsNull'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','ColumnId')AS 'ColumnId'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','FullTextTypeColumn')AS 'FullTextTypeColumn'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsComputed')AS 'IsComputed'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsCursorType')AS 'IsCursorType'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsDeterministic')AS 'IsDeterministic'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsFulltextIndexed')AS 'IsFulltextIndexed'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsIdentity')AS 'IsIdentity'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsIdNotForRepl')AS 'IsIdNotForRepl'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsIndexable')AS 'IsIndexable'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsOutParam')AS 'IsOutParam'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsPrecise')AS 'IsPrecise'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsRowGuidCol')AS 'IsRowGuidCol'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsSystemVerified')AS 'IsSystemVerified'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsXmlIndexable')AS 'IsXmlIndexable'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','Precision')AS 'Precision'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','Scale')AS 'Scale'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','StatisticalSemantics')AS 'StatisticalSemantics'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','SystemDataAccess')AS 'SystemDataAccess'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','UserDataAccess')AS 'UserDataAccess'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','UsesAnsiTrim')AS 'UsesAnsiTrim'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsSparse')AS 'IsSparse'," +
                                                        "COLUMNPROPERTY( OBJECT_ID('{TABLENAME}'),'{COLUMNNAME}','IsColumnSet')AS 'IsColumnSet'";



        public static readonly string SqlGetservertimestamp = "SELECT CURRENT_TIMESTAMP";


        public static readonly string SqlGettablefromcolumn = "SELECT t.name'Table Name', " +
                                                        "c.name'Column Name' FROM sys.tables AS t " +
                                                        "INNER JOIN sys.columns c ON t.OBJECT_ID = c.OBJECT_ID " +
                                                        "WHERE c.name LIKE '%{COLUMNAME}%' " +
                                                        "ORDER BY t.name;";


        public static readonly string SqlGetprimarykey = "SELECT KU.table_name as tablename,column_name as primarykeycolumn FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME and ku.table_name='{TABLENAME}' ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION;";



        public static readonly string SqlCountrecords = "select count(*) from {TABLENAME}";


        public static readonly string SqlGettablescript = "declare @table varchar(100) " +
                                                    "set @table = '{TABLENAME}' " +
                                                    "declare @sql table(s varchar(1000), id int identity) " +
                                                    "insert into  @sql(s) values ('create table [' + @table + '] (') " +
                                                    "insert into @sql(s) " +
                                                    "select '  ['+column_name+'] ' + data_type + coalesce('('+cast(character_maximum_length as varchar)+')','') + ' ' + " +
                                                        "case when exists (  " +
                                                            "select id from syscolumns " +
                                                           " where object_name(id)=@table " +
                                                    "and name=column_name " +
                                                            "and columnproperty(id,name,'IsIdentity') = 1  " +
                                                        ") then " +
                                                            "'IDENTITY(' +  " +
                                                           " cast(ident_seed(@table) as varchar) + ',' +  " +
                                                            "cast(ident_incr(@table) as varchar) + ')' " +
                                                        "else '' " +
                                                        "end + ' ' + " +
                                                        "( case when IS_NULLABLE = 'No' then 'NOT ' else '' end ) + 'NULL ' +  " +
                                                        "coalesce('DEFAULT '+COLUMN_DEFAULT,'') + ',' " +
                                                     "from information_schema.columns where table_name = @table " +
                                                     "order by ordinal_position " +
                                                    "declare @pkname varchar(100) " +
                                                    "select @pkname = constraint_name from information_schema.table_constraints " +
                                                    "where table_name = @table and constraint_type='PRIMARY KEY' " +
                                                    "if ( @pkname is not null ) begin " +
                                                       " insert into @sql(s) values('  PRIMARY KEY (') " +
                                                        "insert into @sql(s) " +
    	                                                    "select '   ['+COLUMN_NAME+'],' from information_schema.key_column_usage " +
    	                                                    "where constraint_name = @pkname " +
    	                                                    "order by ordinal_position " +
                                                        "update @sql set s=left(s,len(s)-1) where id=@@identity " +
                                                       " insert into @sql(s) values ('  )') " +
                                                    "end " +
                                                    "else begin " +
                                                        "update @sql set s=left(s,len(s)-1) where id=@@identity " +
                                                    "end " +
                                                    "insert into @sql(s) values( ')' ) " +
                                                    "select s from @sql order by id";

    }
}
