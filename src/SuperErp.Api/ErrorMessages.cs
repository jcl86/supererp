namespace SuperErp.Core
{
    public static class ErrorMessages
    {
        public static string Empty(string fieldName) => $"{fieldName} can not be empty";
        public static string MaximunLength(string fieldName, int maxLength) => $"{fieldName} maximun length can not be greater than {maxLength}";
    }
}
