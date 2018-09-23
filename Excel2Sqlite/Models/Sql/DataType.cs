using System;

namespace Excel2Sqlite
{
    public enum DataType
    {
        System_Boolean = 0,
        System_Int32 = 1,
        System_Int64 = 2,
        System_Double = 3,
        System_DateTime = 4,
        System_String = 5
    }

    public static class DataTypeExtension
    {
        public static string GetSqlDataType(this DataType dataType)
        {
            switch (dataType)
            {
                case (DataType.System_Boolean):
                    return "INTEGER";
                case (DataType.System_DateTime):
                    return "TEXT";
                case (DataType.System_Double):
                    return "DOUBLE";
                case (DataType.System_Int32):
                    return "int32";
                case (DataType.System_Int64):
                    return "int64";
                case (DataType.System_String):
                    return "VARCHAR(60)";
                default:
                    return "VARCHAR(60)";
            }
        }

        public static dynamic GetDynamicValue(this DataType dataType, string value)
        {
            switch (dataType)
            {
                case (DataType.System_Boolean):
                    return bool.Parse(value);
                case (DataType.System_DateTime):
                    return DateTime.Parse(value);
                case (DataType.System_Double):
                    return double.Parse(value);
                case (DataType.System_Int32):
                    return int.Parse(value);
                case (DataType.System_Int64):
                    return long.Parse(value);
                case (DataType.System_String):
                    return value;
                default:
                    return value;
            }
        }
    }
}
